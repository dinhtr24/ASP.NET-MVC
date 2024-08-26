using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TranHoangDinh_2120110042.Context;

namespace TranHoangDinh_2120110042.Areas.Admin.Controllers
{
    public class CategoryController : Controller
    {
        ElectronicEntities obj = new ElectronicEntities();
        // GET: Admin/Category
        public ActionResult Index()
        {
            return View(obj.Categories.ToList());
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Category objCate)
        {
            try
            {
                if (objCate.ImageUpload != null)
                {
                    string fileName = Path.GetFileNameWithoutExtension(objCate.ImageUpload.FileName);
                    string extension = Path.GetExtension(objCate.ImageUpload.FileName);
                    fileName = fileName + extension;
                    objCate.CateImage = fileName;
                    objCate.ImageUpload.SaveAs(Path.Combine(Server.MapPath("~/Content/images/"), fileName));
                }
                obj.Categories.Add(objCate);
                obj.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                return RedirectToAction("Index");
            }
            return View();
        }

        [HttpGet]
        public ActionResult Details(int id)
        {
            var objCate = obj.Categories.Where(n => n.CateId == id).FirstOrDefault();
            return View(objCate);
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            var objCate = obj.Categories.Where(n => n.CateId == id).FirstOrDefault();
            return View(objCate);
        }

        [HttpPost]
        public ActionResult Delete(Category objCa)
        {
            var objCate = obj.Categories.Where(n => n.CateId == objCa.CateId).FirstOrDefault();
            obj.Categories.Remove(objCate);
            obj.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var objCate = obj.Categories.Where(n => n.CateId == id).FirstOrDefault();
            return View(objCate);
        }

        [HttpPost]
        public ActionResult Edit(Category objCa)
        {
            if (objCa.ImageUpload != null)
            {
                string fileName = Path.GetFileNameWithoutExtension(objCa.ImageUpload.FileName);
                string extension = Path.GetExtension(objCa.ImageUpload.FileName);
                fileName = fileName + extension;
                objCa.CateImage = fileName;
                objCa.ImageUpload.SaveAs(Path.Combine(Server.MapPath("~/Content/images/"), fileName));
            }
            obj.Entry(objCa).State = EntityState.Modified;
            obj.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}