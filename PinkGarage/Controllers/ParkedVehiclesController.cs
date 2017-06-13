using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PinkGarage.Models;

namespace PinkGarage.Controllers {
    public class ParkedVehiclesController : Controller {
        private GarageDbContext db = new GarageDbContext();

        // GET: ParkedVehicles
        public ActionResult Index() {
            return View(db.ParkedVehicles.ToList());
        }

        // GET: ParkedVehicles/Details/5
        public ActionResult Details(int? id) {
            if(id == null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ParkedVehicle parkedVehicle = db.ParkedVehicles.Find(id);
            if(parkedVehicle == null) {
                return HttpNotFound();
            }
            return View(parkedVehicle);
        }
        // GET: ParkedVehicles/checkout
        public ActionResult Receipt(int? id) {
            if(id == null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ParkedVehicle parkedVehicle = db.ParkedVehicles.Find(id);
            if(parkedVehicle == null) {
                return HttpNotFound();
            }

            return View(parkedVehicle);
        }



        // POST: ParkedVehicles/Delete/5
        [HttpPost, ActionName("Receipt")]
        [ValidateAntiForgeryToken]
        public ActionResult ReceiptConfirmed(int id) {
            ParkedVehicle parkedVehicle = db.ParkedVehicles.Find(id);
                       db.ParkedVehicles.Remove(parkedVehicle);
                     db.SaveChanges();

            
            return RedirectToAction("Index");
        }

        // GET: ParkedVehicles/Create
        public ActionResult Create() {
            return View();
        }

        // POST: ParkedVehicles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,RegNum,Type,EngineType, Color,Brand,Model,NumOfWheels")] ParkedVehicle parkedVehicle) {
            if(ModelState.IsValid) {
                parkedVehicle.CheckInTime = DateTime.Now;
                db.ParkedVehicles.Add(parkedVehicle);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(parkedVehicle);
        }

        // GET: ParkedVehicles/Edit/5
        public ActionResult Edit(int? id) {
            if(id == null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ParkedVehicle parkedVehicle = db.ParkedVehicles.Find(id);
            if(parkedVehicle == null) {
                return HttpNotFound();
            }
            return View(parkedVehicle);
        }

        // POST: ParkedVehicles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,RegNum,Type,EngineType,Color,Brand,Model,NumOfWheels")] ParkedVehicle parkedVehicle) {
            if(ModelState.IsValid) {
                db.Entry(parkedVehicle).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(parkedVehicle);
        }

        // GET: ParkedVehicles/Delete/5
        public ActionResult Delete(int? id) {
            if(id == null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ParkedVehicle parkedVehicle = db.ParkedVehicles.Find(id);
            if(parkedVehicle == null) {
                return HttpNotFound();
            }
            return View(parkedVehicle);
        }



        // POST: ParkedVehicles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id) {
            ParkedVehicle parkedVehicle = db.ParkedVehicles.Find(id);
            DateTime inTime = parkedVehicle.CheckInTime;

                       db.ParkedVehicles.Remove(parkedVehicle);
                       db.SaveChanges();
            return RedirectToAction("Index", inTime);
        }

        public ActionResult Checkout(string regnum)
        {
            var model = db.ParkedVehicles.Where(i => i.RegNum == regnum);
            ViewBag.VehicleRegNum = regnum;
            return View(model.ToList());
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
