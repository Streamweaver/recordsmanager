using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FireRosterMVC.Models.Codes
{
    public class CareerDevelopmentLevel
    {
        [Key]
        public int ID { get; set; }

        [StringLength(10), Required]
        public string Label { get; set; }
    }
}