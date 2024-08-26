using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TranHoangDinh_2120110042.Context;
using TranHoangDinh_2120110042.Models;

namespace TranHoangDinh_2120110042.Controllers
{
    public class PaymentController : Controller
    {
        ElectronicEntities objElec = new ElectronicEntities();  
        // GET: Payment
        public ActionResult Pay()
        {
            if (Session["UserId"] == null)
            {
                return RedirectToAction("Login", "User");
            }
            else
            {
                var lstCart = (List<CartModel>)Session["cart"];
                Order objOrder = new Order();
                objOrder.OrderName = "Order - " + DateTime.Now.ToString("yyyyMMddHHmmss");
                objOrder.OrderUserId = int.Parse(Session["UserId"].ToString());
                objOrder.OrderCreatedOnUtc = DateTime.Now;
                objOrder.OrderStatus = 1;
                objElec.Orders.Add(objOrder);
                objElec.SaveChanges();

                int intOrderId = objOrder.OrderId;
                List<OrderDetail> lstOrderDetail = new List<OrderDetail>();

                if (lstCart == null || lstCart.Count == 0)
                {
                    return RedirectToAction("PayFail", "Payment");
                }
                else
                {

                    foreach (var item in lstCart)
                    {
                        OrderDetail obj = new OrderDetail();
                        obj.Quantity = item.Quantity;
                        obj.OrderIdDe = intOrderId;
                        obj.ProductIdDe = item.Product.Id;
                        lstOrderDetail.Add(obj);
                    }
                    objElec.OrderDetails.AddRange(lstOrderDetail);
                    objElec.SaveChanges();
                }
            }
            return View();
        }

        public ActionResult PayFail()
        {
            return View();
        }
    }
}