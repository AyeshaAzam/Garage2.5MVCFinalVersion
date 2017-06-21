using PinkGarage.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PinkGarage.Controllers
{
    public class StatisticsController : Controller
    {
        private CityGarageDbContext db = new CityGarageDbContext();

        // GET: Statistics
        public ActionResult Index()
        {
            IQueryable<ParkedVehicle> model;
            List<SelectListItem> items = new List<SelectListItem>();
            items.Add(new SelectListItem { Text = "Parked Vehicle", Value = "1" });
            items.Add(new SelectListItem { Text = "Member", Value = "2" });
            ViewBag.PropertySelect = items;
            return View();
        }
    }
}