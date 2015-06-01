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
    public class PositionStatusController : Controller
    {
        private FireRosterDB db = new FireRosterDB();

        // GET: PositionStatus
        public async Task<ActionResult> Index()
        {
            return View(await db.PositionStatus.ToListAsync());
        }

        // GET: PositionStatus/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PositionStatus positionStatus = await db.PositionStatus.FindAsync(id);
            if (positionStatus == null)
            {
                return HttpNotFound();
            }
            return View(positionStatus);
        }

        // GET: PositionStatus/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PositionStatus/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ID,Label")] PositionStatus positionStatus)
        {
            if (ModelState.IsValid)
            {
                db.PositionStatus.Add(positionStatus);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(positionStatus);
        }

        // GET: PositionStatus/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PositionStatus positionStatus = await db.PositionStatus.FindAsync(id);
            if (positionStatus == null)
            {
                return HttpNotFound();
            }
            return View(positionStatus);
        }

        // POST: PositionStatus/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ID,Label")] PositionStatus positionStatus)
        {
            if (ModelState.IsValid)
            {
                db.Entry(positionStatus).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(positionStatus);
        }

        // GET: PositionStatus/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PositionStatus positionStatus = await db.PositionStatus.FindAsync(id);
            if (positionStatus == null)
            {
                return HttpNotFound();
            }
            return View(positionStatus);
        }

        // POST: PositionStatus/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            PositionStatus positionStatus = await db.PositionStatus.FindAsync(id);
            db.PositionStatus.Remove(positionStatus);
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
