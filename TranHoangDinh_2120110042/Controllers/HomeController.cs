using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using TranHoangDinh_2120110042.Context;
using TranHoangDinh_2120110042.Models;

namespace TranHoangDinh_2120110042.Controllers
{
    public class HomeController : Controller
    {
        ElectronicEntities objElec = new ElectronicEntities();
        public ActionResult Index()
        {
            HomeModel model = new HomeModel();
            model.ListProduct = objElec.Products.ToList();
            model.ListCategory = objElec.Categories.ToList();
            return View(model);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }


    }
}