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
                    ViewBag.Error = "Sorry!!! No Vehicle with the Registration Number : " + searchString + " is currentely checkin";
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
        public ActionResult Details(int? id, string time) {
            if(id == null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ParkedVehicle parkedVehicle = db.ParkedVehicles.Find(id);
            if(parkedVehicle == null) {
                return HttpNotFound();
            }
            ViewBag.Parkedtime = time;
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

        // POST: ParkedVehicles/Receipt
        [HttpPost, ActionName("Receipt")]
        [ValidateAntiForgeryToken]
        public ActionResult ReceiptConfirmed(int id) {
            ParkedVehicle parkedVehicle = db.ParkedVehicles.Find(id);
            db.ParkedVehicles.Remove(parkedVehicle);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: ParkedVehicles/Checkin
        public ActionResult Checkin() {
            return View();
        }

        // POST: ParkedVehicles/Checkin
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Checkin([Bind(Include = "ID,RegNum,Type,EngineType, Color,Brand,Model,NumOfWheels")] ParkedVehicle parkedVehicle) {
            var vehicles = db.ParkedVehicles.Select(p => p);

            vehicles = vehicles.Where(p => p.RegNum.Equals(parkedVehicle.RegNum));
            if(vehicles.Count() > 0) {
                ModelState.AddModelError("RegNum", "A vehicle with same registration number already exists in the garage. Try with another one!");
            }

            if(ModelState.IsValid) {
                parkedVehicle.CheckInTime = DateTime.Now;
                db.ParkedVehicles.Add(parkedVehicle);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(parkedVehicle);
        }

        public ActionResult Checkout(string regnum)
        {
            ViewBag.Error = "";
            ViewBag.VehicleRegNum = regnum;
            var model = db.ParkedVehicles.Where(i => i.RegNum == regnum);
            if (!String.IsNullOrEmpty(regnum))
            {
                if (model.Count() == 0)
                {
                    ViewBag.Error = "Sorry!!! No Vehicle with the Registration Number : " + regnum + " is currentely checkin";
                    ViewBag.VehicleRegNum = "";
                }
            }
                         
            return View(model.ToList());
        }




        // GET: Employees/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ParkedVehicle parkedVehicle = db.ParkedVehicles.Find(id);
            if (parkedVehicle == null)
            {
                return HttpNotFound();
            }
            return View(parkedVehicle);
        }

        // POST: Employees/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,RegNum,Type,EngineType, Color,Brand,Model,NumOfWheels,CheckInTime")] ParkedVehicle parkedVehicle)
        {
            if (ModelState.IsValid)
            {
                db.Entry(parkedVehicle).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(parkedVehicle);
        }

        public ActionResult SearchVehicle(int? value, string SearchString)
        {
            IQueryable<ParkedVehicle> model;
            List<SelectListItem> items = new List<SelectListItem>();
            items.Add(new SelectListItem { Text = "Registration Number", Value = "1"});
            items.Add(new SelectListItem { Text = "Type", Value = "2" });
            items.Add(new SelectListItem { Text = "Color", Value = "3"});
            items.Add(new SelectListItem { Text = "Brand", Value = "4" });
            ViewBag.PropertySelect = items;
            model = db.ParkedVehicles.Where(i => i.ID == 0);
            switch (value)
            {
                case 1:
                    model = db.ParkedVehicles.Where(i => i.RegNum == SearchString);
                    break;
                case 2:
                    model = db.ParkedVehicles.Where(i => i.Type.ToString() == SearchString);
                    break;
                case 3:
                    model = db.ParkedVehicles.Where(i => i.Color == SearchString);
                    break;
                case 4:
                    model = db.ParkedVehicles.Where(i => i.Brand == SearchString);
                    break;
                default:
                    break;
            }
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
