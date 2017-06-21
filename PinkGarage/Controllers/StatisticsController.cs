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
    }
}