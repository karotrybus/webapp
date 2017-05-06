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
    public class SkipperController : Controller
    {
        private MarineContainer db = new MarineContainer();

        // GET: Skipper
        public ActionResult Index()
        {
            return View(db.SkipperSet.ToList());
        }

        // GET: Skipper/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Skipper skipper = db.SkipperSet.Find(id);
            if (skipper == null)
            {
                return HttpNotFound();
            }
            return View(skipper);
        }

        // GET: Skipper/SkippersBoats
        public ActionResult SkippersBoats(int? id)
        {
            return View(db.BoatSet.Where(b => b.SkipperId == id).OrderBy(b => b.Name).ToList());
        }

        // GET: Skipper/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Skipper/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Create([Bind(Include = "Id,Name,Email,PhoneNumber")] Skipper skipper)
        {
            if (ModelState.IsValid)
            {
                db.SkipperSet.Add(skipper);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(skipper);
        }

        // GET: Skipper/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Skipper skipper = db.SkipperSet.Find(id);
            if (skipper == null)
            {
                return HttpNotFound();
            }
            return View(skipper);
        }

        // POST: Skipper/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Edit([Bind(Include = "Id,Name,Email,PhoneNumber")] Skipper skipper)
        {
            if (ModelState.IsValid)
            {
                db.Entry(skipper).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(skipper);
        }

        // GET: Skipper/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Skipper skipper = db.SkipperSet.Find(id);
            if (skipper == null)
            {
                return HttpNotFound();
            }
            return View(skipper);
        }

        // POST: Skipper/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult DeleteConfirmed(int id)
        {
            Skipper skipper = db.SkipperSet.Find(id);
            db.SkipperSet.Remove(skipper);
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
