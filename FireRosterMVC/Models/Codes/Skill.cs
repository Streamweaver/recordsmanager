using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FireRosterMVC.Models
{
    public class Skill
    {
        [Key]
        public int ID { get; set; }
        [StringLength(50)]
        public string Name { get; set; }
        [StringLength(100)]
        public string ImageLink { get; set; }
        [StringLength(10)]
        public string Abbreviation { get; set; }

        public virtual ICollection<Staff> Staff { get; set; }
    }
}