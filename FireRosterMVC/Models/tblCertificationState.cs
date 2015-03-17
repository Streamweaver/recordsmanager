namespace FireRosterMVC.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("tblCertificationState")]
    public partial class tblCertificationState
    {
        [Key]
        public int StateCertificationID { get; set; }

        public int? EmployeeID { get; set; }

        public int? OracleHrID { get; set; }

        public DateTime? DateCreated { get; set; }

        public DateTime? DateDeleted { get; set; }

        public bool? IsDeleted { get; set; }

        public DateTime? CertificationStarts { get; set; }

        public DateTime? CertificationExpires { get; set; }

        [StringLength(50)]
        public string CertificationNumber { get; set; }

        [StringLength(50)]
        public string CertificationLevel { get; set; }

        [StringLength(50)]
        public string UpdateBy { get; set; }
    }
}
