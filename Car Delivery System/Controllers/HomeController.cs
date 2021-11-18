using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Car_Delivery_System.CommonEntities;

namespace Car_Delivery_System.Controllers
{
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            if (!validateLoggedInUser())
            {
                return RedirectToAction("Login", "Account");
            }
            return View();
        }

        public ActionResult ErrorPage(string err)
        {
            ViewBag.message = err;
            return View();
        }
    }
}