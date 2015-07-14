using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PagedList;
using System.Globalization;
using FireRosterMVC.Models;
using FireRosterMVC.Enums;

namespace FireRosterMVC.Controllers
{
    public class OvertimeController : Controller
    {
        private FireRosterDB db = new FireRosterDB();

        // GET: Overtimes
        public ActionResult Index(int? Status_ID, string period, int? Location_ID, int? Shift_ID, string code, int? page, int? size)
        {
            int pageSize = (size ?? 20);
            int pageNumber = (page ?? 1);

            PopulateLocationDropDownList(Location_ID);
            PopulateShiftDropDownList(Shift_ID);
            PopulateStatusDropDownList(Status_ID);

            var overtime = from o in db.Overtime
                               .OrderByDescending(o => o.Start)
                               select o;

            if (Location_ID != null)
            {
                overtime = overtime.Where(o => o.Location_ID == Location_ID);
                ViewBag.CurrentLocation_ID = Location_ID;
            }
            if (Shift_ID != null)
            {
                overtime = overtime.Where(o => o.Shift == (Shift)Shift_ID);
                ViewBag.CurrentShift_ID = Shift_ID;
            }
            if (Status_ID != null)
            {
                overtime = overtime.Where(o => o.Status == (OvertimeStatus)Status_ID);
                ViewBag.CurrentStatus_ID = Status_ID;
            }

            return View(overtime.ToPagedList(pageNumber, pageSize));
        }

        // GET: Overtimes/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Overtime overtime = await db.Overtime.FindAsync(id);
            if (overtime == null)
            {
                return HttpNotFound();
            }
            return View(overtime);
        }

        // GET: Overtimes/Create
        public ActionResult Create()
        {
            ViewBag.Code_ID = new SelectList(db.OvertimeCodes, "ID", "Code");
            ViewBag.Location_ID = new SelectList(db.Locations, "ID", "Code");
            ViewBag.Staff_ID = new SelectList(db.StaffList, "ID", "SSN");
            return View();
        }

        // POST: Overtimes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ID,Description,Code_ID,Staff_ID,Location_ID,Shift,Status,Start,End,Hours,ReviewedBy,ReviewedOn")] Overtime overtime)
        {
            if (ModelState.IsValid)
            {
                db.Overtime.Add(overtime);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.Code_ID = new SelectList(db.OvertimeCodes, "ID", "Code", overtime.Code_ID);
            ViewBag.Location_ID = new SelectList(db.Locations, "ID", "Code", overtime.Location_ID);
            ViewBag.Staff_ID = new SelectList(db.StaffList, "ID", "SSN", overtime.Staff_ID);
            return View(overtime);
        }

        // GET: Overtimes/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Overtime overtime = await db.Overtime.FindAsync(id);
            if (overtime == null)
            {
                return HttpNotFound();
            }
            ViewBag.Code_ID = new SelectList(db.OvertimeCodes, "ID", "Code", overtime.Code_ID);
            ViewBag.Location_ID = new SelectList(db.Locations, "ID", "Code", overtime.Location_ID);
            ViewBag.Staff_ID = new SelectList(db.StaffList, "ID", "SSN", overtime.Staff_ID);
            return View(overtime);
        }

        // POST: Overtimes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ID,Description,Code_ID,Staff_ID,Location_ID,Shift,Status,Start,End,Hours,ReviewedBy,ReviewedOn")] Overtime overtime)
        {
            if (ModelState.IsValid)
            {
                db.Entry(overtime).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.Code_ID = new SelectList(db.OvertimeCodes, "ID", "Code", overtime.Code_ID);
            ViewBag.Location_ID = new SelectList(db.Locations, "ID", "Code", overtime.Location_ID);
            ViewBag.Staff_ID = new SelectList(db.StaffList, "ID", "SSN", overtime.Staff_ID);
            return View(overtime);
        }

        // GET: Overtimes/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Overtime overtime = await db.Overtime.FindAsync(id);
            if (overtime == null)
            {
                return HttpNotFound();
            }
            return View(overtime);
        }

        // POST: Overtimes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Overtime overtime = await db.Overtime.FindAsync(id);
            db.Overtime.Remove(overtime);
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


        private void PopulateLocationDropDownList(object selectedLocation = null)
        {
            var locationQuery = from l in db.Locations
                                orderby l.Order
                                select l;
            ViewBag.Location_ID = new SelectList(locationQuery, "ID", "Name", selectedLocation);
        }

        private void PopulateShiftDropDownList(object selectedShift = null)
        {
            SelectList shifts = new SelectList(Enum.GetValues(typeof(Shift)).Cast<Shift>().Select(
                v => new SelectListItem
                {
                    Text = v.ToString(),
                    Value = ((int)v).ToString(),
                    Selected = (selectedShift != null && (int)v == (int)selectedShift ? true : false)
                }
                ).ToList(), "Value", "Text");
            ViewBag.Shift_ID = shifts;
        }

        private void PopulateStatusDropDownList(object selectedStatus = null)
        {
            SelectList states = new SelectList(Enum.GetValues(typeof(OvertimeStatus)).Cast<OvertimeStatus>().Select(
                s => new SelectListItem
                {
                    Text = s.ToString(),
                    Value = ((int)s).ToString(),
                    Selected = (selectedStatus != null && (int)s == (int)selectedStatus ? true : false)
                }
                ).ToList(), "Value", "Text");
            ViewBag.Status_ID = states;
        }
    }
}
