using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace FireRosterMVC.Models
{
    public class OvertimeAvailability
    {
        [Key]
        public int ID { get; set; }

        public int? Staff_ID { get; set; }
        [ForeignKey("Staff_ID"), Required]
        public virtual Staff Staff { get; set; }

        [Required]
        public DateTime Start { get; set; }

        public DateTime End { get; set; }
    }
}