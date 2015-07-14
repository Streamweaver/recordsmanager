using FireRosterMVC.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FireRosterMVC.Models
{
    public class Audit
    {
        [Key]
        public int ID { get; set; }
        public string ControllerClass { get; set; }
        public string RecordID { get; set; }
        public RecordAction Action { get; set; }
        public string Details { get; set; }
        public DateTime On { get; set; }
        public string UserName { get; set; }
    }
}