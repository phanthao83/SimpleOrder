using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SODtaModel
{
    public class Customer
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Name")]
        [MaxLength(100)]
        public string Name { get; set; }

        [Display(Name = "Email")]
        [MaxLength(100)]
        public string Email { get; set; }

        [Display(Name = "Year of Birth")]
        public int YearofBirth { get; set; }

    }
}
