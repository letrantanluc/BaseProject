using BaseProject.Models;
using BaseProject.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace BaseProject.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index()
        {
            List<Product> products = db.Products.ToList();
            ViewBag.Products = products;
            return View(db.Categories.ToList());
           
        }


    }
}