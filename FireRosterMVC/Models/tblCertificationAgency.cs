namespace FireRosterMVC.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("tblCertificationAgency")]
    public partial class tblCertificationAgency
    {
        [Key]
        public int AgencyCertificationID { get; set; }

        public int? StateCertificationID { get; set; }

        public int? EmployeeID { get; set; }

        public DateTime? DateCreated { get; set; }

        public DateTime? DateDeleted { get; set; }

        public bool? IsDeleted { get; set; }

        [StringLength(50)]
        public string CertificationNumber { get; set; }

        [StringLength(50)]
        public string CertificationLevel { get; set; }

        public DateTime? EffectiveStartDate { get; set; }

        public DateTime? EffectiveEndDate { get; set; }

        [StringLength(50)]
        public string UpdateBy { get; set; }
    }
}
