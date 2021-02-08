using SODtaAccess.Data.Repository.IRepository;
using SODtaModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SODtaAccess.Data.Repository
{
    public class CustomerAddressRepository : Repository<CustomerAddress>, ICustomerAddressRepository
    {
        private readonly ApplicationDbContext _db;
        public CustomerAddressRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public async Task<List<CustomerAddress>> GetByCustomerAsync(int customerId)
        {
            var result =  await GetAllAsync(filter: a => a.CustomerId == customerId);
            return result; 
        }

        public void Update(CustomerAddress address)
        {
            var updatedAddress = _db.CustomerAddress.FirstOrDefault(s => s.Id == address.Id);
            updatedAddress.Address1 = address.Address1;
            updatedAddress.Address2 = address.Address2;
            updatedAddress.AddressType = address.AddressType;
            updatedAddress.City = address.City;
            updatedAddress.PhoneNumber = address.PhoneNumber;
            updatedAddress.State = address.State;
            updatedAddress.ShopName = address.ShopName; 
            
        }
    }
}
