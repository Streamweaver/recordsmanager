namespace FireRosterMVC.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("tblEmergencyContact")]
    public partial class tblEmergencyContact
    {
        [Key]
        public int EmergencyContactID { get; set; }

        public int? EmployeeID { get; set; }

        public DateTime? DateCreated { get; set; }

        public DateTime? LastUpdateDate { get; set; }

        [StringLength(250)]
        public string LastUpdateBy { get; set; }

        public DateTime? DateRemoved { get; set; }

        public bool? IsRemoved { get; set; }

        [StringLength(50)]
        public string FirstName { get; set; }

        [StringLength(50)]
        public string LastName { get; set; }

        [StringLength(50)]
        public string Relationship { get; set; }

        [StringLength(10)]
        public string PhoneNumber { get; set; }

        [StringLength(50)]
        public string PhoneType { get; set; }

        public int? ContactOrder { get; set; }

        public virtual tblEmployee tblEmployee { get; set; }
    }
}
