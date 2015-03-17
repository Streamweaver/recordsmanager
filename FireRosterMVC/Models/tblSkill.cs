namespace FireRosterMVC.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("tblSkill")]
    public partial class tblSkill
    {
        public tblSkill()
        {
            tblEmployees = new HashSet<tblEmployee>();
        }

        [Key]
        public int SkillID { get; set; }

        [StringLength(50)]
        public string SkillName { get; set; }

        [StringLength(250)]
        public string SkillImageURL { get; set; }

        [StringLength(10)]
        public string SkillAbbreviation { get; set; }

        public virtual ICollection<tblEmployee> tblEmployees { get; set; }
    }
}
