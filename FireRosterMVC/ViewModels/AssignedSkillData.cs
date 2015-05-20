using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FireRosterMVC.ViewModels
{
    public class AssignedSkillData
    {
        public int SkillID { get; set; }
        public string Title { get; set; }
        public bool Assigned { get; set; }
    }
}