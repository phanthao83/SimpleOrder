using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using SOBusinessControl.Ultility;
using SODtaAccess.Data.Repository.IRepository;
using SODtaModel;
using SODtaModel.View;
using System.Threading.Tasks;
namespace SOBusinessControl.Domain
{
    public class OrderBiz
    {
        private readonly IUnitOfWork _unitOfWork;
        public OrderBiz(IUnitOfWork unitofWork)
        {
            _unitOfWork = unitofWork;
        }

        public async Task<List<Order>> GetAllAsync()
        {
            var result = await _unitOfWork.OrderRepository.GetAllAsync();
            return result;

        }

        public async Task<OrderDetailView> GetOrderDetailAsync(int orderId)
        {
            /*Noted that OrderDetailOption is optinal. */
            Order orderInfo = await _unitOfWork.OrderRepository.GetAsync(orderId);
            if (orderInfo == null) throw new RestException(System.Net.HttpStatusCode.NotFound, new { order = "Not Found" });

            OrderDetailView result = new OrderDetailView()
            {
                Id = orderInfo.Id,
                CreateDate = orderInfo.CreateDate,
                CustomerId = orderInfo.CustomerId,
                Total = orderInfo.Total,
                Status = CodeTranslation.ToOrderStatusDescription(orderInfo.Status),
            };
            result.OrderLines = await GetOrderDetailList(orderId);

            return result;
        }

        private async Task<List<OrderLineView>> GetOrderDetailList(int orderId)
        {

            var detailLst = await _unitOfWork.OrderDetailRepository.GetByOrderAsync(orderId);
            if (detailLst == null)
                throw new RestException(System.Net.HttpStatusCode.NotFound, new { order = "Not Found" });

            var orderLineOptionLst = await _unitOfWork.OrderDetailOptionRepository.GetByOrderAsync(orderId);

            //Order Line Info
            List<OrderLineView> lstDetail = new List<OrderLineView>();
            foreach (var detail in detailLst)
            {
                if (detail.Status != OrderLineStatus.Cancelled)
                {
                    OrderLineView line = new OrderLineView()
                    {
                        Id = detail.Id,
                        ProductiId = detail.ProductiId,
                        ProductName = detail.Product.Name,
                        DiscountTotal = detail.DiscountTotal,
                        PromotionCode = detail.PromotionCode,
                        Quanity = detail.Quanity,
                        Status = CodeTranslation.ToOrderLineStatusDescription(detail.Status),
                        UPrice = detail.UPrice,
                        SubTotal = detail.SubTotal
                    };
                    //Get Options 
                    line.PurchasedOptions = GetOptionForEachLine(orderLineOptionLst, detail.Id);
                    lstDetail.Add(line);
                }

            }
            return lstDetail;
        }

        private List<OrderLineOptionView> GetOptionForEachLine(List<OrderLineOptionView> lstOption, int orderDetailId)
        {
            if (lstOption == null) return null;

            var selectedLst = lstOption.FindAll(o => o.OrderDetailId == orderDetailId);

            return selectedLst;
        }
        public async Task<bool> CreateOrderAsync(OrderDetailView orderView)
        {
            var validated = await ValidateDataAsync(orderView); 
            if (!validated) new RestException(HttpStatusCode.InternalServerError, new { order = "Invalid data" });

            Order order = new Order()
            {
                Id = 0,
                CreateDate = DateTime.Now,
                CustomerId = orderView.CustomerId,
                Status = OrderStatus.New,
                Total = orderView.Total,
            };
            await _unitOfWork.OrderRepository.AddAsync(order);

            //OrderLine 
            foreach (OrderLineView line in orderView.OrderLines)
            {
                OrderDetail detail = new OrderDetail()
                {
                    Id = 0,
                    Order = order,
                    DiscountTotal = line.DiscountTotal,
                    PromotionCode = line.PromotionCode,
                    ProductiId = line.ProductiId,
                    Quanity = line.Quanity,
                    Status = CodeTranslation.ToOrderLineCode(line.Status),
                    SubTotal = line.SubTotal,
                    UPrice = line.UPrice
                };
                await _unitOfWork.OrderDetailRepository.AddAsync(detail);

                //Options 
                if (line.PurchasedOptions != null)
                {
                    foreach (OrderLineOptionView opt in line.PurchasedOptions)
                    {
                        OrderDetailOption detailOpt = new OrderDetailOption()
                        {
                            Id = 0,
                            AdditionalCost = opt.AdditionalCost,
                            OptionId = opt.OptionId,
                            OrderDetailID = opt.OrderDetailId,
                        };
                        await _unitOfWork.OrderDetailOptionRepository.AddAsync(detailOpt);
                    }
                }

            }

            var success = await _unitOfWork.SaveAsync() > 0;
            if (success) return success;
            else
                throw new RestException(HttpStatusCode.InternalServerError, new { order = "Unable to save order" });
        }

