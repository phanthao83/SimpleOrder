using SODtaAccess.Data.Repository.IRepository;
using SODtaModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SODtaAccess.Data.Repository
{
    public class ProductOptionRepository: Repository<ProductOption>, IProductOptionRepository
    {
        private readonly ApplicationDbContext _db;
        public ProductOptionRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public async Task<List<ProductOption>> GetByProductAsync(int productId)
        {
            return await GetAllAsync(filter: o => o.ProductId == productId);  // _db.ProductOptions.Where(s => s.ProductId == productId);
        }
        public List<ProductOption> GetByProduct(int productId)
        {
            return  GetAll(filter: o => o.ProductId == productId);  // _db.ProductOptions.Where(s => s.ProductId == productId);
        }


        public void Update(ProductOption option)
        {
            var updatedOption = _db.ProductOptions.FirstOrDefault(s => s.Id == option.Id);
            updatedOption.OptionDescription = option.OptionDescription;
            updatedOption.AdditionalCost = option.AdditionalCost; 

        }
    }
}
