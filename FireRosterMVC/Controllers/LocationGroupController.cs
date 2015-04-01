﻿using System;
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
    public class LocationGroupController : Controller
    {
        private FireRosterDB db = new FireRosterDB();

        // GET: LocationGroup
        public async Task<ActionResult> Index()
        {
            return View(await db.LocationGroups.ToListAsync());
        }

        // GET: LocationGroup/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LocationGroup locationGroup = await db.LocationGroups.FindAsync(id);
            if (locationGroup == null)
            {
                return HttpNotFound();
            }
            return View(locationGroup);
        }

        // GET: LocationGroup/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: LocationGroup/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ID,Label")] LocationGroup locationGroup)
        {
            if (ModelState.IsValid)
            {
                db.LocationGroups.Add(locationGroup);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(locationGroup);
        }

        // GET: LocationGroup/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LocationGroup locationGroup = await db.LocationGroups.FindAsync(id);
            if (locationGroup == null)
            {
                return HttpNotFound();
            }
            return View(locationGroup);
        }

        // POST: LocationGroup/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ID,Label")] LocationGroup locationGroup)
        {
            if (ModelState.IsValid)
            {
                db.Entry(locationGroup).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(locationGroup);
        }

        // GET: LocationGroup/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LocationGroup locationGroup = await db.LocationGroups.FindAsync(id);
            if (locationGroup == null)
            {
                return HttpNotFound();
            }
            return View(locationGroup);
        }

        // POST: LocationGroup/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            LocationGroup locationGroup = await db.LocationGroups.FindAsync(id);
            db.LocationGroups.Remove(locationGroup);
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