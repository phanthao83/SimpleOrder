using SODtaModel;
using System;
using System.Collections.Generic;
using System.Text;
using SODtaAccess.Data.Repository.IRepository;
using System.Linq;

namespace SODtaAccess.Data.Repository
{
    public class OrderRepository : Repository<Order>, IOrderRepository
    {
        
        private readonly ApplicationDbContext _db;

        public OrderRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Save(Order order)
        {
            var updatedOrder = _db.Orders.FirstOrDefault(s => s.Id == order.Id);
            updatedOrder.UpdatedDate = DateTime.Now;
            updatedOrder.Status = order.Status;
            updatedOrder.Total = order.Total; 
            
        }
    }
}
