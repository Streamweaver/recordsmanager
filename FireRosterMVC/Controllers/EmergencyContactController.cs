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
    public class EmergencyContactController : Controller
    {
        private FireRosterDB db = new FireRosterDB();

        // GET: EmergencyContacts
        public async Task<ActionResult> Index(int staffId)
        {
            return View(await db.EmergencyContacts.Where(e => e.Staff_ID == staffId).ToListAsync());
        }

        // GET: EmergencyContacts/Details/5
        public async Task<ActionResult> Details(int staffId, int? id)
        {
            Staff staff = db.StaffList.Find(staffId);
            if (staff == null)
            {
                return HttpNotFound("Staff member not found.");
            }
 
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EmergencyContact emergencyContact = await db.EmergencyContacts.FindAsync(id);
            if (emergencyContact == null)
            {
                return HttpNotFound();
            }
            return View(emergencyContact);
        }

        // GET: EmergencyContacts/Create
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

        // POST: EmergencyContacts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(int staffId, [Bind(Include = "ID,FirstName,LastName,Relationship,PhoneType_ID,PhoneNumber,Order")] EmergencyContact emergencyContact)
        {
            Staff staff = db.StaffList.Find(staffId);
            if (staff == null)
            {
                return HttpNotFound("Staff member not found.");
            }
            ViewBag.StaffID = staff.ID;
            emergencyContact.Staff_ID = staff.ID;

            if (ModelState.IsValid)
            {
                db.EmergencyContacts.Add(emergencyContact);
                await db.SaveChangesAsync();
                return RedirectToAction("Details", "Staff", new { id = staff.ID });
            }
            PopulateTypeDropDownList(emergencyContact.PhoneType_ID);
            return View(emergencyContact);
        }

        // GET: EmergencyContacts/Edit/5
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
            ViewBag.StaffID = staff.ID;

            EmergencyContact emergencyContact = await db.EmergencyContacts.FindAsync(id);
            if (emergencyContact == null)
            {
                return HttpNotFound();
            }
            PopulateTypeDropDownList(emergencyContact.PhoneType_ID);
            return View(emergencyContact);
        }

        // POST: EmergencyContacts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int staffId, [Bind(Include = "ID,FirstName,LastName,Relationship,PhoneType_ID,PhoneNumber,Order")] EmergencyContact emergencyContact)
        {
            Staff staff = db.StaffList.Find(staffId);
            if (staff == null)
            {
                return HttpNotFound("Staff member not found.");
            }
            emergencyContact.Staff_ID = staff.ID;

            if (ModelState.IsValid)
            {
                db.Entry(emergencyContact).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Details", "Staff", new { id = staff.ID });
            }
            PopulateTypeDropDownList(emergencyContact.PhoneType_ID);
            return View(emergencyContact);
        }

        // GET: EmergencyContacts/Delete/5
        public async Task<ActionResult> Delete(int staffId, int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EmergencyContact emergencyContact = await db.EmergencyContacts.FindAsync(id);
            if (emergencyContact == null)
            {
                return HttpNotFound();
            }
            return View(emergencyContact);
        }

        // POST: EmergencyContacts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int staffId, int id)
        {
            EmergencyContact emergencyContact = await db.EmergencyContacts.FindAsync(id);
            db.EmergencyContacts.Remove(emergencyContact);
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
            ViewBag.PhoneType_ID = new SelectList(typeQuery, "ID", "Label", selectedType);
        }
    }
}
