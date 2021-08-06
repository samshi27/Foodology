using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FoodDatabase;

namespace FoodUserInterface.Controllers
{
    public class RestaurantController : Controller
    {
        FoodDatabaseEntities entities = new FoodDatabaseEntities();

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(fs_restaurant adm)
        {

            fs_restaurant admin = entities.fs_restaurant.Where(x => x.r_email == adm.r_email && x.r_password == adm.r_password).SingleOrDefault();
            if (admin != null)
            {
                Session["r_id"] = admin.r_id.ToString();
                return RedirectToAction("CreateMenu");
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
        public ActionResult Signup(fs_category uobj)
        {
            using (FoodDatabaseEntities entity = new FoodDatabaseEntities())
            {
                entity.fs_category.Add(uobj);
                entity.SaveChanges();

                return RedirectToAction("Login");
            }
        }


        public ActionResult CreateMenu()
        {
            return View();
        }




        // ---------------- ADD CATEGORY ---------------------

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
                fs_category category = new fs_category
                {
                    c_name = cvm.c_name,
                    c_image = path,
                    c_status = 1,
                    c_r_id = Convert.ToInt32(Session["r_id"].ToString())
                };
                entities.fs_category.Add(category);
                entities.SaveChanges();
                ViewBag.SuccessMessage = "Image uploaded successfully";
                return RedirectToAction("CreateItem");
            }
            return View();
        }



        public ActionResult ViewCategory()
        {
            List<fs_category> categories = entities.fs_category.Where(model => model.c_status == 1).ToList();
            //List<fs_category> categories = entities.fs_category.Where(model => model.c_status == 1 && model.c_r_id == id).ToList();

            return View(categories);
        }

        /*
        public ActionResult ViewCategory(int? id, int? page)
        {

            if (Session["r_id"] == null)
            {
                return RedirectToAction("Login");
            }

            int pagesize = 6, pageindex = 1;
            pageindex = page.HasValue ? Convert.ToInt32(page) : 1;
            //var list = entities.fs_category.Where(model => model.c_r_id == id).OrderByDescending(model => model.c_id).ToList();
            var list = entities.fs_category.Where(model => model.c_status == 1).OrderByDescending(model => model.c_id).ToList();
            IPagedList<fs_category> stu = list.ToPagedList(pageindex, pagesize);

            return View(stu);
        }
        
        
        [HttpPost]
        public ActionResult ViewCategory(int? id, int? page, string search)
        {

            int pagesize = 6, pageindex = 1;
            pageindex = page.HasValue ? Convert.ToInt32(page) : 1;
            var list = entities.fs_category.Where(model => model.c_name.Contains(search)).OrderByDescending(model => model.c_id).ToList();
            IPagedList<fs_category> stu = list.ToPagedList(pageindex, pagesize);

            return View(stu);
        }
        */
        // ---------------- END ADD CATEGORY ---------------------


        // ---------------- ADD ITEM ---------------------

        [HttpGet]
        public ActionResult CreateItem(int id =0)
        {
            if (Session["r_id"] == null)
            {
                return RedirectToAction("Login");
            }

            fs_item itemModel = new fs_item();
            using (FoodDatabaseEntities entities = new FoodDatabaseEntities())
            {
                itemModel.CategoryCollection = entities.fs_category.ToList<fs_category>();
            }
             
                return View(itemModel);
        }

        [HttpPost]
        public ActionResult CreateItem(fs_item cvm, HttpPostedFileBase imgFile)
        {
            string path = uploadingImgFile(imgFile);
            if (path.Equals("-1"))
            {
                ViewBag.error = "Image could not be uploaded";
            }
            else
            {
                fs_item item = new fs_item();

                    item.i_name = cvm.i_name;
                    item.i_price = cvm.i_price;
                    item.i_desc = cvm.i_desc;
                    item.i_image = path;
                    item.i_status = 1;
                    item.i_c_id = cvm.i_c_id;
                    item.i_r_id = Convert.ToInt32(Session["r_id"].ToString());
                
                entities.fs_item.Add(item);
                entities.SaveChanges();
                ViewBag.SuccessMessage = "Image uploaded successfully";
                return RedirectToAction("ViewItem");
            }
            return View();
        }

        /*
        public ActionResult ViewItem(int? page)
        {
            int pagesize = 6, pageindex = 1;
            pageindex = page.HasValue ? Convert.ToInt32(page) : 1;
            var list = entities.fs_item.Where(model => model.i_status == 1).OrderByDescending(model => model.i_id).ToList();
            IPagedList<fs_item> stu = list.ToPagedList(pageindex, pagesize);

            return View(stu);
        }
        */
        // ----------------- END ADD ITEM -------------------
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
                        path = Path.Combine(Server.MapPath("~/Content/upload_cat"), random + Path.GetFileName(file.FileName));
                        file.SaveAs(path);
                        path = "~/Content/upload_cat/" + random + Path.GetFileName(file.FileName);
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