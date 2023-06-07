using BaseProject.Controllers;
using BaseProject.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BaseProject.Areas.Admin.Controllers
{
    //[Authorize(Roles = "adminTL")]
    public class ManageProductController : BaseController<Product>
    {
        // GET: Admin/Product
        public ActionResult Index()
        {
            return View();
        }
    }
}