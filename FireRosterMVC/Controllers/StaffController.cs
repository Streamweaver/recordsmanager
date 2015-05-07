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

namespace FireRosterMVC.Controllers
{
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

        // GET: Staff/Delete/5
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
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Staff Staff = await db.StaffList.FindAsync(id);
            db.StaffList.Remove(Staff);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

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
                    Selected = v.ToString().ToLower() == selectedStatus
                }
                );
        }

    }
}
