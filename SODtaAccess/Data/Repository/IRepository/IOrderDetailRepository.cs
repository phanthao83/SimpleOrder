using SODtaModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SODtaAccess.Data.Repository.IRepository
{
    public interface IOrderDetailRepository : IRepository<OrderDetail>
    {
        Task<List<OrderDetail>> GetByOrderAsync(int orderId);
        void Update(OrderDetail detail); 
    }
}
