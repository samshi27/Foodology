using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FoodDatabase;
using FoodServices.Controllers;
using System.Web.Security;

namespace FoodUserInterface.Controllers
{
    public class UserController : Controller
    {
        UserServicesController userServices = new UserServicesController();
        FoodDatabaseEntities entities = new FoodDatabaseEntities();

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(fs_user uobj)
        {
            using (FoodDatabaseEntities entity = new FoodDatabaseEntities())
            {
                var uobject = entity.fs_user.Where(x => x.u_username.Equals(uobj.u_username) && x.u_password.Equals(uobj.u_password)).FirstOrDefault();
                
                if (uobject != null)
                {
                    Session["u_id"] = uobject.u_id.ToString();
                    FormsAuthentication.SetAuthCookie(uobject.u_username, true);
                    Session["u_username"] = uobject.u_username.ToString();
                    return RedirectToAction("Home", "User");
                }

                else
                {
                    ViewBag.error = "Invalid Username or Password.";
                }
                return View();
            }
        }

        public ActionResult Logout()
        {
            Session.RemoveAll();
            return RedirectToAction("Home");
        }

        [HttpGet]
        public ActionResult Signup()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Signup(fs_user uobj)
        {
            using (FoodDatabaseEntities entity = new FoodDatabaseEntities())
            {
                entity.fs_user.Add(uobj);
                entity.SaveChanges();

                return RedirectToAction("Login");
            }
        }

        public ActionResult Home()
        {
            if (TempData["cart"] != null)
            {
                int x = 0;
                List<Cart> finalCart = TempData["cart"] as List<Cart>;
                
                foreach(var item in finalCart)
                {
                    x += item.itemBill;
                }

                TempData["total"] = x;
                TempData["item_count"] = finalCart.Count();
            }
            TempData.Keep();
            
            List<fs_restaurant> restaurants = entities.fs_restaurant.Where(model => model.r_status == 1).ToList();
            
            return View(restaurants);
        }

        public ActionResult Index()
        {
            var userList = userServices.GetRegUsers();
            return View(userList);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(fs_user employee)
        {
            userServices.Post(employee);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var user = userServices.GetRegUsers(id);
            return View(user);
        }

        [HttpPost]
        public ActionResult Edit(int id, fs_user user)
        {
            userServices.Put(id, user);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            var emp = userServices.GetRegUsers(id);
            return View(emp);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteEmp(int id)
        {
            userServices.Delete(id);
            return RedirectToAction("Index");
        }

        public ActionResult ViewCategory(int id)
        {
            List<fs_category> categories = entities.fs_category.Where(model => model.c_status == 1 && model.c_r_id == id).ToList();
            return View(categories);
        }
        
        public ActionResult ViewItem(int id)
        {
            List<fs_item> items = entities.fs_item.Where(model => model.i_status == 1 && model.i_c_id == id).ToList();
            return View(items);
        }
        
        public ActionResult ItemDetails(int id)
        {
            fs_item item = entities.fs_item.Single(i => i.i_id == id);

            return View(item);
        }
        
        [HttpPost]
        public ActionResult ItemDetails(int id, int qty)
        {
            if(Session["u_id"] == null)

            {
                return RedirectToAction("Login");
            }

            fs_item item = entities.fs_item.Single(i => i.i_id == id);
            List<Cart> cartList = new List<Cart>();


            Cart cartItem = new Cart
            {
                itemId = id,
                itemName = item.i_name,
                itemPrice = Convert.ToInt32(item.i_price),
                itemQty = Convert.ToInt32(qty)
            };

            cartItem.itemBill = cartItem.itemPrice * cartItem.itemQty;
            
            if(TempData["cart"] == null)
            {
                cartList.Add(cartItem);
                TempData["cart"] = cartList;
            }

            else
            {
                List<Cart> cartList2 = TempData["cart"] as List<Cart>;
                int flag = 0;
                foreach (var i in cartList2)
                {
                    if(i.itemId == cartItem.itemId)
                    {
                        i.itemQty += cartItem.itemQty;
                        i.itemBill += cartItem.itemBill;
                        flag = 1;
                    }
                }

                if(flag == 0)
                {
                    cartList2.Add(cartItem);
                }
                TempData["cart"] = cartList2;
            }
            
            TempData.Keep();
            return RedirectToAction("Home");
        }

        public ActionResult Cart()
        {
            TempData.Keep();
            return View();
        }
        
        [HttpPost]
        public ActionResult Cart(string contact, string address)
        {
            if (ModelState.IsValid)
            {
                List<Cart> carts = TempData["cart"] as List<Cart>;
                foreach(var item in carts)
                {
                    fs_order order = new fs_order
                    {
                        o_i_id = item.itemId,
                        o_u_id = Convert.ToInt32(Session["u_id"].ToString()),
                        o_total = (int)TempData["total"],
                        o_contact = contact,
                        o_address = address,
                        o_date = DateTime.Now
                    };
                    entities.fs_order.Add(order);
                    entities.SaveChanges();
                }
                TempData.Remove("total");
                TempData.Remove("cart");
                TempData["message"] = "Order Booked Successfully.";
                return RedirectToAction("Cart");
            }
            TempData.Keep();
            return View();
        }

        public ActionResult Order()
        {
            List<fs_order> orders = new List<fs_order>();
            return View();
        }


        public ActionResult RemoveItem(int? id)
        {
            if(TempData["cart"] == null)
            {
                TempData.Remove("total");
                TempData.Remove("cart");
            }
            else
            {
                List<Cart> carts = TempData["cart"] as List<Cart>;
                Cart cart = carts.Where(x => x.itemId == id).SingleOrDefault();
                carts.Remove(cart);
                int s = 0;

                foreach (var item in carts)
                {
                    s += item.itemBill;
                }
                TempData["total"] = s;
            }
            TempData.Keep();
            return RedirectToAction("Cart");
        }
    }
}