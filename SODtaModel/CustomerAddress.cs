using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SODtaModel
{
    public static class Address_Type
    {
        public const string MainOffice = "M";
        public const string Shop = "S";
        public const string ClosedShop = "I";
        
    }
    public class CustomerAddress
    {
        [Key]
        public int Id { get; set; }



        [Display(Name = "Customer")]
        public int CustomerId { get; set; }

        [ForeignKey("CustomerId")]
        public Customer Customer { get; set; }


        [Required]
        [Display(Name = "Shop Name")]
        [MaxLength(100)]
        public string ShopName { get; set; }


        [Required]
        [Display(Name = "Address Type")]
        [MaxLength(1)]
        public string AddressType { get; set; }



        [Display(Name = "Create Date")]
        public DateTime CreateDate { get; set; }


        [Required]
        [Display(Name = "Address1")]
        [MaxLength(50)]
        public string Address1 { get; set; }

        [Display(Name = "Address2")]
        [MaxLength(50)]
        public string Address2 { get; set; }


        [Required]
        [Display(Name = "City")]
        [MaxLength(50)]
        public string City { get; set; }


        [Required]
        [Display(Name = "State")]
        [MaxLength(2)]
        public string State { get; set; }


        [Required]
        [Display(Name = "ZipCode")]
        [MaxLength(5)]
        public string ZipCode { get; set; }


        [Display(Name = "Contact Number")]
        [MaxLength(10)]
        public string PhoneNumber { get; set; }


    }
}
