using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SODtaAccess.Data.Repository.IRepository
{
    public interface IUnitOfWork : IDisposable
    {
        IProductOptionRepository ProductOptionRepository { get; }
        IOrderDetailOptionRepository OrderDetailOptionRepository { get; }
        IOrderDetailRepository OrderDetailRepository { get; }
        IOrderRepository OrderRepository { get;  }
        IProductRepository ProductRepository { get;}
        ICustomerRepository CustomerRepository { get;}
        ICustomerAddressRepository CustomerAddressRepository { get;  }
        Task<int> SaveAsync();
        void Save(); 
    }
}
