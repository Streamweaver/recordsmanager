namespace FireRosterMVC.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("tblEmployeePhoto")]
    public partial class tblEmployeePhoto
    {
        [Key]
        [StringLength(50)]
        public string OracleHrID { get; set; }

        [Column(TypeName = "image")]
        public byte[] photo { get; set; }
    }
}
