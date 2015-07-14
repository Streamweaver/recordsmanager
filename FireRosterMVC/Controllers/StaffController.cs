using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FireRosterMVC.Models;
using PagedList;
using System.Linq.Expressions;
using FireRosterMVC.Enums;
using System.Data.Entity.Infrastructure;
using System.Text.RegularExpressions;
using FireRosterMVC.ViewModels;

namespace FireRosterMVC.Controllers
{
    [Authorize(Roles = "Admin, Manager, Payroll, Training")]
    public class StaffController : Controller
    {
        private FireRosterDB db;

        public StaffController()
        {
            db = new FireRosterDB();
        }

        public StaffController(FireRosterDB context)
        {
            db = context;
        }

        // GET: Staff
        public ActionResult Index(string searchString, string statusFilter, int? page)
        {
            int pageSize = 20;
            int pageNumber = (page ?? 1);
            string activeStatus = String.IsNullOrEmpty(statusFilter) ? "active" : statusFilter.ToLower();
            ViewBag.CurrentStatusFilter = activeStatus;

            ViewBag.StatusFilter = Enum.GetValues(typeof(Activestatus)).Cast<Activestatus>().Select(
                v => new SelectListItem
                {
                    Text = v.ToString(),
                    Value = v.ToString().ToLower(),
                    Selected = v.ToString().ToLower() == activeStatus
                }
                );

            var staff = from s in db.StaffList
                            .Where(s => s.Deleted == false)
                            .OrderBy(s => s.LastName)
                        select s;

            if (!String.IsNullOrEmpty(searchString))
            {
                ViewBag.CurrentFilter = searchString;
                staff = staff.Where(s => s.LastName.Contains(searchString)
                    || s.FirstName.Contains(searchString) || s.OracleHrID.Equals(searchString));
            }

            // Found details of this in sored database function dbo.udf_isEmployeeActive
            // Do not use view or function, it does not determine inactive status correctly
            switch (activeStatus)
            {  
                case "inactive":
                    staff = staff.Where(s => s.TerminationDate < DateTime.Now);
                    break;
                case "all":
                    break;
                default: // defaults to active
                    staff = staff.Where(s => s.EmploymentDate != null && s.EmploymentDate <= DateTime.Now);
                    staff = staff.Where(s => s.TerminationDate == null || s.TerminationDate > DateTime.Now);
                    break;
            }
            
            return View(staff.ToPagedList(pageNumber, pageSize));
        }

        // POST: Staff/AsyncLookUp
        public JsonResult AsyncLookUp(string term)
        {
            string raw = Regex.Replace(term, @"[^\w\s]", string.Empty);
            string[] terms = raw.Split(' ');

            var result = new List<KeyValuePair<string, string>>();

            var stafflist = from s in db.StaffList
                                select s;
            
            foreach (string word in terms) 
            {
                stafflist = stafflist.Where(
                    s => s.LastName.Contains(word) || 
                    s.FirstName.Contains(word) || 
                    s.OracleHrID.Equals(word));
            }

            stafflist = stafflist.Take(20);
            
            foreach (var staff in stafflist) {
                result.Add(new KeyValuePair<string, string>(staff.ID.ToString(), staff.DisplayName + " - " + staff.OracleHrID));
            };

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        // GET: Staff/Gallery
        public ActionResult Gallery(int? page)
        {
            int pageSize = 20;
            int pageNumber = (page ?? 1);

            var staff = from s in db.StaffList
                            .Where(s => s.Deleted == false)
                            .Where(s => s.Photo != null)
                            .Where(s => s.EmploymentDate != null && s.EmploymentDate <= DateTime.Now)
                            .Where(s => s.TerminationDate == null || s.TerminationDate > DateTime.Now)
                            .OrderBy(s => s.LastName)
                        select s;

            return View(staff.ToPagedList(pageNumber, pageSize));
        }

        // GET: Staff/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Staff Staff = await db.StaffList.FindAsync(id);
            if (Staff == null)
            {
                return HttpNotFound();
            }
            return View(Staff);
        }

        // GET: Staff/Create
        public ActionResult Create()
        {
            PopulateGenderDropDownList();
            PopulateRaceDropDownList();
            PopulateCDLDropDownList();
            return View();
        }

        // POST: Staff/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ID,SSN,NamePrefix,FirstName,MiddleName,LastName,NameSuffix,DateOfBirth,RankDate,CareerDevelopmentDate,EmploymentDate,TerminationDate,MilitaryLeave,RosterRank,HenricoUserID,OracleHrID,BadgeNumber,Gender_ID,Race_ID,CDL_ID")] Staff Staff)
        {
            if (ModelState.IsValid)
            {
                Staff.CreatedOn = DateTime.Now;
                db.StaffList.Add(Staff);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(Staff);
        }

        // GET: Staff/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Staff Staff = await db.StaffList.FindAsync(id);
            if (Staff == null)
            {
                return HttpNotFound();
            }
            PopulateGenderDropDownList(Staff.Gender_ID);
            PopulateRaceDropDownList(Staff.Race_ID);
            PopulateCDLDropDownList(Staff.CDL_ID);
            return View(Staff);
        }

