using BaseProject.Models;
using BaseProject.Models.EF;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace BaseProject.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class CategoryController : Controller
    {
       
        ApplicationDbContext db = new ApplicationDbContext();
        // GET: Admin/Category
        public ActionResult Index()
        {

            return View(db.Categories.ToList());
        }

        public ActionResult Create()
        {
            return View();
        }

      
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,CategoryName,Description,CreatedAt,UpdatedAt")] Category category)
        {
            if (ModelState.IsValid)
            {
                category.CreatedAt= DateTime.Now;
                category.UpdatedAt = DateTime.Now;
                db.Categories.Add(category);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(category);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = db.Categories.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,CategoryName,Description,CreatedAt,UpdatedAt")] Category category)
        {
            if (ModelState.IsValid)
            {
                //db.Categories.Attach(category);
                category.UpdatedAt = DateTime.Now;
                db.Entry(category).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(category);
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            var item=db.Categories.Find(id);
            if (item != null)
            {
             
                db.Categories.Remove(item);
                db.SaveChanges();
                return Json(new {success=true });
            }
            return Json(new { success = false });
        }
    }
}