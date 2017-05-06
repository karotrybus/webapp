using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using webapp.Models;

namespace webapp.Controllers
{
    public class BoatController : Controller
    {
        private MarineContainer db = new MarineContainer();

        // GET: Boat
        public ActionResult Index(String name, String bunks, String type)
        {
            var boatSet = db.BoatSet.Include(b => b.Skipper).Include(b => b.Port);
            boatSet = boatSet.OrderBy(b => b.Name);
            if (name != "" && name != null) 
                boatSet = boatSet.Where(b => b.Name.Contains(name));
            if (bunks != "" && bunks != null)
                boatSet = boatSet.Where(b => b.Bunks.ToString() == bunks);
            if (type != "" && type != null)
                boatSet = boatSet.Where(b => b.Type.Contains(type));
            
            return View(boatSet.ToList());
        }

        // GET: Boat/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Boat boat = db.BoatSet.Find(id);
            if (boat == null)
            {
                return HttpNotFound();
            }
            return View(boat);
        }

        // GET: Boat/Create
        public ActionResult Create()
        {
            ViewBag.SkipperId = new SelectList(db.SkipperSet, "Id", "Name");
            ViewBag.PortId = new SelectList(db.PortSet, "Id", "Location");
            return View();
        }

        // POST: Boat/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Create([Bind(Include = "Id,Name,Type,Model,Price,Bunks,isRented,SkipperId,PortId")] Boat boat)
        {
            if (ModelState.IsValid)
            {
                db.BoatSet.Add(boat);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.SkipperId = new SelectList(db.SkipperSet, "Id", "Name", boat.SkipperId);
            ViewBag.PortId = new SelectList(db.PortSet, "Id", "Location", boat.PortId);
            return View(boat);
        }

        // GET: Boat/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Boat boat = db.BoatSet.Find(id);
            if (boat == null)
            {
                return HttpNotFound();
            }
            ViewBag.SkipperId = new SelectList(db.SkipperSet, "Id", "Name", boat.SkipperId);
            ViewBag.PortId = new SelectList(db.PortSet, "Id", "Location", boat.PortId);
            return View(boat);
        }

        // POST: Boat/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Edit([Bind(Include = "Id,Name,Type,Model,Price,Bunks,isRented,SkipperId,PortId")] Boat boat)
        {
            if (ModelState.IsValid)
            {
                db.Entry(boat).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.SkipperId = new SelectList(db.SkipperSet, "Id", "Name", boat.SkipperId);
            ViewBag.PortId = new SelectList(db.PortSet, "Id", "Location", boat.PortId);
            return View(boat);
        }

        // GET: /Boat/RentBoat
        public ActionResult RentBoat(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Boat boat = db.BoatSet.Find(id);
            if (boat == null)
            {
                return HttpNotFound();
            }
            return View();
        }

        // POST: /Boat/RentBoat
        [HttpPost]
        public ActionResult RentBoat(int id, bool isRented)
        {
            Boat boat = db.BoatSet.Find(id);
            if (ModelState.IsValid)
            {
                db.BoatSet.Attach(boat);
                db.Entry(boat).State = EntityState.Modified;
                boat.isRented = true;
              
                db.Entry(boat).Property(b => b.isRented).IsModified = true;
                try
                {
                    db.SaveChanges();
                }
                catch (System.Data.Entity.Validation.DbEntityValidationException ex)
                {
                    foreach (var entityValidationErrors in ex.EntityValidationErrors)
                    {
                        foreach (var validationError in entityValidationErrors.ValidationErrors)
                        {
                            Console.WriteLine("Property: " + validationError.PropertyName + " Error: " + validationError.ErrorMessage);
                        }
                    }
                }

                return RedirectToAction("Index");
            }
            return View(boat);
        }

        // GET: Boat/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Boat boat = db.BoatSet.Find(id);
            if (boat == null)
            {
                return HttpNotFound();
            }
            return View(boat);
        }

        // POST: Boat/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult DeleteConfirmed(int id)
        {
            Boat boat = db.BoatSet.Find(id);
            db.BoatSet.Remove(boat);
            db.SaveChanges();
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

        // GET: Boat
        public ActionResult Sorted(int c)
        {
            var boatSet = db.BoatSet.Include(b => b.Skipper).Include(b => b.Port);
            if (c == 1)
                boatSet = db.BoatSet.OrderBy(b => b.Model);
            else if (c == 2)
                boatSet = db.BoatSet.OrderBy(b => b.Bunks);
            else if (c == 3)
                boatSet = db.BoatSet.OrderBy(b => b.Price);                
            return View(boatSet.ToList());
        }
 
    }
}