        // POST: Staff/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Staff staff)
        {
            Staff f = db.StaffList.Find(staff.ID);
            f.SSN = staff.SSN;
            f.NamePrefix = staff.NamePrefix;
            f.FirstName = staff.FirstName;
            f.MiddleName = staff.MiddleName;
            f.LastName = staff.LastName;
            f.NameSuffix = staff.NameSuffix;
            f.DateOfBirth = staff.DateOfBirth;
            f.RankDate = staff.RankDate;
            f.CareerDevelopmentDate = staff.CareerDevelopmentDate;
            f.EmploymentDate = staff.EmploymentDate;
            f.TerminationDate = staff.TerminationDate;
            f.MilitaryLeave = staff.MilitaryLeave;
            f.RosterRank = staff.RosterRank;
            f.HenricoUserID = staff.HenricoUserID;
            f.OracleHrID = staff.OracleHrID;
            f.BadgeNumber = staff.BadgeNumber;
            f.Gender_ID = staff.Gender_ID;
            f.Race_ID = staff.Race_ID;
            f.CDL_ID = staff.CDL_ID;

            if (ModelState.IsValid)
            {
                f.UpdatedOn = DateTime.Now;
                db.Entry(f).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Details", "Staff", new { id = f.ID });
            }
            return View(f);
        }

        // GET: Staff/EditSkills/5
        public ActionResult EditSkills(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Staff staff = db.StaffList
                            .Include(i => i.Skills)
                            .Where(i => i.ID == id)
                            .Single();
            if (staff == null)
            {
                return HttpNotFound();
            }
            PopulateAssignedSkillData(staff);
            return View(staff);
        }

        // POST: Staff/EditSkills/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditSkills(int? id, string[] selectedSkills)
        {
            Staff s = db.StaffList.Find(id);
            UpdateStaffSkills(selectedSkills, s);

            if (ModelState.IsValid)
            {
                s.UpdatedOn = DateTime.Now;
                db.Entry(s).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Details", "Staff", new { id = s.ID });
            }
            return View(s);
        }

        // GET: Staff/Delete/5
        [Authorize(Roles="Admin")]
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Staff Staff = await db.StaffList.FindAsync(id);
            if (Staff == null)
            {
                return HttpNotFound();
            }
            return View(Staff);
        }

        // POST: Staff/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Staff Staff = await db.StaffList.FindAsync(id);
            db.StaffList.Remove(Staff);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        [Authorize(Roles = "Admin")]
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private void PopulateGenderDropDownList(object selectedGender = null)
        {
            var genderQuery = from g in db.Gender
                              orderby g.Label
                              select g;
            ViewBag.Gender_ID = new SelectList(genderQuery, "ID", "Label", selectedGender);
        }

        private void PopulateRaceDropDownList(object selectedRace = null)
        {
            var raceQuery = from r in db.Race
                            orderby r.Label
                            select r;
            ViewBag.Race_ID = new SelectList(raceQuery, "ID", "Label", selectedRace);
        }

        private void PopulateCDLDropDownList(object selectedCDL = null)
        {
            var cdlQuery = from c in db.CareerDevelopmentLevels
                           orderby c.Label
                           select c;
            ViewBag.CDL_ID = new SelectList(cdlQuery, "ID", "Label", selectedCDL);
        }

        private void PopulateStatusDropDownList(object selectedStatus = null)
        {
            ViewBag.StatusFilter = Enum.GetValues(typeof(Activestatus)).Cast<Activestatus>().Select(
                v => new SelectListItem
                {
                    Text = v.ToString(),
                    Value = v.ToString().ToLower(),
                    Selected = (string)selectedStatus == v.ToString().ToLower()
                }
                );
        }

        private void PopulateAssignedSkillData(Staff staff)
        {
            var allSkills = db.Skills;
            var staffSkills = new HashSet<int>(staff.Skills.Select(s => s.ID));
            var viewModel = new List<AssignedSkillData>();
            foreach (var skill in allSkills)
            {
                viewModel.Add(new AssignedSkillData
                {
                    SkillID = skill.ID,
                    Title = skill.Name,
                    Assigned = staffSkills.Contains(skill.ID)
                });
            }
            ViewBag.Skills = viewModel;
        }

        private void UpdateStaffSkills(string[] selectedSkills, Staff staff)
        {
            if (selectedSkills == null)
            {
                staff.Skills = new List<Skill>();
            }
            var selectedSkillsHS = new HashSet<string>(selectedSkills);
            var staffSkills = new HashSet<int>(staff.Skills.Select(s => s.ID));
            foreach (var skill in db.Skills)
            {
                if (selectedSkillsHS.Contains(skill.ID.ToString()))
                {
                    if (!staffSkills.Contains(skill.ID))
                    {
                        staff.Skills.Add(skill);
                    }
                }
                else
                {
                    if (staffSkills.Contains(skill.ID))
                    {
                        staff.Skills.Remove(skill);
                    }
                }
            }
        }

    }


}
