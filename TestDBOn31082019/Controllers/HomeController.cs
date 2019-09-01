using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Oracle.ManagedDataAccess.Client;
using TestDBOn31082019.Models;

namespace TestDBOn31082019.Controllers
{
    public class HomeController : Controller
    {
        private List<Student> students { get; set; }

        public ActionResult Index()
        {
            List<string> selector = new List<string>()
            {
                Student.Prop_Course,
                Student.Prop_ID
            };

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}