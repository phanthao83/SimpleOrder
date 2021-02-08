using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SODtaModel
{
    public class OrderDetailOption
    {
        [Key]
        public int Id { get; set; }


        [Display(Name = "OrderDetail")]
        public int OrderDetailID { get; set; }
        [ForeignKey("OrderDetailID")]
        public OrderDetail OrderDetail { get; set; }


        [Display(Name = "Product Option")]
        public int OptionId { get; set; }
        [ForeignKey("OptionId")]
        public ProductOption ProductOption { get; set; }

        [Display(Name = "Additional Cost")]
        public double AdditionalCost { get; set; }
    }
}
