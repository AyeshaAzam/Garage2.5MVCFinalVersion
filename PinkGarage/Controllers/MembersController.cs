﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PinkGarage.Models;

namespace PinkGarage.Controllers
{
    public class MembersController : Controller
    {
        private CityGarageDbContext db = new CityGarageDbContext();

        //// GET: Members
        //public ActionResult Index()
        //{
        //    return View(db.Members.ToList());
        //}


        ////search by FirstName method
        public ActionResult Index(string SearchFName)
        {
            ViewBag.Error = "";
            var members = db.Members.Select(m => m);

            if (!String.IsNullOrEmpty(SearchFName))
            {
                members = db.Members.Where(m => m.FName.Contains(SearchFName)
                 || m.LName.Contains(SearchFName));
                if (members.Count() == 0)
                    ViewBag.Error = "Name ['" + SearchFName + "'] Not found! ";
            }

            return View(members.ToList());
        }


        // GET: Members/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Member member = db.Members.Find(id);
            if (member == null)
            {
                return HttpNotFound();
            }
            return View(member);
        }

        // GET: Members/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Members/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MemberId,FName,LName,Address,PhoneNumber")] Member member)
        {
            if (ModelState.IsValid)
            {
                db.Members.Add(member);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(member);
        }

        // GET: Members/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Member member = db.Members.Find(id);
            if (member == null)
            {
                return HttpNotFound();
            }
            return View(member);
        }

        // POST: Members/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MemberId,FName,LName,Address,PhoneNumber")] Member member)
        {
            if (ModelState.IsValid)
            {
                db.Entry(member).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(member);
        }

        // GET: Members/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Member member = db.Members.Find(id);
            if (member == null)
            {
                return HttpNotFound();
            }
            return View(member);
        }

        // POST: Members/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
             Member member = db.Members.Find(id);
            var idmember = db.Vehicles.Where(p => p.MemberId == id);
            if(idmember.Count() == 0)
            {
                db.Members.Remove(member);
                db.SaveChanges();
            }
            else
            {
                TempData["ErrorMessage"] = "Sorry!!!. " + member.FName + " " + member.LName + " has vehicle(s) parked. Please Check out the Vehicle(s) before deleting the membership";
            }
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
