using Shop.DAL;
using Shop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Shop.Controllers
{
    public class HomeController : Controller
    {
        private CoursesContext db = new CoursesContext();

        public ActionResult Index()
        {
            var ListCategory = db.Categories.ToList();

            return View();
        }
    }
}