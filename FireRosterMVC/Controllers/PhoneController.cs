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
    [Authorize]
    public class PhoneController : Controller
    {
        private FireRosterDB db = new FireRosterDB();

        // GET: Phone
        public async Task<ActionResult> Index(int staffId)
        {
            return View(await db.Phones.Where(p => p.Staff_ID == staffId).ToListAsync());
        }

        // GET: Phone/Details/5
        public async Task<ActionResult> Details(int staffId, int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Phone phone = await db.Phones.FindAsync(id);
            if (phone == null)
            {
                return HttpNotFound();
            }
            return View(phone);
        }

        // GET: Phone/Create
        public ActionResult Create(int staffId)
        {
            Staff staff = db.StaffList.Find(staffId);
            if (staff == null)
            {
                return HttpNotFound("Staff member not found.");
            }
            ViewBag.StaffID = staff.ID;
            PopulateTypeDropDownList();
            return View();
        }

        // POST: Phone/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(int staffId, [Bind(Include = "ID,Number,Type_ID,Primary")] Phone phone)
        {
            Staff staff = db.StaffList.Find(staffId);
            if (staff == null)
            {
                return HttpNotFound("Staff member not found.");
            }
            ViewBag.StaffID = staff.ID;
            phone.Staff_ID = staff.ID;

            if (ModelState.IsValid)
            {
                db.Phones.Add(phone);
                await db.SaveChangesAsync();
                return RedirectToAction("Details", "Staff", new { id = staff.ID });
            }
            PopulateTypeDropDownList(phone.Type_ID);
            return View(phone);
        }

        // GET: Phone/Edit/5
        public async Task<ActionResult> Edit(int staffId, int? id)
        {
            
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Staff staff = db.StaffList.Find(staffId);
            if (staff == null)
            {
                return HttpNotFound("Staff member not found.");
            }

            Phone phone = await db.Phones.FindAsync(id);
            if (phone == null)
            {
                return HttpNotFound();
            }
            PopulateTypeDropDownList(phone.Type_ID);
            return View(phone);
        }

        // POST: Phone/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int staffId, [Bind(Include = "ID,Number,Type_ID,Primary")] Phone phone)
        {
            Staff staff = db.StaffList.Find(staffId);
            if (staff == null)
            {
                return HttpNotFound("Staff member not found.");
            }
            phone.Staff_ID = staff.ID;

            if (ModelState.IsValid)
            {
                db.Entry(phone).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Details", "Staff", new { id = staff.ID });
            }
            return View(phone);
        }

        // GET: Phone/Delete/5
        public async Task<ActionResult> Delete(int staffId, int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Staff staff = db.StaffList.Find(staffId);
            if (staff == null)
            {
                return HttpNotFound("Staff member not found.");
            }
            ViewBag.StaffID = staff.ID;

            Phone phone = await db.Phones.FindAsync(id);
            if (phone == null)
            {
                return HttpNotFound();
            }
            return View(phone);
        }

        // POST: Phone/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int staffId, int id)
        {
            Phone phone = await db.Phones.FindAsync(id);
            db.Phones.Remove(phone);
            await db.SaveChangesAsync();
            return RedirectToAction("Details", "Staff", new { id = staffId });
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private void PopulateTypeDropDownList(object selectedType = null)
        {
            var typeQuery = from t in db.PhoneTypes
                              orderby t.Label
                              select t;
            ViewBag.Type_ID = new SelectList(typeQuery, "ID", "Label", selectedType);
        }
    }
}
