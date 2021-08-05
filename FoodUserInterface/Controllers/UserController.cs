using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FoodDatabase;
using PagedList;
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

        public ActionResult Home(int? page)
        {
            int pagesize = 6, pageindex = 1;
            pageindex = page.HasValue ? Convert.ToInt32(page) : 1;
            var list = entities.fs_restaurant.Where(model => model.r_status == 1).OrderByDescending(model => model.r_id).ToList();
            IPagedList<fs_restaurant> stu = list.ToPagedList(pageindex, pagesize);
            return View(stu);
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
        /*
        public ActionResult ViewRestaurant(int? page)
        {
            int pagesize = 6, pageindex = 1;
            pageindex = page.HasValue ? Convert.ToInt32(page) : 1;
            var list = entities.fs_restaurant.Where(model => model.r_status == 1).OrderByDescending(model => model.r_id).ToList();
            IPagedList<fs_restaurant> stu = list.ToPagedList(pageindex, pagesize);

            return View(stu);
        }*/
    }
}