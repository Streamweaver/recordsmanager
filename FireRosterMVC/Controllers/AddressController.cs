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
    [Authorize(Roles = "Admin, Manager")]
    public class AddressController : Controller
    {
        private FireRosterDB db = new FireRosterDB();

        // GET: Address
        public async Task<ActionResult> Index(int staffId)
        {

            return View(await db.Address.Where(a => a.Staff_ID == staffId).ToListAsync());
        }

        // GET: Address/Details/5
        [Authorize(Roles = "Admin, Manager, Payroll")]
        public async Task<ActionResult> Details(int staffID, int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Address address = await db.Address.FindAsync(id);
            if (address == null)
            {
                return HttpNotFound();
            }
            return View(address);
        }

        // GET: Address/Create
        public ActionResult Create(int staffId)
        {
            Staff staff = db.StaffList.Find(staffId);
            if (staff == null)
            {
                return HttpNotFound("Staff member not found.");
            }
            ViewBag.StaffID = staff.ID;
            return View();
        }

        // POST: Address/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(int staffId, [Bind(Include = "ID,Street1,Street2,City,State,Zip,Primary")] Address address)
        {
            Staff staff = db.StaffList.Find(staffId);
            if (staff == null)
            {
                return HttpNotFound("Staff member not found.");
            }
            ViewBag.StaffID = staff.ID;
            address.Staff_ID = staff.ID;

            if (ModelState.IsValid)
            {
                db.Address.Add(address);
                await db.SaveChangesAsync();
                return RedirectToAction("Details", "Staff", new { id = staff.ID });
            }

            return View(address);
        }

        // GET: Address/Edit/5
        public async Task<ActionResult> Edit(int staffId, int? id)
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

            Address address = await db.Address.FindAsync(id);
           
            if (address == null)
            {
                return HttpNotFound();
            }
            return View(address);
        }

        // POST: Address/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int staffId, [Bind(Include = "ID,Street1,Street2,City,State,Zip,Primary")] Address address)
        {
            Staff staff = db.StaffList.Find(staffId);
            if (staff == null)
            {
                return HttpNotFound("Staff member not found.");
            }
            address.Staff_ID = staffId;

            if (ModelState.IsValid)
            {
                db.Entry(address).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Details", "Staff", new { id = staff.ID });
            }
            return View(address);
        }

        // GET: Address/Delete/5
        public async Task<ActionResult> Delete(int staffId, int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Address address = await db.Address.FindAsync(id);
            if (address == null)
            {
                return HttpNotFound();
            }
            return View(address);
        }

        // POST: Address/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int staffId, int id)
        {
            Address address = await db.Address.FindAsync(id);
            db.Address.Remove(address);
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
    }
}
