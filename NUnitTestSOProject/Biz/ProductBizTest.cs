using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using SimpleOrder.Controllers;
using SOBusinessControl.Domain;
using SOBusinessControl.Ultility;
using SODtaAccess.Data.Repository.IRepository;
using SODtaModel;
using SODtaModel.View;

namespace NUnitTestSOProject.Biz
{
    public class ProductBizTest
    {
        private Mock<IUnitOfWork> _mockUnit;
        private readonly int _existedProductId = 1; 
        [SetUp]
        public void Setup()
        {
            _mockUnit = new Mock<IUnitOfWork>();
            _ = _mockUnit.Setup(unit => unit.ProductRepository.GetAllAsync(null, null,null )).Returns(Task.FromResult(GetAllProduct()));
            _ = _mockUnit.Setup(unit => unit.ProductRepository.GetAsync(_existedProductId)).Returns(Task.FromResult(GetAllProduct()[0]));
            _ = _mockUnit.Setup(unit => unit.ProductOptionRepository.GetByProductAsync(_existedProductId)).Returns(Task.FromResult(GetProductOption(_existedProductId)));
        }

        [Test]
        public void GetDetail_Sucessfull()
        {
            ProductBiz productBiz = new ProductBiz(_mockUnit.Object);
            var result =  productBiz.GetDetailAsync(_existedProductId); 
            Assert.NotNull(result);
            Assert.IsTrue(result.Result.GetType() == typeof(ProductDetailView));

            ProductDetailView productView = (ProductDetailView)result.Result;
            Assert.IsTrue(productView.ProductOptions.Count == 2); 
        }

        [Test]
        public void GetDetail_Fail()
        {
            try
            {
                ProductBiz productBiz = new ProductBiz(_mockUnit.Object);
                var result = productBiz.GetDetailAsync(1000);
                Assert.Null(result.Result);
            }
            catch (Exception ex)
            {
                Assert.Pass();
            
            }
        }





        [Test]
        public void GetAll_Sucessfull()
        {
            ProductBiz productBiz = new ProductBiz(_mockUnit.Object);
            var  allPRoducts =   productBiz.GetAllAsync();
            Assert.NotNull(allPRoducts);
            Assert.IsTrue(allPRoducts.Result.Count == GetAllProduct().Count);
        }

        private List<Product> GetAllProduct() 
        {
            List<Product> products = new List<Product>();
            products.Add(new Product() { Id =1 , Name = "Product 1" , BasePrice = 10, MaxAllowedOrderQty = 2, Description ="Desc Product 1" });
            products.Add(new Product() { Id = 2, Name = "Product 2", BasePrice = 10, MaxAllowedOrderQty = 2, Description = "Desc Product 2" });
            products.Add(new Product() { Id = 3, Name = "Product 3", BasePrice = 10, MaxAllowedOrderQty = 2, Description = "Desc Product 3" });

            return products; 
        
        }

        private List<ProductOption> GetProductOption(int productId)
        {
            List<ProductOption> optProducts = new List<ProductOption>();
            optProducts.Add(new ProductOption() { Id = 1, AdditionalCost = 10, OptionDescription = "Addional 1 " , ProductId = productId});
            optProducts.Add(new ProductOption() { Id = 2, AdditionalCost = 10, OptionDescription = "Addional 2 ", ProductId = productId});

            return optProducts;

        }
    }
}