        private async Task<bool> ValidateDataAsync(OrderDetailView orderView)
        {

            if (orderView == null)
                throw new RestException(HttpStatusCode.BadRequest, new { order = "Wrong data posted" });

            if (orderView.CustomerId == 0)
                throw new RestException(HttpStatusCode.BadRequest, new { order = "Invalid customer info" });

            var customer = await _unitOfWork.CustomerRepository.GetAsync(orderView.CustomerId);
            if (customer == null)
                throw new RestException(HttpStatusCode.BadRequest, new { order = "Invalid customer info" });

            if (orderView.OrderLines == null)
                throw new RestException(HttpStatusCode.BadRequest, new { order = "No order line " });

            Dictionary<int, int> purchasedProductList = new Dictionary<int, int>();
            Dictionary<int, int> maxAllowedPurchasedProductList = new Dictionary<int, int>();

            foreach (OrderLineView line in orderView.OrderLines)
            {
                if (line.ProductiId == 0)
                    throw new RestException(HttpStatusCode.BadRequest, new { order = "Invalid selected product " });

                Product product = await _unitOfWork.ProductRepository.GetAsync(line.ProductiId);
                if (product == null)
                    throw new RestException(HttpStatusCode.BadRequest, new { order = "Invalid selected product " });

                if (!maxAllowedPurchasedProductList.ContainsKey(product.Id)) maxAllowedPurchasedProductList.Add(product.Id, product.MaxAllowedOrderQty);
                if (purchasedProductList.ContainsKey(line.ProductiId))
                {
                    int oldValue;
                    if (purchasedProductList.TryGetValue(line.ProductiId, out oldValue))
                    {
                        purchasedProductList.Remove(line.ProductiId);
                        purchasedProductList.Add(line.ProductiId, oldValue + line.Quanity);
                    }
                }
                else purchasedProductList.Add(line.ProductiId, line.Quanity);

                if (line.PurchasedOptions != null)
                {
                    List<ProductOption> productOpts = await _unitOfWork.ProductOptionRepository.GetByProductAsync(line.ProductiId);
                    if (productOpts == null)
                        throw new RestException(HttpStatusCode.BadRequest, new { order = "Not found any product options for product " + line.ProductiId.ToString() });
                    foreach (OrderLineOptionView opt in line.PurchasedOptions)
                    {
                        var selectedOption = productOpts.Find(p => p.Id == opt.OptionId);
                        if (selectedOption == null)
                            throw new RestException(HttpStatusCode.BadRequest, new { order = "The seleced option of product " + line.ProductiId.ToString() } + " not valid");

                    }

                }
            }

            //Check constraint the purchased product is not over than MaxAllowedQuantity Per Order
            foreach (var purachasedProduct in purchasedProductList)
            {
                int maxAllowedQuantity = 0;
                if (maxAllowedPurchasedProductList.TryGetValue(purachasedProduct.Key, out maxAllowedQuantity))
                {
                    if (maxAllowedQuantity < purachasedProduct.Value)
                        throw new RestException(HttpStatusCode.BadRequest, new { order = "The quantity of  product " + purachasedProduct.Key.ToString() } + " is over the max allowed qty for the respective product");
                }
            }

            //Check total value 

            return true;
        }
    }
}
