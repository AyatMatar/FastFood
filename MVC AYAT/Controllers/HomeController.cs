using MVC_AYAT.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC_AYAT.Controllers
{
    public class  HomeController : Controller
    {
        PizzaHatEntities pizzaHatdata = new PizzaHatEntities();
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Regstration()
        {
            UserModel Model = new UserModel();
            return View(Model);
        }
        [HttpPost]
        public ActionResult Regstration(UserModel model)
        {
            var OidRecord = pizzaHatdata.Users.Where(x=>x.Name == model.Name && x.Mobilenumber == model.Mobilenumber && x.UserType== "Normal").FirstOrDefault();
            if (OidRecord == null)
            {
                User NewUse = new User();
                NewUse.Name = model.Name;
                NewUse.Password = model.Password;
                NewUse.Mobilenumber = model.Mobilenumber;
                NewUse.UserType = "Normal";
                pizzaHatdata.Users.Add(NewUse);
                pizzaHatdata.SaveChanges();
                return RedirectToAction("login");
            }
            model.Status = "you are Aiready Registration";
            return View(model);
        }
        [HttpGet]
        public ActionResult Login()
        {
            UserModel Model = new UserModel();

            return View(Model);
        }

      
        [HttpPost]
        public ActionResult Login(UserModel Model)
        {
            var UserRecord = pizzaHatdata.Users.Where(x => x.Name == Model.Name && x.Password == Model.Password).FirstOrDefault();
            if (UserRecord != null)
            {
                Session["userinfo"] = UserRecord;
                if(UserRecord.UserType== "Normal")
                {
                    return RedirectToAction("menu");
                }
                else
                {
                    return RedirectToAction("Index","Item");
                }
            }
            Model.Status = "user name or passowrd is wrong";
            return View(Model);
        }

        [HttpPost]
        public void AddcortItem(CartItem cartItem)
        {
            var DBOrderRecoed = pizzaHatdata.Orders.Where(x => x.UserID == cartItem.UserID && x.TrxnStatus == "temp").FirstOrDefault();
            int OrderID = -1;
            if (DBOrderRecoed == null)
            {
                Order Myorder = new Order();
                Myorder.Checkout = DateTime.Now;
                Myorder.TrxnStatus = "temp";
                Myorder.UserID = cartItem.UserID;
                pizzaHatdata.Orders.Add(Myorder);
                pizzaHatdata.SaveChanges();
                OrderID = Myorder.OrderID;
            }
            else
            {
                OrderID = DBOrderRecoed.OrderID;
            }

            double OneItemPrice = Convert.ToDouble(pizzaHatdata.ItemSizePrics.Where(X => X.ItemSizePricID == cartItem.ItemSizePrice).FirstOrDefault().pric);
            double TotalPrice = OneItemPrice * cartItem.Quantity;
            Orderdetal orderdetal = new Orderdetal();
            orderdetal.ItemSizePriceID = cartItem.ItemSizePrice;
            


            var DBDetils = pizzaHatdata.Orderdetals.Where(x => x.ItemSizePriceID == cartItem.ItemSizePrice && x.OrderID == OrderID).FirstOrDefault();
            if (DBDetils == null)
            {
                orderdetal.Quontity = cartItem.Quantity;
                orderdetal.Price = TotalPrice;

            }
            else
            {
                orderdetal.Quontity = (cartItem.Quantity + DBDetils.Quontity);
                orderdetal.Price = (TotalPrice + DBDetils.Price);

            }

            orderdetal.OrderID = OrderID;
            if (DBDetils != null)
            {
                pizzaHatdata.Orderdetals.Remove(DBDetils);
            }

            pizzaHatdata.Orderdetals.Add(orderdetal);
             pizzaHatdata.SaveChanges();


        }
        [HttpGet]
        public ActionResult logout()
        {
            Session["userinfo"] = null;
            return RedirectToAction("Index");
        }
        public ActionResult menu()
        {
           
            List<Category> categories = pizzaHatdata.Categories.ToList();
            return View(categories);
        }

        public ActionResult ShowCartItems(int OrderID)
        {
            var Orderdetals = pizzaHatdata.Orderdetals.Where(x => x.OrderID == OrderID).ToList();

            return View(Orderdetals);
        }

        [HttpGet]
        public void  RemoveRow (int OrderdetalsID)
        {
            Orderdetal DeletOrder = pizzaHatdata.Orderdetals.Where(x =>x.OrderdetalsID == OrderdetalsID).FirstOrDefault();
            if (DeletOrder != null)
            {
                pizzaHatdata.Orderdetals.Remove(DeletOrder);
                pizzaHatdata.SaveChanges();
            }


        }
        [HttpGet]
        public void AddQuantity(int OrderdetalsID)
        {
            Orderdetal Record = pizzaHatdata.Orderdetals.Where(x => x.OrderdetalsID == OrderdetalsID).FirstOrDefault();
            if (Record!= null)
            {
                Record.Quontity = Record.Quontity + 1;
                Record.Price = Record.Price + Convert.ToDouble(Record.ItemSizePric.pric);
                pizzaHatdata.SaveChanges();
            }


        }
        [HttpGet]
        public void RemovQuantity(int OrderdetalsID)
        {
            Orderdetal Record = pizzaHatdata.Orderdetals.Where(x => x.OrderdetalsID == OrderdetalsID).FirstOrDefault();
            if (Record != null)
            {
                if (Record.Quontity == 1)
                {
                    pizzaHatdata.Orderdetals.Remove(Record);
                }
                else
                {
                    Record.Quontity = Record.Quontity - 1;
                    Record.Price = Record.Price - Convert.ToDouble(Record.ItemSizePric.pric);
                }
                pizzaHatdata.SaveChanges();
            }


        }

        [HttpGet]
        public void Checkout(int OrderID)
        {
            var order = pizzaHatdata.Orders.Where(x => x.OrderID == OrderID).FirstOrDefault();
            if (order!= null)
            {
                order.TrxnStatus = "Pending";
                order.Checkout = DateTime.Now;
                pizzaHatdata.SaveChanges();
            }


        }

        [HttpGet]
        public ActionResult About()
        {

            return View();

        }

        public ActionResult Orders()
        {
            var Orders = pizzaHatdata.Orders.Where(x => x.TrxnStatus == "Pending").ToList();
            return View(Orders);
        }
        public ActionResult Details(int OrderID)
        {
            var OrderDetails = pizzaHatdata.Orderdetals.Where(x => x.OrderID == OrderID).ToList();
            return View(OrderDetails);
        }
        [HttpGet]
        public ActionResult Delete(int id)
        {
            var DeleteOrderdetals = pizzaHatdata.Orderdetals.Where(x => x.OrderID == id).ToList();
            pizzaHatdata.Orderdetals.RemoveRange(DeleteOrderdetals);
            pizzaHatdata.SaveChanges();

            var DeleteOrder = pizzaHatdata.Orders.Where(x => x.OrderID == id).FirstOrDefault();
            pizzaHatdata.Orders.Remove(DeleteOrder);
            pizzaHatdata.SaveChanges();

            return RedirectToAction("Orders");

        }
        public void Submit(int orderID)
        {
            
               var Order = pizzaHatdata.Orders.Where(x => x.OrderID == orderID).FirstOrDefault();
            if (Order!= null)
            {
                Order.TrxnStatus = "Done";
                Order.SubmitTime = DateTime.Now;
                pizzaHatdata.SaveChanges();
            }


        }

    }

}