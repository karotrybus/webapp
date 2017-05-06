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
    public class PortController : Controller
    {
        private MarineContainer db = new MarineContainer();

        // GET: Port
        public ActionResult Index()
        {
            return View(db.PortSet.ToList());
        }

        // GET: Port/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Port port = db.PortSet.Find(id);
            if (port == null)
            {
                return HttpNotFound();
            }
            return View(port);
        }

        // GET: Port
        public ActionResult PortsBoats(int? id)
        {
            return View(db.BoatSet.Where(b => b.PortId == id).OrderBy(b => b.Name).ToList());
        }

        public ActionResult PortsSkippers(int? id)
        {
            //             var skipperSet = db.PortSet.Find(id).SkipperSet;
            //   return View(db.SkipperSet.Where(s => s.PortSet.Where(p => p.Id) == id).OrderBy(s => s.Name).ToList());
            //        return View(db.SkipperSet.Where(db.PortSet.Find(id)).OrderBy(s => s.Name).ToList());
            return View(db.PortSet.Find(id).SkipperSet.OrderBy(s => s.Name).ToList());
        }

        // GET: Port/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Port/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Create([Bind(Include = "Id,Location,Spots,Email")] Port port)
        {
            if (ModelState.IsValid)
            {
                db.PortSet.Add(port);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(port);
        }

        // GET: Port/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Port port = db.PortSet.Find(id);
            if (port == null)
            {
                return HttpNotFound();
            }
            return View(port);
        }

        // POST: Port/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Edit([Bind(Include = "Id,Location,Spots,Email")] Port port)
        {
            if (ModelState.IsValid)
            {
                db.Entry(port).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(port);
        }

        // GET: Port/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Port port = db.PortSet.Find(id);
            if (port == null)
            {
                return HttpNotFound();
            }
            return View(port);
        }

        // POST: Port/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult DeleteConfirmed(int id)
        {
            Port port = db.PortSet.Find(id);
            db.PortSet.Remove(port);
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
    }
}
