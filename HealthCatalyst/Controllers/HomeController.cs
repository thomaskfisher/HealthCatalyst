using HealthCatalyst.DAL;
using HealthCatalyst.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace HealthCatalyst.Controllers
{
    public class HomeController : Controller
    {
        HealthCatalystContext db = new HealthCatalystContext();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Contact()
        {
            return View();
        }
    }
}