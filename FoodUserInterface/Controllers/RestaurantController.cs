using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FoodDatabase;
using PagedList;

namespace FoodUserInterface.Controllers
{
    public class RestaurantController : Controller
    {
        FoodServicesEntities entities = new FoodServicesEntities();

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(fs_restaurant adm)
        {

            fs_restaurant admin = entities.fs_restaurant.Where(x => x.r_name == adm.r_name && x.r_password == adm.r_password).SingleOrDefault();
            if (admin != null)
            {
                Session["r_id"] = admin.r_id.ToString();
                return RedirectToAction("Create");
            }
            else
            {
                ViewBag.error = "Invalid Username or Password.";
            }
            return View();
        }

        [HttpGet]
        public ActionResult Signup()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Signup(fs_restaurant uobj)
        {
            using (FoodServicesEntities entity = new FoodServicesEntities())
            {
                entity.fs_restaurant.Add(uobj);
                entity.SaveChanges();

                return RedirectToAction("Login");
            }
        }

        [HttpGet]
        public ActionResult Create()
        {
            if (Session["r_id"] == null)
            {
                return RedirectToAction("Login");
            }
            return View();
        }

        [HttpPost]
        public ActionResult Create(fs_category cvm, HttpPostedFileBase imgFile)
        {
            string path = uploadingImgFile(imgFile);
            if (path.Equals("-1"))
            {
                ViewBag.error = "Image could not be uploaded";
            }
            else
            {
                fs_category cat = new fs_category
                {
                    c_name = cvm.c_name,
                    c_image = path,
                    c_status = 1,
                    c_res_id = Convert.ToInt32(Session["ad_id"].ToString())
                };
                entities.fs_category.Add(cat);
                entities.SaveChanges();
                ViewBag.SuccessMessage = "Image uploaded successfully";
                return RedirectToAction("ViewCategory");
            }
            return View();
        }

        public ActionResult ViewCategory(int? page)
        {
            int pagesize = 6, pageindex = 1;
            pageindex = page.HasValue ? Convert.ToInt32(page) : 1;
            var list = entities.fs_category.Where(model => model.c_status == 1).OrderByDescending(model => model.c_id).ToList();
            IPagedList<fs_category> stu = list.ToPagedList(pageindex, pagesize);

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