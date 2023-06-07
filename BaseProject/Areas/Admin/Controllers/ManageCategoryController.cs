using BaseProject.Controllers;
using BaseProject.Models;
using BaseProject.Models.EF;
using PagedList;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace BaseProject.Areas.Admin.Controllers
{
    //[Authorize(Roles = "adminTL")]
    public class ManageCategoryController : BaseController<Category>
    {
       
        ApplicationDbContext db = new ApplicationDbContext();
        // GET: Admin/Category
        //public ActionResult Index()
        //{

        //    return View(db.Categories.ToList());
        //}

        public ActionResult Index(string Searchtext, int? page)
        {
            var pageSize = 10;
            if (page == null)
            {
                page = 1;
            }
            IEnumerable<Category> items = db.Categories.OrderByDescending(x => x.Id);
            if (!string.IsNullOrEmpty(Searchtext))
            {

                items = items.Where(x => x.Alias.Contains(Searchtext) || x.CategoryName.Contains(Searchtext));
            }
            var pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;
            items = items.ToPagedList(pageIndex, pageSize);
            ViewBag.PageSize = pageSize;
            ViewBag.Page = page;
            return View(items);
        }

        public ActionResult Create()
        {
            return View();
        }

      
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,CategoryName,Description,CreatedAt,UpdatedAt,Images")] Category category)
        {
            if (ModelState.IsValid)
            {
                category.CreatedAt= DateTime.Now;
                category.UpdatedAt = DateTime.Now;
                category.Alias = BaseProject.Models.Common.Filter.FilterChar(category.CategoryName);
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
        public ActionResult Edit([Bind(Include = "Id,CategoryName,Description,CreatedAt,UpdatedAt,Images")] Category category)
        {
            if (ModelState.IsValid)
            {
                //db.Categories.Attach(category);
                category.UpdatedAt = DateTime.Now;
                category.Alias = BaseProject.Models.Common.Filter.FilterChar(category.CategoryName);
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


        [HttpPost]
        public ActionResult DeleteAll(string ids)
        {
            if (!string.IsNullOrEmpty(ids))
            {
                var items = ids.Split(',');
                if (items != null && items.Any())
                {
                    foreach (var item in items)
                    {
                        var obj = db.Categories.Find(Convert.ToInt32(item));
                        db.Categories.Remove(obj);
                        db.SaveChanges();
                    }
                }
                return Json(new { success = true });
            }
            return Json(new { success = false });
        }
    }
}