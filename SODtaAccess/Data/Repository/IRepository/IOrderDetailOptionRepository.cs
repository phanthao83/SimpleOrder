using SODtaModel;
using SODtaModel.View;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SODtaAccess.Data.Repository.IRepository
{
    public interface IOrderDetailOptionRepository : IRepository<OrderDetailOption>
    {
        Task<List<OrderDetailOption>> GetByOrderDetailAsync(int orderDetailId);
        Task<List<OrderLineOptionView>> GetByOrderAsync(int orderId);
    }
}
