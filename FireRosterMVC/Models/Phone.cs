using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FireRosterMVC.Models
{
    public class Phone
    {
        [Key]
        public int ID { get; set; }
        public string Number { get; set; }
        public bool Primary { get; set; }

        public virtual PhoneType Type { get; set; }
        public virtual Staff Staff { get; set; }
    }
}