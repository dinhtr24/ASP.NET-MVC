using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TranHoangDinh_2120110042.Context;

namespace TranHoangDinh_2120110042.Controllers
{
    public class ProductController : Controller
    {
        ElectronicEntities obj = new ElectronicEntities();
        // GET: Product
        public ActionResult Detail(int id)
        {
            var pro = obj.Products.Find(id);
            return View(pro);
        }
    }
}