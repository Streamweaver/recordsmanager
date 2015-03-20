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

namespace FireRosterMVC.Controllers
{
    public class RosterController : Controller
    {
        private FireRosterDB db = new FireRosterDB();

        // GET: Roster
        public async Task<ActionResult> Index()
        {
            var tblEmployeeRosterAssignments = db.tblEmployeeRosterAssignments.Include(t => t.tblEmployee).Include(t => t.tblRank).Include(t => t.tblShift);
            return View(await tblEmployeeRosterAssignments.ToListAsync());
        }

        // GET: Roster/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblEmployeeRosterAssignment tblEmployeeRosterAssignment = await db.tblEmployeeRosterAssignments.FindAsync(id);
            if (tblEmployeeRosterAssignment == null)
            {
                return HttpNotFound();
            }
            return View(tblEmployeeRosterAssignment);
        }

        // GET: Roster/Create
        public ActionResult Create()
        {
            ViewBag.EmployeeID = new SelectList(db.tblEmployees, "EmployeeID", "LastUpdateBy");
            ViewBag.RankID = new SelectList(db.tblRanks, "RankID", "LastUpdateBy");
            ViewBag.ShiftID = new SelectList(db.tblShifts, "ShiftID", "LastUpdateBy");
            return View();
        }

        // POST: Roster/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "EmployeeRosterAssignmentID,EmployeeID,LocationID,ShiftID,RankID,DateCreated,LastUpdateDate,LastUpdateBy,DateRemoved,isRemoved,isTestData,EmployeeRosterAssignmentStartDate,EmployeeRosterAssignmentEndDate")] tblEmployeeRosterAssignment tblEmployeeRosterAssignment)
        {
            if (ModelState.IsValid)
            {
                db.tblEmployeeRosterAssignments.Add(tblEmployeeRosterAssignment);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.EmployeeID = new SelectList(db.tblEmployees, "EmployeeID", "LastUpdateBy", tblEmployeeRosterAssignment.EmployeeID);
            ViewBag.RankID = new SelectList(db.tblRanks, "RankID", "LastUpdateBy", tblEmployeeRosterAssignment.RankID);
            ViewBag.ShiftID = new SelectList(db.tblShifts, "ShiftID", "LastUpdateBy", tblEmployeeRosterAssignment.ShiftID);
            return View(tblEmployeeRosterAssignment);
        }

        // GET: Roster/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblEmployeeRosterAssignment tblEmployeeRosterAssignment = await db.tblEmployeeRosterAssignments.FindAsync(id);
            if (tblEmployeeRosterAssignment == null)
            {
                return HttpNotFound();
            }
            ViewBag.EmployeeID = new SelectList(db.tblEmployees, "EmployeeID", "LastUpdateBy", tblEmployeeRosterAssignment.EmployeeID);
            ViewBag.RankID = new SelectList(db.tblRanks, "RankID", "LastUpdateBy", tblEmployeeRosterAssignment.RankID);
            ViewBag.ShiftID = new SelectList(db.tblShifts, "ShiftID", "LastUpdateBy", tblEmployeeRosterAssignment.ShiftID);
            return View(tblEmployeeRosterAssignment);
        }

        // POST: Roster/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "EmployeeRosterAssignmentID,EmployeeID,LocationID,ShiftID,RankID,DateCreated,LastUpdateDate,LastUpdateBy,DateRemoved,isRemoved,isTestData,EmployeeRosterAssignmentStartDate,EmployeeRosterAssignmentEndDate")] tblEmployeeRosterAssignment tblEmployeeRosterAssignment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tblEmployeeRosterAssignment).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.EmployeeID = new SelectList(db.tblEmployees, "EmployeeID", "LastUpdateBy", tblEmployeeRosterAssignment.EmployeeID);
            ViewBag.RankID = new SelectList(db.tblRanks, "RankID", "LastUpdateBy", tblEmployeeRosterAssignment.RankID);
            ViewBag.ShiftID = new SelectList(db.tblShifts, "ShiftID", "LastUpdateBy", tblEmployeeRosterAssignment.ShiftID);
            return View(tblEmployeeRosterAssignment);
        }

        // GET: Roster/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblEmployeeRosterAssignment tblEmployeeRosterAssignment = await db.tblEmployeeRosterAssignments.FindAsync(id);
            if (tblEmployeeRosterAssignment == null)
            {
                return HttpNotFound();
            }
            return View(tblEmployeeRosterAssignment);
        }

        // POST: Roster/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            tblEmployeeRosterAssignment tblEmployeeRosterAssignment = await db.tblEmployeeRosterAssignments.FindAsync(id);
            db.tblEmployeeRosterAssignments.Remove(tblEmployeeRosterAssignment);
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
