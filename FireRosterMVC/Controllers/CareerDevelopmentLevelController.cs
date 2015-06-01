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
using FireRosterMVC.Models.Codes;

namespace FireRosterMVC.Controllers
{
    [Authorize]
    public class CareerDevelopmentLevelController : Controller
    {
        private FireRosterDB db = new FireRosterDB();

        // GET: CareerDevelopmentLevel
        public async Task<ActionResult> Index()
        {
            return View(await db.CareerDevelopmentLevels.ToListAsync());
        }

        // GET: CareerDevelopmentLevel/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CareerDevelopmentLevel careerDevelopmentLevel = await db.CareerDevelopmentLevels.FindAsync(id);
            if (careerDevelopmentLevel == null)
            {
                return HttpNotFound();
            }
            return View(careerDevelopmentLevel);
        }

        // GET: CareerDevelopmentLevel/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CareerDevelopmentLevel/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ID,Label")] CareerDevelopmentLevel careerDevelopmentLevel)
        {
            if (ModelState.IsValid)
            {
                db.CareerDevelopmentLevels.Add(careerDevelopmentLevel);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(careerDevelopmentLevel);
        }

        // GET: CareerDevelopmentLevel/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CareerDevelopmentLevel careerDevelopmentLevel = await db.CareerDevelopmentLevels.FindAsync(id);
            if (careerDevelopmentLevel == null)
            {
                return HttpNotFound();
            }
            return View(careerDevelopmentLevel);
        }

        // POST: CareerDevelopmentLevel/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ID,Label")] CareerDevelopmentLevel careerDevelopmentLevel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(careerDevelopmentLevel).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(careerDevelopmentLevel);
        }

        // GET: CareerDevelopmentLevel/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CareerDevelopmentLevel careerDevelopmentLevel = await db.CareerDevelopmentLevels.FindAsync(id);
            if (careerDevelopmentLevel == null)
            {
                return HttpNotFound();
            }
            return View(careerDevelopmentLevel);
        }

        // POST: CareerDevelopmentLevel/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            CareerDevelopmentLevel careerDevelopmentLevel = await db.CareerDevelopmentLevels.FindAsync(id);
            db.CareerDevelopmentLevels.Remove(careerDevelopmentLevel);
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
