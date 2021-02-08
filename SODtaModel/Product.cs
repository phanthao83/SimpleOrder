using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SODtaModel
{
    public class Product
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Product Name")]
        [MaxLength(100)]
        public string Name { get; set; }

        [Display(Name = "Description")]
        [MaxLength(200)]
        public string Description { get; set; }


        [Display(Name = "Max Allowed Order Quantity")]
        public int MaxAllowedOrderQty { get; set; }

        //Price not include add in Options
        [Display(Name = "Base Price")]
        public double BasePrice { get; set; }
    }
}
