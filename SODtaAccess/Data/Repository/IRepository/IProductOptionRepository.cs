using SODtaModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SODtaAccess.Data.Repository.IRepository
{
    public interface IProductOptionRepository : IRepository<ProductOption>
    {
        Task<List<ProductOption>>  GetByProductAsync(int productId);
        List<ProductOption>  GetByProduct(int productId);
        void Update(ProductOption option); 
    }
}
