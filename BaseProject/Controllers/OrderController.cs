﻿using BaseProject.Models.EF;
using BaseProject.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace BaseProject.Controllers
{
    [Authorize]
    public class OrderController : BaseController<Order>
    {
        private readonly CartManager _cartManager;
        public OrderController()
        {
            _cartManager = new CartManager();

        }
        public static bool ValidateVNPhoneNumber(string phoneNumber)
        {
            phoneNumber = phoneNumber.Replace("+84", "0");
            Regex regex = new Regex(@"^(0)(86|96|97|98|32|33|34|35|36|37|38|39|91|94|83|84|85|81|82|90|93|70|79|77|76|78|92|56|58|99|59|55|87)\d{7}$");
            return regex.IsMatch(phoneNumber);
        }
        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var random = new Random();
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray()).ToUpper();
        }
        // GET: Order
        public ActionResult CheckOut()
        {
            var cart = _cartManager.GetCartItems();
            if (!User.Identity.IsAuthenticated)
            {
                // Người dùng chưa đăng nhập, chuyển hướng về trang đăng nhập
                return RedirectToAction("Login", "Account");
            }
            if (cart.Count() < 1)
            {
                return RedirectToAction("Index", "Home");
            }
            ViewBag.Carts = cart;
            return View();
        }
        [HttpPost]
        //public ActionResult CheckOut(FormCollection formCollection)
        //{
        //    List<string> errors = new List<string>();
        //    try
        //    {
        //        var CustomerName = formCollection["CustomerName"];
        //        var PhoneNumber = formCollection["PhoneNumber"];
        //        var Address = formCollection["Address"];
        //        var Payment = formCollection["Payment"];


        //        if (string.IsNullOrEmpty(CustomerName))
        //        {
        //            errors.Add("Vui lòng nhập tên.");
        //        }

        //        if (string.IsNullOrEmpty(Address))
        //        {
        //            errors.Add("Vui lòng nhập địa chỉ.");
        //        }

        //        if (ValidateVNPhoneNumber(PhoneNumber) != true)
        //        {
        //            errors.Add("Số điện thoại không hợp lệ.");
        //        }

        //        switch (Payment)
        //        {
        //            case "cash":
        //            case "momo":
        //                break;
        //            default:
        //                errors.Add("Phương thức thanh toán không hợp lệ.");
        //                break;
        //        }

        //        if (errors.Count == 0)
        //        {
        //            Order order = new Order();

        //            string code = RandomString(12);
        //            order.Code = code;
        //            order.CustomerName = CustomerName;
        //            order.PhoneNumber = PhoneNumber;
        //            order.Address = Address;
        //            order.Payment = Payment;


        //            //Add(order);
        //            Session["orderCode"] = code;
        //            // Lấy tổng tiền từ giỏ hàng
        //            var cart = _cartManager.GetCartItems();
        //            decimal totalOrder = 0;
        //            foreach (var item in cart)
        //            {
        //                var itemTotal = item.Price * item.Quantity; // giá sản phẩm


        //                totalOrder += itemTotal;
        //            }
        //            order.Total = totalOrder;
        //            Session["order"] = order;
        //            totalOrder = 0;


        //            foreach (var item in cart)
        //            {
        //                var itemTotal = item.Price * item.Quantity;
        //                foreach (var option in cart)
        //                {
        //                    itemTotal += option.Price * item.Quantity;
        //                }
        //                OrderDetail orderDetail = new OrderDetail();
        //                orderDetail.Order = order;
        //                orderDetail.ProductId = item.ProductId;

        //                orderDetail.Price = item.Price;
        //                orderDetail.Total = itemTotal;
        //                orderDetail.Quantity = item.Quantity;
        //                totalOrder += itemTotal;
        //                Context.OrderDetails.Add(orderDetail);
        //                Context.SaveChanges();
        //            }
        //            //Cập nhật tổng số tiền
        //            order.Total = totalOrder;
        //            Update(order);
        //            _cartManager.ClearCart();



        //            return RedirectToAction("CompleteOrder", "Order");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        errors.Add(ex.Message);
        //    }
        //    TempData["Errors"] = errors;
        //    return RedirectToAction("CheckOut", "Order");
        //}

        public ActionResult CheckOut(FormCollection formCollection)
        {
            List<string> errors = new List<string>();
            try
            {
                var CustomerName = formCollection["CustomerName"];
                var PhoneNumber = formCollection["PhoneNumber"];
                var Address = formCollection["Address"];
                var Payment = formCollection["Payment"];


                if (string.IsNullOrEmpty(CustomerName))
                {
                    errors.Add("Vui lòng nhập tên.");
                }

                if (string.IsNullOrEmpty(Address))
                {
                    errors.Add("Vui lòng nhập địa chỉ.");
                }

                if (ValidateVNPhoneNumber(PhoneNumber) != true)
                {
                    errors.Add("Số điện thoại không hợp lệ.");
                }

                switch (Payment)
                {
                    case "cash":
                    case "momo":
                        break;
                    default:
                        errors.Add("Phương thức thanh toán không hợp lệ.");
                        break;
                }

                if (errors.Count == 0)
                {
                    Order order = new Order();

                    string code = RandomString(12);
                    order.Code = code;
                    order.CustomerName = CustomerName;
                    order.PhoneNumber = PhoneNumber;
                    order.Address = Address;
                    order.Payment = Payment;


                    //Add(order);
                    Session["orderCode"] = code;
                    // Lấy tổng tiền từ giỏ hàng
                    var cart = _cartManager.GetCartItems();
                    decimal totalOrder = 0;
                    foreach (var item in cart)
                    {
                        var itemTotal = item.Price * item.Quantity; // giá sản phẩm


                        totalOrder += itemTotal;
                    }
                    order.Total = totalOrder;
                    Session["order"] = order;
                    
                    switch (Payment)
                    {
                        case "momo":
                            return RedirectToAction("MomoPay", "Pay");
                        default:
                            totalOrder = 0;
                            foreach (var item in cart)
                            {
                                var itemTotal = item.Price * item.Quantity;
                                foreach (var option in cart)
                                {
                                    itemTotal += option.Price * item.Quantity;
                                }
                                OrderDetail orderDetail = new OrderDetail();
                                orderDetail.Order = order;
                                orderDetail.ProductId = item.ProductId;

                                orderDetail.Price = item.Price;
                                orderDetail.Total = itemTotal;
                                orderDetail.Quantity = item.Quantity;
                                totalOrder += itemTotal;
                                Context.OrderDetails.Add(orderDetail);
                                Context.SaveChanges();
                            }
                            //Cập nhật tổng số tiền
                            order.Total = totalOrder;
                            Update(order);
                            _cartManager.ClearCart();
                            break;
                    }    
                    return RedirectToAction("CompleteOrder", "Order");
                }
            }
            catch (Exception ex)
            {
                errors.Add(ex.Message);
            }
            TempData["Errors"] = errors;
            return RedirectToAction("CheckOut", "Order");
        }
       
        //[Route("Order/SearchOrder")]
        //public ActionResult SearchOrder()
        //{
        //    return View();
        //}
        [Route("Order/SearchOrder/{orderCode}")]
        public ActionResult SearchOrder(string orderCode)
        {

            if (orderCode == null)
            {
                return View();
            }
            var order = Context.Orders.FirstOrDefault(p => p.Code == orderCode);
            //var find =  from o in Context.Orders join od in Context.OrderDetails on o.Id equals od.Id where o.Code== orderCode
            //            select o;


            if (order != null)
            {
                var orderDetails = Context.OrderDetails.Where(p => p.Order.Code == orderCode).ToList();
                ViewBag.OrderDetails = orderDetails;
                return View(order);
            }
            return View();
        }

        public ActionResult CompleteOrder()
        {
            return View();
        }
    }
}
