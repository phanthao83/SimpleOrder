using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SODtaModel
{
      public static class OrderStatus
       {
           public const string New = "N";
           public const string Pending = "P";
           public const string In_Process = "A";
           public const string Fullfilled = "F";
           public const string Paid = "D";
           public const string Completed = "C";

       }
    
    public class Order
    {
        [Key]
        public int Id { get; set; }

       
        [Display(Name = "Total")]
        public double Total { get; set; }

        [Display(Name = "Customer")]
        public int CustomerId { get; set; }

        [ForeignKey("CustomerId")]
        public Customer Customer { get; set; }


        [Required]
        [Display(Name = "Order Status")]
        [MaxLength(1)]
        public string Status { get; set; }

        [Display(Name = "Create Date")]
        public DateTime CreateDate { get; set; }

        [Display(Name = "Last Updated Date")]
        public DateTime UpdatedDate { get; set; }

    }
}
