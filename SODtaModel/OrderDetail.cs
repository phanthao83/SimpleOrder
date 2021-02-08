using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SODtaModel
{
    public static class OrderLineStatus
    {
        public const string Active = "A";
        public const string Cancelled = "C";
        public const string PrePurchased = "P";

    }
    public class OrderDetail
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Order")]
        public int OrderId { get; set; }
        [ForeignKey("OrderId")]
        public Order Order { get; set; }


        [Display(Name = "Product")]
        public int ProductiId { get; set; }
        [ForeignKey("ProductiId")]
        public Product Product { get; set; }

        [Display(Name = "Quantity")]
        public int Quanity { get; set; }

        
        [Display(Name = "Unit Price")]
        public double UPrice { get; set; }


        [Display(Name = "Discount Total")]
        public double DiscountTotal { get; set; }


        [Display(Name = "SubTotal")]
        public double SubTotal { get; set; }

        [Display(Name = "Promotion Code")]
        [MaxLength(10)]
        public string PromotionCode { get; set; }

        [Display(Name = "Order Detail Status")]
        [MaxLength(1)]
        public string Status { get; set; }


    }
}
