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
using FireRosterMVC.Enums;
using PagedList;

namespace FireRosterMVC.Controllers
{
    public class PositionController : Controller
    {
        private FireRosterDB db = new FireRosterDB();

        // GET: Position
        public ActionResult Index(int? page)
        {
            int pageSize = 20;
            int pageNumber = (page ?? 1);
            //string activeStatus = String.IsNullOrEmpty(filterStatus) ? "0" : filterStatus;
            //ViewBag.CurrentStatusFilter = activeStatus;

            var positions = from p in db.Positions
                            .OrderBy(p => p.StartDate)
                            select p;

            return View(positions.ToPagedList(pageNumber, pageSize));
        }

        // GET: Position/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Position position = await db.Positions.FindAsync(id);
            if (position == null)
            {
                return HttpNotFound();
            }
            return View(position);
        }

        // GET: Position/Create
        public ActionResult Create()
        {
            PopulateStatusDropDownList();
            PopulateLocationDropDownList();
            PopulateRankDrowDownlist();
            return View();
        }

        // POST: Position/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ID,Code,Status_ID,Staff_ID,Location_ID,Rank_ID,Shift,StartDate,EndDate")] Position position)
        {
            // ValidateDateCodeOverlap(position);

            if (ModelState.IsValid)
            {
                db.Positions.Add(position);
                await db.SaveChangesAsync();
                return RedirectToAction("Details", "Position", new { id = position.ID });
            }

            PopulateStatusDropDownList(position.Status_ID);
            PopulateLocationDropDownList(position.Location_ID);
            PopulateRankDrowDownlist(position.Rank_ID);
            return View(position);
        }

        // GET: Position/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Position position = await db.Positions.FindAsync(id);
            if (position == null)
            {
                return HttpNotFound();
            }
            PopulateStatusDropDownList(position.Status_ID);
            PopulateLocationDropDownList(position.Location_ID);
            PopulateRankDrowDownlist(position.Rank_ID);
            return View(position);
        }

        // POST: Position/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ID,Code,Status_ID,Staff_ID,Location_ID,Rank_ID,Shift,StartDate,EndDate")] Position position)
        {
            if (ModelState.IsValid)
            {
                db.Entry(position).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Details", "Position", new { id = position.ID });
            }
            PopulateStatusDropDownList(position.Status_ID);
            PopulateLocationDropDownList(position.Location_ID);
            PopulateRankDrowDownlist(position.Rank_ID);
            return View(position);
        }

        // GET: Position/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Position position = await db.Positions.FindAsync(id);
            if (position == null)
            {
                return HttpNotFound();
            }
            return View(position);
        }

        // POST: Position/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Position position = await db.Positions.FindAsync(id);
            db.Positions.Remove(position);
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

        private void PopulateStatusDropDownList(object selectedStatus = null)
        {
            var statusQuery = from s in db.PositionStatus
                           orderby s.Label
                           select s;
            ViewBag.Status_ID = new SelectList(statusQuery, "ID", "Label", selectedStatus);
        }

        private void PopulateLocationDropDownList(object selectedLocation = null)
        {
            var locationQuery = from l in db.Locations
                              orderby l.Order
                              select l;
            ViewBag.Location_ID = new SelectList(locationQuery, "ID", "Name", selectedLocation);
        }

        private void PopulateRankDrowDownlist(object selectedRank = null)
        {
            var rankQuery = from r in db.Ranks
                            orderby r.Order
                            select r;
            ViewBag.Rank_ID = new SelectList(rankQuery, "ID", "Code", selectedRank);
        }

        private void ValidateDateCodeOverlap(Position position)
        {
            ModelState.AddModelError(string.Empty, "Position start or end dates overlap with an existing position with the same Code.");
        }

    }
}
