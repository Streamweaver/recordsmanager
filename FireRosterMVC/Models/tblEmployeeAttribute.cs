namespace FireRosterMVC.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("tblEmployeeAttribute")]
    public partial class tblEmployeeAttribute
    {
        [Key]
        public int EmployeeAttributeID { get; set; }

        public int? EmployeeID { get; set; }

        public int? EmployeeAttributeKeyID { get; set; }

        [StringLength(50)]
        public string EmployeeAttributeValue { get; set; }

        public virtual tblEmployee tblEmployee { get; set; }

        public virtual tblEmployeeAttributeKey tblEmployeeAttributeKey { get; set; }
    }
}
