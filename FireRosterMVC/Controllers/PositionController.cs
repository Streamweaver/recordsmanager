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
using System.Globalization;

namespace FireRosterMVC.Controllers
{
    public class PositionController : Controller
    {
        private FireRosterDB db = new FireRosterDB();

        // GET: Position
        public ActionResult Index(string Code, int? State_ID, int? Location_ID, int? Shift_ID, int? Rank_ID, int? Status_ID, string period, int? Staff_ID, int? page, int? size)
        {
            int pageSize = (size ?? 20);
            int pageNumber = (page ?? 1);

            PopulateRankDropDownlist(Rank_ID);
            PopulateLocationDropDownList(Location_ID);
            PopulateStatusDropDownList(Status_ID);
            PopulateShiftDropDownList(Shift_ID);
            PopulateStateDropDownList(State_ID);

            var positions = from p in db.Positions
                            .OrderByDescending(p => p.StartDate)
                            select p;

            if (Shift_ID != null)
            {
                positions = positions.Where(p => p.Shift == (Shift)Shift_ID);
                ViewBag.CurrentShift = Shift_ID;
            }

            if (Rank_ID != null)
            {
                positions = positions.Where(p => p.Rank_ID == Rank_ID);
                ViewBag.CurrentRank_ID = Rank_ID;
            }

            if (Status_ID != null)
            {
                positions = positions.Where(p => p.Status_ID == Status_ID);
                ViewBag.CurrentStatus_ID = Status_ID;
            }

            if (Location_ID != null)
            {
                positions = positions.Where(p => p.Location_ID == Location_ID);
                ViewBag.CurrentLocation_ID = Location_ID;
            }

            if (!String.IsNullOrEmpty(Code))
            {
                positions = positions.Where(p => p.Code.Equals(Code));
                ViewBag.CurrentCode = Code;
            }

            if (Staff_ID != null)
            {
                positions = positions.Where(p => p.Staff_ID == Staff_ID);
                ViewBag.CurrentStaff_ID = Staff_ID;
            }

            if (State_ID != null)
            {
                ViewBag.CurrentState_ID = State_ID;
            }
            switch (State_ID)
            {  
                case 0:
                    positions = positions.Where(p => p.Staff_ID == null);
                    break;
                case 1:
                    positions = positions.Where(p => p.Staff_ID != null);
                    break;
                default: // defaults to all
                    break;
            }

            CultureInfo enUS = new CultureInfo("en-US");
            DateTime dt;
            if (DateTime.TryParseExact(period, "MM/dd/yyyy", enUS, DateTimeStyles.None, out dt))
            {
                positions = positions
                    .Where(p => p.StartDate <= dt)
                    .Where(p => p.EndDate >= dt || p.EndDate == null);
                ViewBag.CurrentPeriod = period;
            }

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
            PopulateRankDropDownlist();
            return View();
        }

        // POST: Position/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ID,Code,Status_ID,Staff_ID,Location_ID,Rank_ID,Shift,StartDate,EndDate")] Position position)
        {
            ValidateStartDateOverlap(position);

            if (ModelState.IsValid)
            {
                db.Positions.Add(position);
                await db.SaveChangesAsync();
                return RedirectToAction("Details", "Position", new { id = position.ID });
            }

            PopulateStatusDropDownList(position.Status_ID);
            PopulateLocationDropDownList(position.Location_ID);
            PopulateRankDropDownlist(position.Rank_ID);
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
            PopulateRankDropDownlist(position.Rank_ID);
            return View(position);
        }

        // POST: Position/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ID,Code,Status_ID,Staff_ID,Location_ID,Rank_ID,Shift,StartDate,EndDate")] Position position)
        {
            ValidateStartDateOverlap(position);
            if (ModelState.IsValid)
            {
                db.Entry(position).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Details", "Position", new { id = position.ID });
            }
            PopulateStatusDropDownList(position.Status_ID);
            PopulateLocationDropDownList(position.Location_ID);
            PopulateRankDropDownlist(position.Rank_ID);
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

        private void PopulateRankDropDownlist(object selectedRank = null)
        {
            var rankQuery = from r in db.Ranks
                            orderby r.Order
                            select r;
            ViewBag.Rank_ID = new SelectList(rankQuery, "ID", "Code", selectedRank);
        }

        private void PopulateShiftDropDownList(object selectedShift = null)
        {
            SelectList shifts = new SelectList(Enum.GetValues(typeof(Shift)).Cast<Shift>().Select(
                v => new SelectListItem
                {
                    Text = v.ToString(),
                    Value = ((int)v).ToString(),
                    Selected = (selectedShift != null && (int)v == (int)selectedShift ? true : false)
                }
                ).ToList(), "Value", "Text");
            ViewBag.Shift_ID = shifts;
        }

        private void PopulateStateDropDownList(object selectedState = null)
        {
            SelectList states = new SelectList(Enum.GetValues(typeof(PositionStates)).Cast<PositionStates>().Select(
                s => new SelectListItem
                {
                    Text = s.ToString(),
                    Value = ((int)s).ToString(),
                    Selected = (selectedState != null && (int)s == (int)selectedState ? true : false)
                }
                ).ToList(), "Value", "Text");
            ViewBag.State_ID = states;
        }

        private void ValidateStartDateOverlap(Position position)
        {
            // Base position find based on code.
            var results = from p in db.Positions
                            .Where(p => p.Code == position.Code)
                            .Where(p => p.ID != position.ID)
                          select p;

            // It is invalid if the start date falls within an existing position date range.
            var results_start = results
                                    .Where(p => p.StartDate < position.StartDate)
                                    .Where(p => p.EndDate > position.StartDate || p.EndDate == null);
            if (results_start.Count() > 0)
            {
                ModelState.AddModelError("StartDate",
                    "A position with this Code has not ended before or on this start date."
                    );
            }

            // It is invalid if the end date falls within an existing position range.
            var results_end = results
                                .Where(p => p.StartDate < position.EndDate)
                                .Where(p => p.EndDate > position.EndDate || p.EndDate == null);
            if (results_end.Count() > 0)
            {
                ModelState.AddModelError(string.Empty,
                    "A position with thie Code overlaps this start and end date periods."
                    );
            }

            // it is invalid if an existing position falls within the date range of this position.
            var results_btwn = results
                                .Where(p => p.StartDate > position.StartDate)
                                .Where(p => p.EndDate < position.EndDate || position.EndDate == null);
            if (results_btwn.Count() > 0)
            {
                ModelState.AddModelError(string.Empty,
                    "Another positions with this Code falls inside the start and end dates of this position.");
            }

            // It is invalid if the end date comes before the start date
            if (position.StartDate > position.EndDate)
            {
                ModelState.AddModelError("EndDate",
                    "A position cannot end before it starts."
                    );
            }
        }

    }
}
