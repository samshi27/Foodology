using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FoodDatabase;
using FoodServices.Controllers;

namespace FoodUserInterface.Controllers
{
    public class UserController : Controller
    {
        UserServicesController userServices = new UserServicesController();

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(fs_user uobj)
        {
            using (FoodServicesEntities entity = new FoodServicesEntities())
            {
                var uobject = entity.fs_user.Where(x => x.u_username.Equals(uobj.u_username) && x.u_password.Equals(uobj.u_password)).FirstOrDefault();

                if (uobject != null)
                {
                    Session["u_id"] = uobject.u_id.ToString();
                    return RedirectToAction("Home");
                }

                else
                {
                    ViewBag.error = "Invalid Username or Password.";
                }
                return View();
            }
        }

        [HttpGet]
        public ActionResult Signup()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Signup(fs_user uobj)
        {
            using (FoodServicesEntities entity = new FoodServicesEntities())
            {
                entity.fs_user.Add(uobj);
                entity.SaveChanges();

                return RedirectToAction("Login");
            }
        }

        public ActionResult Home()
        {
            return View();
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
    }
}