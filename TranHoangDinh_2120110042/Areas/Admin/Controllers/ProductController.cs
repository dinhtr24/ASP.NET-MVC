using PagedList;
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
    public class ProductController : Controller
    {
        ElectronicEntities obj = new ElectronicEntities();
        // GET: Admin/Product
        public ActionResult Index(string currentFilter, string SearchString, int? page)
        {
            var lstProduct = new List<Product>();
            if (SearchString != null)
            {
                page = 1;
            }
            else
            {
                SearchString = currentFilter;
            }
            if (!string.IsNullOrEmpty(SearchString))
            {
                lstProduct = obj.Products.Where(n => n.Name.Contains(SearchString)).ToList();
            }
            else
            {
                lstProduct = obj.Products.ToList();
            }
            ViewBag.CurrentFilter = SearchString;
            int pageSize = 5;
            int pageNumber = (page ?? 1);
            lstProduct = lstProduct.OrderByDescending(n => n.Id).ToList();
            return View(lstProduct.ToPagedList(pageNumber, pageSize));
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Product objProduct)
        {
            try
            {
                if (objProduct.ImageUpload != null)
                {
                    string fileName = Path.GetFileNameWithoutExtension(objProduct.ImageUpload.FileName);
                    string extension = Path.GetExtension(objProduct.ImageUpload.FileName);
                    fileName = fileName + extension;
                    objProduct.Image = fileName;
                    objProduct.ImageUpload.SaveAs(Path.Combine(Server.MapPath("~/Content/images/"), fileName));
                }
                obj.Products.Add(objProduct);
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
            var objProduct = obj.Products.Where(n => n.Id == id).FirstOrDefault();
            return View(objProduct);
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            var objProduct = obj.Products.Where(n => n.Id == id).FirstOrDefault();
            return View(objProduct);
        }

        [HttpPost]
        public ActionResult Delete(Product objPro)
        {
            var objProduct = obj.Products.Where(n => n.Id == objPro.Id).FirstOrDefault();
            obj.Products.Remove(objProduct);
            obj.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var objProduct = obj.Products.Where(n => n.Id == id).FirstOrDefault();
            return View(objProduct);
        }

        [HttpPost]
        public ActionResult Edit(Product objPro)
        {
            if (objPro.ImageUpload != null)
            {
                string fileName = Path.GetFileNameWithoutExtension(objPro.ImageUpload.FileName);
                string extension = Path.GetExtension(objPro.ImageUpload.FileName);
                fileName = fileName + extension;
                objPro.Image = fileName;
                objPro.ImageUpload.SaveAs(Path.Combine(Server.MapPath("~/Content/images/"), fileName));
            }
            obj.Entry(objPro).State = EntityState.Modified;
            obj.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}