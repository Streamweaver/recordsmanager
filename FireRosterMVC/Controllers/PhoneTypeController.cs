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
    public class PhoneTypeController : Controller
    {
        private FireRosterDB db = new FireRosterDB();

        // GET: PhoneType
        public async Task<ActionResult> Index()
        {
            return View(await db.PhoneTypes.ToListAsync());
        }

        // GET: PhoneType/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PhoneType phoneType = await db.PhoneTypes.FindAsync(id);
            if (phoneType == null)
            {
                return HttpNotFound();
            }
            return View(phoneType);
        }

        // GET: PhoneType/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PhoneType/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ID,Label")] PhoneType phoneType)
        {
            if (ModelState.IsValid)
            {
                db.PhoneTypes.Add(phoneType);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(phoneType);
        }

        // GET: PhoneType/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PhoneType phoneType = await db.PhoneTypes.FindAsync(id);
            if (phoneType == null)
            {
                return HttpNotFound();
            }
            return View(phoneType);
        }

        // POST: PhoneType/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ID,Label")] PhoneType phoneType)
        {
            if (ModelState.IsValid)
            {
                db.Entry(phoneType).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(phoneType);
        }

        // GET: PhoneType/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PhoneType phoneType = await db.PhoneTypes.FindAsync(id);
            if (phoneType == null)
            {
                return HttpNotFound();
            }
            return View(phoneType);
        }

        // POST: PhoneType/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            PhoneType phoneType = await db.PhoneTypes.FindAsync(id);
            db.PhoneTypes.Remove(phoneType);
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
