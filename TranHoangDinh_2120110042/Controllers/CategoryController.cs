using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TranHoangDinh_2120110042.Context;
using TranHoangDinh_2120110042.Models;

namespace TranHoangDinh_2120110042.Controllers
{
    public class CategoryController : Controller
    {
        // GET: Category
        ElectronicEntities objElec = new ElectronicEntities();
        public ActionResult Cate()
        {
            HomeModel objHome = new HomeModel();
            objHome.ListProduct = objElec.Products.ToList();
            objHome.ListCategory = objElec.Categories.ToList();
            return View(objHome);
        }

        public ActionResult CateProduct(int id)
        {
            var lstPro = objElec.Products.ToList().Where(n => n.CategoryId == id);
            return View(lstPro);
        }

        [HttpGet]
        public ActionResult CateProductBySearchString(string currentFilter, string SearchString, int? page)
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
                lstProduct = objElec.Products.Where(n => n.Name.Contains(SearchString)).ToList();
            }
            else
            {
                lstProduct = objElec.Products.ToList();
            }
            ViewBag.CurrentFilter = SearchString;
            int pageSize = 4;
            int pageNumber = (page ?? 1);
            lstProduct = lstProduct.OrderBy(n => n.Id).ToList();
            return View(lstProduct.ToPagedList(pageNumber, pageSize));
        }
    }
}