using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FireRosterMVC.Models
{
    public class LocationGroup
    {
        [Key]
        public int ID { get; set; }
        [StringLength(30)]
        public string Label { get; set; }
    }
}