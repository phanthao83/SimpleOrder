using SODtaModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace SODtaAccess.Data.Repository.IRepository
{
    public interface IProductRepository : IRepository<Product>
    {
        void Update(Product product); 
    }
}
