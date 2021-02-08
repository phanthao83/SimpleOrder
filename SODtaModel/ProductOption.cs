using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SODtaModel
{
    public class ProductOption
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Description")]
        [MaxLength(200)]
        public string OptionDescription { get; set; }

        [Display(Name = "Product")]
        public int ProductId { get; set; }

        [ForeignKey("ProductId")]
        public Product Product { get; set; }


        [Display(Name = "Additional Cost")]
        public double AdditionalCost { get; set; }
    }
}
