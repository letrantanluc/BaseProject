using BaseProject.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BaseProject.Models.EF;
using BaseProject.Controllers;

namespace BaseProject.Areas.Admin.Controllers
{
    //[Authorize(Roles = "adminTL")]
    public class ManageRoleController : BaseController<Role>
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

        //setrole
        public ActionResult SetRole()
        {
            var roles = db.Roles.ToList();
            var users = db.Users.ToList();
            var roleUserList = new List<AspNetUserRoles>();

            foreach (var role in roles)
            {
                // Get all the users assigned to the role
                var userIds = db.Set<IdentityUserRole>()
                    .Where(ur => ur.RoleId == role.Id)
                    .Select(ur => ur.UserId)
                    .ToList();

                // Get all the users assigned to the role
                var roleUsers = db.Set<ApplicationUser>()
                    .Where(u => userIds.Contains(u.Id))
                    .ToList();

                // Add the role-user mappings to the list
                roleUserList.AddRange(roleUsers.Select(u => new AspNetUserRoles { RoleId = role.Id, UserId = u.Id }));
            }
            ViewBag.Roles = roles;
            ViewBag.Users = users;
            return View(roleUserList);
        }
        [HttpPost]
        public ActionResult SetRole(FormCollection formCollection)
        {
            List<string> errors = new List<string>();
            try
            {
                var RoleId = formCollection["RoleId"];
                var UserId = formCollection["UserId"];
                var getRole = db.Roles.FirstOrDefault(x => x.Id == RoleId);
                var getUser = db.Users.FirstOrDefault(x => x.Id == UserId);
                if (getRole == null)
                {
                    errors.Add("Không tìm thấy Role này.");
                }
                if (getUser == null)
                {
                    errors.Add("Không tìm thấy User này.");
                }
                if (errors.Count == 0)
                {
                    IdentityUserRole userRole = new IdentityUserRole();
                    userRole.UserId = getUser.Id;
                    userRole.RoleId = getRole.Id;
                    getUser.Roles.Add(userRole);
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                errors.Add(ex.Message);
            }
            TempData["Errors"] = errors;
            return RedirectToAction("SetRole", "ManageRole");
        }
        [HttpPost]
        public ActionResult DeleteSetRole(Guid? UserId, Guid? RoleId)
        {
            try
            {
                if (RoleId == null || UserId == null)
                {
                    return Json(new { success = false, message = "Vui lòng nhập đầy đủ thông tin." });
                }
                var getRole = db.Roles.FirstOrDefault(x => x.Id.Equals(RoleId.ToString()));
                var getUser = db.Users.FirstOrDefault(x => x.Id.Equals(UserId.ToString()));

                if (getRole == null)
                {
                    return Json(new { success = false, message = "Không tìm thấy Role này." });
                }
                if (getUser == null)
                {
                    return Json(new { success = false, message = "Không tìm thấy User này." });
                }
                var userRole = db.Set<IdentityUserRole>().FirstOrDefault(ur => ur.UserId == getUser.Id && ur.RoleId == getRole.Id);

                if (userRole != null)
                {
                    // Remove the record from the AspNetUserRoles table
                    db.Set<IdentityUserRole>().Remove(userRole);

                    // Save the changes to the database
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
            return Json(new { success = true, message = "Xóa set role thành công" });
        }






    }
}