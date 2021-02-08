using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using SOBusinessControl.Ultility;
using SODtaAccess.Data.Repository.IRepository;
using SODtaModel;
using SODtaModel.View;
using System.Threading.Tasks;

namespace SOBusinessControl.Domain
{
    public class CustomerBiz
    {
        private readonly IUnitOfWork _unitOfWork;
        public CustomerBiz(IUnitOfWork unitofWork)
        {
            _unitOfWork = unitofWork; 
        }
        public async Task<List<SODtaModel.Customer>> GetAllAsync()
        {
            var result = await _unitOfWork.CustomerRepository.GetAllAsync();
            return result;
        }
        public async Task<CustomerDetailView> GetDetailAsync( int customerId)
        {
            CustomerDetailView result = new CustomerDetailView();
            var customerInfo = await _unitOfWork.CustomerRepository.GetAsync(customerId);
            if (customerInfo == null)
                throw new RestException(HttpStatusCode.NotFound, new { customer = "Not found" });

            result.Id = customerInfo.Id;
            result.Name = customerInfo.Name;
            result.Email = customerInfo.Name;
            result.AddressLst = new List<CustomerAddressView>();
            var addressLst = await _unitOfWork.CustomerAddressRepository.GetByCustomerAsync(customerInfo.Id);
            foreach (var address in addressLst)
            {

                if (address.AddressType != Address_Type.ClosedShop)
                {
                    CustomerAddressView a = new CustomerAddressView()
                    {
                        Id = address.Id,
                        AddressType = CodeTranslation.ToAddressTypeDescription(address.AddressType),
                        Address1 = address.Address1,
                        Address2 = address.Address2,
                        ShopName = address.ShopName,
                        CustomerId = address.CustomerId,
                        City = address.City,
                        State = address.State,
                        ZipCode = address.ZipCode,
                    };
                    
                    result.AddressLst.Add(a);
                }
            }

            return result;
        }

        public async Task<bool> CreateCustomerAsync(CustomerDetailView customerDetail)
        {
            if (customerDetail == null)
                throw new RestException(HttpStatusCode.BadRequest, new { customer = "Wrong data posted" });

            //Add Customer Info
            var customer = new SODtaModel.Customer()
            {
                Id = 0,
                Name = customerDetail.Name,
                Email = customerDetail.Email,
                YearofBirth = customerDetail.YearofBirth,
            };
            await _unitOfWork.CustomerRepository.AddAsync(customer);

            // Add Customer Address List
            if (customerDetail.AddressLst != null && customerDetail.AddressLst.Count > 0)
            {
                foreach (var address in customerDetail.AddressLst)
                {
                    CustomerAddress a = new CustomerAddress()
                    {
                        Customer = customer,
                        AddressType = CodeTranslation.ToAddressTypeCode(address.AddressType),
                        Address1 = address.Address1,
                        Address2 = address.Address2,
                        City = address.City,
                        State = address.State,
                        ZipCode = address.ZipCode,
                        PhoneNumber = address.PhoneNumber,
                        ShopName = address.ShopName,
                        Id = 0
                    }; 

                await _unitOfWork.CustomerAddressRepository.AddAsync(a);
                }
            }

            //Save
            var success = await _unitOfWork.SaveAsync() > 0;
            return success; 
        }
    }
}
