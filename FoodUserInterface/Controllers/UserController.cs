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

        public ActionResult Cart()
        {
            return View();
        }
    }
}