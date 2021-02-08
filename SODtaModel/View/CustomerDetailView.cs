using System;
using System.Collections.Generic;
using System.Text;

namespace SODtaModel.View
{
    public class CustomerDetailView
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public int YearofBirth { get; set; }

        public List<CustomerAddressView> AddressLst { get; set; }
       
    }

    public class CustomerAddressView
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public string ShopName { get; set; }
        public string AddressType { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public string PhoneNumber { get; set; }
    }
}
