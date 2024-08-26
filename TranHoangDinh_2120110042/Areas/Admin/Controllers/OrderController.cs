using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.IO;
using System.Web;
using System.Web.Mvc;
using TranHoangDinh_2120110042.Context;

namespace TranHoangDinh_2120110042.Areas.Admin.Controllers
{
    public class OrderController : Controller
    {
        ElectronicEntities objElec = new ElectronicEntities();
        // GET: Admin/Order
        public ActionResult Index()
        {
            return View(objElec.Orders.ToList());
        }

        [HttpGet]
        public ActionResult Details(int id)
        {
            var objOrder = objElec.Orders.Where(n => n.OrderId == id).FirstOrDefault();
            return View(objOrder);
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            var objOrder = objElec.Orders.Where(n => n.OrderId == id).FirstOrDefault();
            return View(objOrder);
        }

        [HttpPost]
        public ActionResult Delete(Order objOr)
        {
            var objOrder = objElec.Orders.Where(n => n.OrderId == objOr.OrderId).FirstOrDefault();
            objElec.Orders.Remove(objOrder);
            objElec.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}