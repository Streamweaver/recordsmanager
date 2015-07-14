using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PagedList;
using FireRosterMVC.Models;

namespace FireRosterMVC.Controllers
{
    public class OvertimeAvailabilityController : Controller
    {
        private FireRosterDB db = new FireRosterDB();

        // GET: OvertimeAvailabilities
        public ActionResult Index(string after, string before, int? page, int? size)
        {
            int pageSize = (size ?? 50);
            int pageNumber = (page ?? 1);

            var oa = from o in db.OvertimeAvailability
                     .OrderByDescending(o => o.Start)
                     select o;

            CultureInfo enUS = new CultureInfo("en-US");
            DateTime dta, dtb;
            if (DateTime.TryParseExact(after, "MM/dd/yyyy", enUS, DateTimeStyles.None, out dta))
            {
                oa = oa.Where(o => o.Start >= dta);
                ViewBag.CurrentAfter = after;
            }
            if (DateTime.TryParseExact(before, "MM/dd/yyyy", enUS, DateTimeStyles.None, out dtb))
            {
                oa = oa.Where(o => o.End <= dtb);
                ViewBag.CurrentBefore = before;
            }
            var overtimeAvailability = db.OvertimeAvailability.Include(o => o.Staff);
            return View(oa.ToPagedList(pageNumber, pageSize));
        }

        // GET: OvertimeAvailabilities/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OvertimeAvailability overtimeAvailability = await db.OvertimeAvailability.FindAsync(id);
            if (overtimeAvailability == null)
            {
                return HttpNotFound();
            }
            return View(overtimeAvailability);
        }

        // GET: OvertimeAvailabilities/Create
        public ActionResult Create()
        {
            ViewBag.Staff_ID = new SelectList(db.StaffList, "ID", "SSN");
            return View();
        }

        // POST: OvertimeAvailabilities/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ID,Staff_ID,Start,End")] OvertimeAvailability overtimeAvailability)
        {
            if (ModelState.IsValid)
            {
                db.OvertimeAvailability.Add(overtimeAvailability);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.Staff_ID = new SelectList(db.StaffList, "ID", "SSN", overtimeAvailability.Staff_ID);
            return View(overtimeAvailability);
        }

        // GET: OvertimeAvailabilities/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OvertimeAvailability overtimeAvailability = await db.OvertimeAvailability.FindAsync(id);
            if (overtimeAvailability == null)
            {
                return HttpNotFound();
            }
            ViewBag.Staff_ID = new SelectList(db.StaffList, "ID", "SSN", overtimeAvailability.Staff_ID);
            return View(overtimeAvailability);
        }

        // POST: OvertimeAvailabilities/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ID,Staff_ID,Start,End")] OvertimeAvailability overtimeAvailability)
        {
            if (ModelState.IsValid)
            {
                db.Entry(overtimeAvailability).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.Staff_ID = new SelectList(db.StaffList, "ID", "SSN", overtimeAvailability.Staff_ID);
            return View(overtimeAvailability);
        }

        // GET: OvertimeAvailabilities/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OvertimeAvailability overtimeAvailability = await db.OvertimeAvailability.FindAsync(id);
            if (overtimeAvailability == null)
            {
                return HttpNotFound();
            }
            return View(overtimeAvailability);
        }

        // POST: OvertimeAvailabilities/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            OvertimeAvailability overtimeAvailability = await db.OvertimeAvailability.FindAsync(id);
            db.OvertimeAvailability.Remove(overtimeAvailability);
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
