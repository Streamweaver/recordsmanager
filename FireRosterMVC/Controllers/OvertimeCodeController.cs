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
    public class OvertimeCodeController : Controller
    {
        private FireRosterDB db = new FireRosterDB();

        // GET: OvertimeCodes
        public async Task<ActionResult> Index()
        {
            return View(await db.OvertimeCodes.OrderBy(o => o.Code).ToListAsync());
        }

        // GET: OvertimeCodes/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OvertimeCode overtimeCode = await db.OvertimeCodes.FindAsync(id);
            if (overtimeCode == null)
            {
                return HttpNotFound();
            }
            return View(overtimeCode);
        }

        // GET: OvertimeCodes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: OvertimeCodes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ID,Code,Label,ShortLabel,Active")] OvertimeCode overtimeCode)
        {
            if (ModelState.IsValid)
            {
                db.OvertimeCodes.Add(overtimeCode);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(overtimeCode);
        }

        // GET: OvertimeCodes/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OvertimeCode overtimeCode = await db.OvertimeCodes.FindAsync(id);
            if (overtimeCode == null)
            {
                return HttpNotFound();
            }
            return View(overtimeCode);
        }

        // POST: OvertimeCodes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ID,Code,Label,ShortLabel,Active")] OvertimeCode overtimeCode)
        {
            if (ModelState.IsValid)
            {
                db.Entry(overtimeCode).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(overtimeCode);
        }

        // GET: OvertimeCodes/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OvertimeCode overtimeCode = await db.OvertimeCodes.FindAsync(id);
            if (overtimeCode == null)
            {
                return HttpNotFound();
            }
            return View(overtimeCode);
        }

        // POST: OvertimeCodes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            OvertimeCode overtimeCode = await db.OvertimeCodes.FindAsync(id);
            db.OvertimeCodes.Remove(overtimeCode);
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
