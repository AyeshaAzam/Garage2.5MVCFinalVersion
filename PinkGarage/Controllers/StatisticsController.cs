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
           return View();
        }

        //public ActionResult Member()
        //{
        //    ViewBag.Message = "Member Statistics";
           
        //    return View();
        //}

        //public ActionResult Vehicles()
        //{
        //    ViewBag.Message = "Vehicle Statistics";
        //    return View();
        //}
    }
}