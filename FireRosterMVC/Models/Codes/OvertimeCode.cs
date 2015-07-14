using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FireRosterMVC.Models
{
    public class OvertimeCode
    {
        [Key]
        public int ID { get; set; }

        [StringLength(6)]
        public string Code { get; set; }

        [StringLength(65)]
        public string Label { get; set; }

        [StringLength(10), Display(Name="Short Label")]
        public string ShortLabel { get; set; }

        public bool Active { get; set; }

        public OvertimeCode()
        {
            Active = true;
        }
    }
}