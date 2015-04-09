using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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

        public int? Type_ID { get; set; }
        [ForeignKey("Type_ID")]
        public virtual PhoneType Type { get; set; }
        public virtual Staff Staff { get; set; }

        public string DisplayNumber
        {
            get 
            {
               if (String.IsNullOrWhiteSpace(Number))
               {
                   return "Number ID " + ID;
               }
               if (Number.Length == 10)
               {
                   return String.Format("{0: (###) ###-####}", Convert.ToInt64(Number));
               }
               else
               {
                   return Number;
               }
            }
        }
    }
}