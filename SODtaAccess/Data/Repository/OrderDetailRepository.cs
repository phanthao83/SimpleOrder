using SODtaAccess.Data.Repository.IRepository;
using SODtaModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SODtaAccess.Data.Repository
{
    public class OrderDetailRepository : Repository<OrderDetail>, IOrderDetailRepository
    {
        private readonly ApplicationDbContext _db;
        public OrderDetailRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }


        public void Update(OrderDetail detail)
        {
            var updatedDetail= _db.OrderDetails.FirstOrDefault(s => s.Id == detail.Id);
            updatedDetail.Status = detail.Status; 

        }

        public async Task<List<OrderDetail>> GetByOrderAsync(int orderId)
        {
            return await GetAllAsync(filter: o => o.OrderId == orderId, includedProperties: "Product");
        }

       
    }
}
