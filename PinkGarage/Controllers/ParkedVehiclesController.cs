﻿using System;
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

        //// GET: ParkedVehicles
        //public ActionResult Index()
        //{
        //    return View(db.ParkedVehicles.ToList());
        //}





        // GET: ParkedVehicles // adding Sorting Functionality
        public ActionResult Index(string orderBy, string searchString) {
            ViewBag.Error = "";
            ViewBag.SortByRegNum = orderBy == "RegNum" ? "RegNum_desc" : "RegNum";
            ViewBag.SortByType = orderBy == "Type" ? "Type_desc" : "Type";
            ViewBag.SortByColor = orderBy == "Color" ? "Color_desc" : "Color";
            ViewBag.SortByBrand = orderBy == "Brand" ? "Brand_desc" : "Brand";
            ViewBag.SortByCheckIn = orderBy == "CheckInTime" ? "CheckInTime_desc" : "CheckInTime";

            var vehicles = db.ParkedVehicles.Select(p => p);


            //search by RegNum 
            if(!String.IsNullOrEmpty(searchString)) {
                vehicles = vehicles.Where(p => p.RegNum.Equals(searchString));
                if(vehicles.Count() == 0)
                    ViewBag.Error = "RegNumber is Invalid, Please try again!";
                  //ModelState.AddModelError("", "");

            }


            switch(orderBy) {
                case "RegNum":
                    vehicles = vehicles.OrderBy(p => p.RegNum);
                    break;
                // the default sort order is ascending
                case "RegNum_desc":
                    vehicles = vehicles.OrderByDescending(p => p.RegNum);
                    break;
                case "Type":
                    vehicles = vehicles.OrderBy(p => p.Type);
                    break;
                case "Type_desc":
                    vehicles = vehicles.OrderByDescending(p => p.Type);
                    break;

                case "Color":
                    vehicles = vehicles.OrderBy(p => p.Color);
                    break;
                case "Color_desc":
                    vehicles = vehicles.OrderByDescending(p => p.Color);
                    break;

                case "Brand":
                    vehicles = vehicles.OrderBy(p => p.Brand);
                    break;
                case "Brand_desc":
                    vehicles = vehicles.OrderByDescending(p => p.Brand);
                    break;
                case "CheckInTime":
                    vehicles = vehicles.OrderBy(p => p.CheckInTime);
                    break;
                case "CheckInTime_desc":
                    vehicles = vehicles.OrderByDescending(p => p.CheckInTime);
                    break;

                default:
                    break;
            }



            return View(vehicles.ToList());
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
            db.ParkedVehicles.Remove(parkedVehicle);
            db.SaveChanges();


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
            var vehicles = db.ParkedVehicles.Select(p => p);

            vehicles = vehicles.Where(p => p.RegNum.Equals(parkedVehicle.RegNum));
            if(vehicles.Count() > 0) {
                ModelState.AddModelError("RegNum", "The registration number already exists. Try with another one!");
            }

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

        public ActionResult Checkout(string regnum) {
            var model = db.ParkedVehicles.Where(i => i.RegNum == regnum);
            ViewBag.VehicleRegNum = regnum;
            return View(model.ToList());
        }

        protected override void Dispose(bool disposing) {
            if(disposing) {

                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
