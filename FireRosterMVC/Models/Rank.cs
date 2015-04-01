using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FireRosterMVC.Models
{
    public class Rank
    {
        [Key]
        public int ID { get; set; }
        [StringLength(3), Required]
        public string Code { get; set; }
        [StringLength(20)]
        public string Security { get; set; }
        public int Order { get; set; }

        public virtual ICollection<Staff> StaffList { get; set; }

        public Rank()
        {
            Order = 9999;
        }
    }
}