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
    public class ProductBiz
    {
        private readonly IUnitOfWork _unitOfWork;
        public ProductBiz(IUnitOfWork unitofWork)
        {
            _unitOfWork = unitofWork;
        }
        public async Task<List<Product>> GetAllAsync()
        {
            var result = await _unitOfWork.ProductRepository.GetAllAsync();
            return result;

        }
        public async Task<ProductDetailView> GetDetailAsync(int productId)
        {
            var productInfo  = await _unitOfWork.ProductRepository.GetAsync(productId);
            if (productInfo == null)
                throw new RestException(HttpStatusCode.NotFound, new { customer = "Not found" });

            ProductDetailView result = new ProductDetailView()
            {
                Id = productInfo.Id,
                BasePrice = productInfo.BasePrice,
                Description = productInfo.Description,
                MaxAllowedOrderQty = productInfo.MaxAllowedOrderQty,
                Name = productInfo.Name

            };

            //Get Options
            var options = await _unitOfWork.ProductOptionRepository.GetByProductAsync(productId);
            if (options != null)
            {
                List<ProductOptionView> optViews = new List<ProductOptionView>(); 
                foreach (var opt in options)
                {
                    ProductOptionView optView = new ProductOptionView() 
                    {
                        AdditionalCost = opt.AdditionalCost,
                        Id = opt.Id,
                        OptionDescription = opt.OptionDescription
                    };
                    optViews.Add(optView);
                }
                result.ProductOptions = optViews;
            }

            return result;
        }

        public async Task<bool> CreateProductAsync(ProductDetailView productView)
        {
            if (productView == null)
                throw new RestException(HttpStatusCode.BadRequest, new { product = "Unable to get data " });

            Product p = new Product()
            {
                Id = 0,
                Name = productView.Name,
                Description = productView.Description,
                BasePrice = productView.BasePrice,
                MaxAllowedOrderQty = productView.MaxAllowedOrderQty,
            };
            _unitOfWork.ProductRepository.Add(p);

            if (productView.ProductOptions != null)
            {
                foreach (ProductOptionView optV in productView.ProductOptions)
                {
                    ProductOption opt = new ProductOption()
                    {
                        Product = p,
                        OptionDescription = optV.OptionDescription,
                        AdditionalCost = optV.AdditionalCost,
                        Id = 0
                    };
                    _unitOfWork.ProductOptionRepository.Add(opt);
                }
            }
            var success = await _unitOfWork.SaveAsync() > 0;
            if (success) return success; 
            else
                throw new RestException(HttpStatusCode.InternalServerError, new { order = "Unable to save order" }); ;
        }
    }
}
