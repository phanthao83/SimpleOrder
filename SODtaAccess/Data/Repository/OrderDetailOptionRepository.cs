using Dapper;
using SODtaAccess.Data.Repository.IRepository;
using SODtaModel;
using SODtaModel.View;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SODtaAccess.Data.Repository
{
    public class OrderDetailOptionRepository : Repository<OrderDetailOption>,  IOrderDetailOptionRepository
    {
        private readonly ApplicationDbContext _db;
        public OrderDetailOptionRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public async Task<List<OrderLineOptionView>> GetByOrderAsync(int orderId)
        {
            StoreProcedureRepository spRepository = new StoreProcedureRepository(_db);
            var p = new DynamicParameters();
            p.Add("@orderId", orderId);
            
            var result =  await spRepository.ReturnList<OrderLineOptionView>(StoreProcedureList.GET_ALL_DETAIL_OPTION_OF_ORDER, p);

            return result; 
        }

        public async Task<List<OrderDetailOption>> GetByOrderDetailAsync(int orderDetailId)
        {
            return await GetAllAsync(filter: o => o.OrderDetailID == orderDetailId, includedProperties: "ProductOption");
        }
    }
}
