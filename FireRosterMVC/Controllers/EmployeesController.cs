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

namespace FireRosterMVC.Controllers
{
    public class EmployeesController : Controller
    {
        private FireRosterDB db = new FireRosterDB();

        // GET: Employees
        public ActionResult Index(string sortOrder, string sortField, string searchString, string statusFilter, int? page)
        {
            int pageSize = 20;
            int pageNumber = (page ?? 1);
            ViewBag.CurrentSortOrder = String.IsNullOrEmpty(sortOrder) ? "asc" : sortOrder.ToLower() ;
            ViewBag.CurrentSortField = String.IsNullOrEmpty(sortField) ? "name" : sortField.ToLower();
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

            var staff = from s in db.tblEmployees
                            .Where(s => s.isRemoved == false)
                        select s;

            if (!String.IsNullOrEmpty(searchString))
            {
                ViewBag.CurrentFilter = searchString;
                staff = staff.Where(s => s.LastName.Contains(searchString)
                    || s.FirstName.Contains(searchString));
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

            Expression<Func<tblEmployee,Object>> OrderByExpression = e => e.LastName;
            switch (sortField)
            {
                case "name":
                    OrderByExpression = s => s.LastName;
                    break;
                case "cdl":
                    OrderByExpression = s => s.CareerDevelopmentLevel;
                    break;
                case "userid":
                    OrderByExpression = s => s.HenricoUserID;
                    break;
                default:
                    OrderByExpression = s => s.LastName;
                    break;
            }

            if (sortOrder == "desc") 
            {
                staff = staff.OrderByDescending(OrderByExpression);
            }
            else
            {
                staff = staff.OrderBy(OrderByExpression);
            }
            
            return View(staff.ToPagedList(pageNumber, pageSize));
        }

        // GET: Employees/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblEmployee Employee = await db.tblEmployees.FindAsync(id);
            if (Employee == null)
            {
                return HttpNotFound();
            }
            return View(Employee);
        }

        // GET: Employees/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Employees/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "EmployeeID,SSN,NamePrefix,FirstName,MiddleName,LastName,NameSuffix,Sex,Race,DateOfBirth,RankDate,CareerDevelopmentLevel,CareerDevelopmentDate,EmploymentDate,TerminationDate,isMilitaryLeave,RosterRank,HenricoUserID,OracleHrID,BadgeNumber")] tblEmployee Employee)
        {
            if (ModelState.IsValid)
            {
                db.tblEmployees.Add(Employee);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(Employee);
        }

        // GET: Employees/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblEmployee Employee = await db.tblEmployees.FindAsync(id);
            if (Employee == null)
            {
                return HttpNotFound();
            }
            return View(Employee);
        }

        // POST: Employees/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "EmployeeID,DateCreated,LastUpdateDate,LastUpdateBy,DateRemoved,isRemoved,isTestData,SSN,NamePrefix,FirstName,MiddleName,LastName,NameSuffix,Race,Sex,DateOfBirth,RankDate,CareerDevelopmentLevel,CareerDevelopmentDate,EmploymentDate,TerminationDate,isMilitaryLeave,RosterRank,HenricoUserID,OracleHrID,BadgeNumber")] tblEmployee Employee)
        {
            if (ModelState.IsValid)
            {
                db.Entry(Employee).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(Employee);
        }

        // GET: Employees/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblEmployee Employee = await db.tblEmployees.FindAsync(id);
            if (Employee == null)
            {
                return HttpNotFound();
            }
            return View(Employee);
        }

        // POST: Employees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            tblEmployee Employee = await db.tblEmployees.FindAsync(id);
            db.tblEmployees.Remove(Employee);
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
    }
}
