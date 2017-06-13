using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PinkGarage.Controllers {
    public class HomeController : Controller {
        public ActionResult Index() {
            return View();
        }

        public ActionResult About() {
            ViewBag.Message = "Welcome to Pink Garage";

            return View();
        }

        public ActionResult Contact() {
            ViewBag.Message = "You are Welcome to contact us at 070-458778";

            return View();
        }
    }
}