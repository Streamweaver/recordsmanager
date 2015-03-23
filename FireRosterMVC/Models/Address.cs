using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FireRosterMVC.Models
{
    public class Address
    {
        [Key]
        public int ID { get; set; }
        [Display(Name = "Street 1")]
        public string Street1 { get; set; }
        [Display(Name = "Street 2")]
        public string Street2 { get; set; }
        [StringLength(75)]
        public string City { get; set; }
        [StringLength(2)]
        public string State { get; set; }
        [StringLength(20)]
        public string Zip { get; set; }
        [Display(Name = "Primary")]
        public bool Primary { get; set; }

        public virtual Staff Staff { get; set; }
    }
}