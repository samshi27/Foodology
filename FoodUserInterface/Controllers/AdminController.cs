using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FoodDatabase;
using PagedList;
using FoodServices.Controllers;

namespace FoodUserInterface.Controllers
{
    public class AdminController : Controller
    {
        AdminServicesController adminServices = new AdminServicesController();
        FoodDatabaseEntities entities = new FoodDatabaseEntities();

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(fs_admin adm)
        {

            fs_admin admin = entities.fs_admin.Where(x => x.a_username == adm.a_username && x.a_password == adm.a_password).SingleOrDefault();
            if (admin != null)
            {
                Session["a_id"] = admin.a_id.ToString();
                return RedirectToAction("Create");
            }
            else
            {
                ViewBag.error = "Invalid Username or Password.";
            }
            return View();
        }

        [HttpGet]
        public ActionResult Create()
        {
            if (Session["a_id"] == null)
            {
                return RedirectToAction("Login");
            }
            return View();
        }

        [HttpPost]
        public ActionResult Create(fs_restaurant cvm, HttpPostedFileBase imgFile)
        {
           
                string path = uploadingImgFile(imgFile);
                if (path.Equals("-1"))
                {
                    ViewBag.error = "Image could not be uploaded";
                }
                else
                {
                    fs_restaurant fres = new fs_restaurant
                    {
                        r_name = cvm.r_name,
                        r_location = cvm.r_location,
                        r_email = cvm.r_email,
                        r_contact = cvm.r_contact,
                        r_password = cvm.r_password,
                        r_image = path,
                        r_status = 1,
                        r_a_id = Convert.ToInt32(Session["a_id"].ToString())
                    };
                    entities.fs_restaurant.Add(fres);
                    entities.SaveChanges();
                    return RedirectToAction("ViewRestaurant");
                }
                return View();
        }

        public ActionResult ViewRestaurant(int? page)
        {
            int pagesize = 6, pageindex = 1;
            pageindex = page.HasValue ? Convert.ToInt32(page) : 1;
            var list = entities.fs_restaurant.Where(model => model.r_status == 1).OrderByDescending(model => model.r_id).ToList();
            IPagedList<fs_restaurant> stu = list.ToPagedList(pageindex, pagesize);

            return View(stu);
        }

        public string uploadingImgFile(HttpPostedFileBase file)
        {
            Random r = new Random();
            string path = "-1";
            int random = r.Next();
            if (file != null && file.ContentLength > 0)
            {
                string extension = Path.GetExtension(file.FileName);
                if (extension.ToLower().Equals(".jpg") || extension.ToLower().Equals(".jpeg") || extension.ToLower().Equals(".png"))
                {
                    try
                    {
                        path = Path.Combine(Server.MapPath("~/Content/upload"), random + Path.GetFileName(file.FileName));
                        file.SaveAs(path);
                        path = "~/Content/upload/" + random + Path.GetFileName(file.FileName);
                    }
                    catch (Exception ex)
                    {
                        path = "-1";
                    }
                }
                else
                {
                    Response.Write("<script> alert('Only .jpg, .jpeg and .png files are acceptable.'); </script>");
                }
            }
            else
            {
                Response.Write("<script> alert('Please select a file.'); </script>");
                path = "-1";
            }
            return path;
        }

    }
}