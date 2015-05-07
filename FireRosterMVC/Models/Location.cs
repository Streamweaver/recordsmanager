using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace FireRosterMVC.Models
{
    public class Location
    {
        [Key]
        public int ID { get; set; }
        [StringLength(5)]
        public string Code { get; set; } // TODO make this unique
        [Display(Name = "Minimum Complement")]
        public int MinimumComplement { get; set; }
        [StringLength(100)]
        public string Name { get; set; }
        [StringLength(20)]
        public string Phone { get; set; }
        [StringLength(100), Display(Name = "Street 1")]
        public string Street1 { get; set; }
        [StringLength(100), Display(Name = "Street 2")]
        public string Street2 { get; set; }
        [StringLength(50)]
        public string City { get; set; }
        [StringLength(2)]
        public string State { get; set; }
        [StringLength(15)]
        public string Zip { get; set; }
        public int Order { get; set; }

        public virtual ICollection<Position> Positions { get; set; }

        public int? Group_ID { get; set; }
        [ForeignKey("Group_ID")]
        public virtual LocationGroup Group { get; set; }

        public Location()
        {
            Order = 9999;
        }

        public string DisplayPhone
        {
            get
            {
                if (string.IsNullOrWhiteSpace(Phone))
                {
                    return "";
                }
                if (Phone.Length == 10)
                {
                    return String.Format("{0: (###) ###-####}", Convert.ToInt64(Phone));
                }
                if (Phone.Length == 7)
                {
                    return String.Format("{0: ###-####}", Convert.ToInt64(Phone));
                }
                return Phone;
            }
        }
    }
}