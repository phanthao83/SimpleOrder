using SODtaModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace SODtaAccess.Data.Repository.IRepository
{
    public interface IOrderRepository : IRepository<Order>
    {
        void Save(Order order); 
    }
}
