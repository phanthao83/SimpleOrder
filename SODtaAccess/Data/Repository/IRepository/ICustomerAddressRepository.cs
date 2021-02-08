using SODtaModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SODtaAccess.Data.Repository.IRepository
{
    public interface ICustomerAddressRepository: IRepository<CustomerAddress>
    {
        Task<List<CustomerAddress>> GetByCustomerAsync(int customerId);
        void Update(CustomerAddress address); 
    }
}
