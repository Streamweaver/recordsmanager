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
    public class PhoneController : Controller
    {
        private FireRosterDB db = new FireRosterDB();

        // GET: Phone
        public async Task<ActionResult> Index()
        {
            return View(await db.Phones.ToListAsync());
        }

        // GET: Phone/Details/5
        public async Task<ActionResult> Details(int? id)
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
        public ActionResult Create()
        {
            return View();
        }

        // POST: Phone/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ID,Number,Primary")] Phone phone)
        {
            if (ModelState.IsValid)
            {
                db.Phones.Add(phone);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(phone);
        }

        // GET: Phone/Edit/5
        public async Task<ActionResult> Edit(int? id)
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
            PopulateTypeDropDownList(phone.Type_ID);
            return View(phone);
        }

        // POST: Phone/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ID,Number,Primary")] Phone phone)
        {
            if (ModelState.IsValid)
            {
                db.Entry(phone).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(phone);
        }

        // GET: Phone/Delete/5
        public async Task<ActionResult> Delete(int? id)
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

        // POST: Phone/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Phone phone = await db.Phones.FindAsync(id);
            db.Phones.Remove(phone);
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

        private void PopulateTypeDropDownList(object selectedType = null)
        {
            var typeQuery = from t in db.PhoneTypes
                              orderby t.Label
                              select t;
            ViewBag.Type_ID = new SelectList(typeQuery, "ID", "Label", selectedType);
        }
    }
}
