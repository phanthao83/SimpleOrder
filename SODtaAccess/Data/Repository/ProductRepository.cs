using SODtaAccess.Data.Repository.IRepository;
using SODtaModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SODtaAccess.Data.Repository
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private readonly ApplicationDbContext _db;
        public ProductRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
        public void Update(Product product)
        {
            var updatedProduct= _db.Products.FirstOrDefault(s => s.Id == product.Id);
            updatedProduct.Description = product.Description;
            updatedProduct.BasePrice = product.BasePrice;
            updatedProduct.MaxAllowedOrderQty = product.MaxAllowedOrderQty;
            updatedProduct.Name = product.Name; 
            
        }
    }
}
