using SODtaAccess.Data.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SODtaAccess.Data.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        public IProductOptionRepository ProductOptionRepository { get; private set; }
        public IOrderDetailOptionRepository OrderDetailOptionRepository { get; private set; }
        public IOrderDetailRepository OrderDetailRepository { get; private set; }
        public IOrderRepository OrderRepository { get; private set; }
        public IProductRepository ProductRepository { get; private set; }
        public ICustomerRepository CustomerRepository { get; private set; }
        public ICustomerAddressRepository CustomerAddressRepository { get; private set; }

        private ApplicationDbContext _db;
        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
            ProductOptionRepository = new ProductOptionRepository(_db);
            OrderDetailOptionRepository = new OrderDetailOptionRepository(_db);
            OrderDetailRepository = new OrderDetailRepository(_db);
            OrderRepository = new OrderRepository(_db);
            ProductRepository = new ProductRepository(_db);
            CustomerRepository = new CustomerRepository(_db);
            CustomerAddressRepository = new CustomerAddressRepository(_db);
       
        }

        public void Dispose()
        {
            _db.Dispose(); 
        }

        public async Task<int> SaveAsync()
        {
            var success = await _db.SaveChangesAsync();
            return success;
        }

        public void Save()
        {
            _db.SaveChanges(); 
        }

       
    }
}
