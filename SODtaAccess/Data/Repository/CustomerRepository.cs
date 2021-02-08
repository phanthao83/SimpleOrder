using SODtaAccess.Data.Repository.IRepository;
using SODtaModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SODtaAccess.Data.Repository
{
    public class CustomerRepository : Repository<Customer>, ICustomerRepository
    {
        private readonly ApplicationDbContext _db;
        public CustomerRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Customer customer)
        {
            var updatedCustomer = _db.Customers.FirstOrDefault(s => s.Id == customer.Id);
            updatedCustomer.Name = customer.Name;
            updatedCustomer.YearofBirth = customer.YearofBirth;
                       

        }
    }
}
