using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SODtaAccess.Data;
using SODtaAccess.Data.Repository;
using SODtaModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SODtaAccess.Initializer
{
    public class DBInitializer : IDBInitializer
    {
        private readonly ApplicationDbContext _db;
        private UnitOfWork _unitOfWork;

        public DBInitializer(ApplicationDbContext db)
        {
            _db = db;
            _unitOfWork = new UnitOfWork(_db);
        }


        public void InitializeAsync()
        {

            try
            {
                if (_db.Database.GetPendingMigrations().Count() > 0)
                {
                    _db.Database.Migrate();
                }
            }
            catch (Exception ex)
            {
                throw ex; 

            }
            CreateDummyDataAsync(); 


        }


        private void CreateDummyDataAsync()
        {
            List<Customer> customerLst = _unitOfWork.CustomerRepository.GetAll();
            if (customerLst == null || (customerLst != null && customerLst.Count == 0))
            {
                CreateCustomer(5);
                CreateProduct(5);
                CreateOrder(1);

            }
        }
        private void CreateCustomer(int number)
        {
            for (int i = 1; i < number + 1; i++)
            {
                Random random = new Random();
                int index = random.Next(0, 20);
                Customer c = new Customer()
                {

                    Name = "Customer " + index.ToString(),
                    Email = "Customer" + index.ToString() + "@gmail.com",
                    YearofBirth = 1970 + index

                };
                 _unitOfWork.CustomerRepository.Add(c);
                 _unitOfWork.Save();
            }

            //Create address
            List<Customer> customerLst =  _unitOfWork.CustomerRepository.GetAll();
            foreach (Customer c in customerLst)
            {
                CustomerAddress address = new CustomerAddress()
                {
                    ShopName = c.Name + " HQ",
                    AddressType = Address_Type.MainOffice,
                    Address1 = " HQ " + c.Id.ToString() + " ABC Street ",
                    City = "Benton",
                    State = "AR",
                    PhoneNumber = "9567063325",
                    ZipCode = "72112",
                    CustomerId = c.Id

                };
                 _unitOfWork.CustomerAddressRepository.Add(address);
                CustomerAddress address2 = new CustomerAddress()
                {
                    ShopName = c.Name + " Shop1",
                    AddressType = Address_Type.MainOffice,
                    Address1 = " HQ " + c.Id.ToString() + " XYZ Street ",
                    City = "Benton",
                    State = "AR",
                    PhoneNumber = "9567063325",
                    ZipCode = "72112",
                    CustomerId = c.Id

                };
                 _unitOfWork.CustomerAddressRepository.Add(address2);
                 _unitOfWork.Save();
            }
        }

        private void  CreateProduct(int number)
        {
            for (int i = 1; i < number + 1; i++)
            {
                Product p = new Product()
                {
                    Name = "Product " + i.ToString(),
                    MaxAllowedOrderQty = i + 1,
                    BasePrice = 1000 * i,
                };
                 _unitOfWork.ProductRepository.Add(p);
            }
             _unitOfWork.Save();

            //Production option
            IEnumerable<Product> productLst =  _unitOfWork.ProductRepository.GetAll();
            foreach (Product p in productLst)
            {
                ProductOption opt1 = new ProductOption()
                {
                    ProductId = p.Id,
                    OptionDescription = "Description for option 1 ",
                    AdditionalCost = 150,
                };
                _unitOfWork.ProductOptionRepository.Add(opt1);
                ProductOption opt2 = new ProductOption()
                {
                    ProductId = p.Id,
                    OptionDescription = "Description for option 1 ",
                    AdditionalCost = 171,
                };
                 _unitOfWork.ProductOptionRepository.Add(opt2);
                 _unitOfWork.Save();
            }
        }

        private void CreateOrder(int number)
        {
            List<Customer> customerLst =  _unitOfWork.CustomerRepository.GetAll();
            List<Product> productLst =  _unitOfWork.ProductRepository.GetAll();
            Random random = new Random();

            for (int i = 1; i < number + 1; i++)
            {
                int indexC = random.Next(0, customerLst.Count() - 1);
                Customer customer = customerLst.ToList()[indexC];
                //Create OrderHeader
                Order order = new Order()
                {
                    CustomerId = customer.Id,
                    CreateDate = DateTime.Now,
                    Total = 0,
                    Status = OrderStatus.New,
                    UpdatedDate = DateTime.Now
                };
                _unitOfWork.OrderRepository.Add(order);

                // Create OrderLine
                for (int j = 0; j < 2; j++)
                {
                    int indexP = random.Next(0, productLst.Count() - 1);
                    Product p = productLst.ToList()[indexP];

                    OrderDetail detail = new OrderDetail()
                    {
                        Order = order,
                        DiscountTotal = 0,
                        Product = p,
                        PromotionCode = "",
                        Status = OrderLineStatus.Active,
                        Quanity = 1,
                        UPrice = 0,
                        SubTotal = 0
                    };

                    //Add option
                    IEnumerable<ProductOption> optLst =  _unitOfWork.ProductOptionRepository.GetByProduct(p.Id);
                    ProductOption selectedOpt = optLst.ToList()[0];
                    OrderDetailOption opt = new OrderDetailOption()
                    {
                        AdditionalCost = selectedOpt.AdditionalCost,
                        OrderDetail = detail,
                        OptionId = selectedOpt.Id
                    };

                    //UPdate back to detail
                    detail.SubTotal = (p.BasePrice + opt.AdditionalCost) * detail.Quanity - detail.DiscountTotal;
                    order.Total += detail.SubTotal;
                    _unitOfWork.OrderDetailRepository.Add(detail);
                     _unitOfWork.OrderDetailOptionRepository.Add(opt);
                }

                _unitOfWork.Save();
            }


        }


    }



}
