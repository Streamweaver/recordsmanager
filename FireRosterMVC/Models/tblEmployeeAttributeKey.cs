namespace FireRosterMVC.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("tblEmployeeAttributeKey")]
    public partial class tblEmployeeAttributeKey
    {
        public tblEmployeeAttributeKey()
        {
            tblEmployeeAttributes = new HashSet<tblEmployeeAttribute>();
        }

        [Key]
        public int EmployeeAttributeKeyID { get; set; }

        [StringLength(50)]
        public string KeyName { get; set; }

        public virtual ICollection<tblEmployeeAttribute> tblEmployeeAttributes { get; set; }
    }
}
