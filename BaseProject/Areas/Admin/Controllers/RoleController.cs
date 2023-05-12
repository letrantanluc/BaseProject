using BaseProject.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BaseProject.Areas.Admin.Controllers
{
    public class RoleController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Admin/Role
        public ActionResult Index()
        {
            var items = db.Roles.ToList();
            return View(items);
        }
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IdentityRole model)
        {
            if (ModelState.IsValid)
            {
                var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));
                roleManager.Create(model);
                return RedirectToAction("Index");
            }
            return View(model);
        }
        public ActionResult Delete(Guid? Id)
        {
            if (Id == null)
            {
                return Json(new { success = false, message = "ID không hợp lệ" });
            }
            var role = db.Roles.FirstOrDefault(x => x.Id.Equals(Id.ToString()));
            if (role == null)
            {
                return Json(new { success = false, message = "Role không tồn tại" });
            }
            try
            {
                db.Roles.Remove(role); // Xóa role
                db.SaveChanges(); // Lưu thay đổi vào cơ sở dữ liệu

                return Json(new { success = true, message = "Xóa role thành công" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Lỗi xóa role: " + ex.Message });
            }
        }
        

    }
}