using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FireRosterMVC.Models
{
    public class EmergencyContact
    {
        [Key]
        public int ID { get; set; }
        [StringLength(50), Display(Name="First Name")]
        public string FirstName { get; set; }
        [StringLength(50), Display(Name="Last Name")]
        public string LastName { get; set; }
        [StringLength(50)]
        public string Relationship { get; set; }
        [StringLength(10), Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }
        public int Order { get; set; } // Sort by contact priority order.
        [Display(Name="Phone Type")]
        public virtual PhoneType PhoneType { get; set; }

        public virtual Staff Staff { get; set; }

        public EmergencyContact()
        {
            Order = 100;
        }

        public string DisplayName
        {
            get {
                return LastName + ", " + FirstName;
            }
            
        }
    }
}