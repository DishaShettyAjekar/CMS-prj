using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Rotativa;
namespace MVCProject.Controllers
{

    public class HomeController : Controller
    {
        
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }
        
        [HttpGet]
        [Authorize(Roles = "User,Admin")]
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register(tbl_User user)
        {
            if (ModelState.IsValid)
            {
                using (SampleMVCEntities db = new SampleMVCEntities())
                {
                    user.Role = "User";
                    db.tbl_User.Add(user);
                    db.SaveChanges();
                }

                return RedirectToAction("List");
            }
            else
            {
                return View();
            }

        }
        [HttpGet]
        public ActionResult List()
        {
            SampleMVCEntities db = new SampleMVCEntities();
            List<tbl_User> list = db.tbl_User.ToList();
            return View(list);
        }

        [HttpPost]
        public ActionResult List(string textSearch)
        {
            SampleMVCEntities db = new SampleMVCEntities();
            List<tbl_User> usr = db.tbl_User.ToList();

            if (textSearch != null)
            {
                usr = db.tbl_User.Where(x => x.UserName.Contains(textSearch) || x.Email.Contains(textSearch) ||
                    x.Gender.Contains(textSearch)).ToList();
            }
            return View(usr);
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            SampleMVCEntities db = new SampleMVCEntities();
            tbl_User user = db.tbl_User.Find(id);
            ViewBag.skills = db.tbl_skills.ToList();
            user.Gender = user.Gender.Trim();
            return View(user);
        }

        [HttpPost]
        public ActionResult Edit(tbl_User user, HttpPostedFileBase postedFile)
        {
            SampleMVCEntities db = new SampleMVCEntities();
            string filename;
            if (user.UserImage != null)
            {
                filename = Path.GetFileNameWithoutExtension(user.UserImage.FileName);
                string extension = Path.GetExtension(user.UserImage.FileName);
                user.imagePath = "~/images/" + filename + extension;
                filename = Path.Combine(Server.MapPath("~/images/"), filename + extension);
                //saves image in folder
                user.UserImage.SaveAs(filename);
            }
            else
            {
                //user.imagePath = "~/images/img.png";
                //filename = Path.Combine(Server.MapPath("~/images/"), "img.png");
                //bool isExist = System.IO.File.Exists(user.imagePath);
                ////user.UserImage.SaveAs(filename);
            }
            user.Role = "User";
            db.Entry(user).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("List");
        }

        [HttpGet]
        public ActionResult Details(int id)
        {
            SampleMVCEntities db = new SampleMVCEntities();
            tbl_User user = db.tbl_User.Find(id);
            return View(user);
        }

        public ActionResult Delete(int id)
        {
            SampleMVCEntities db = new SampleMVCEntities();
            tbl_User user = db.tbl_User.Find(id);
            db.tbl_User.Remove(user);
            db.SaveChanges();
            return RedirectToAction("List");
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return this.RedirectToAction("Login", "Account");
        }


        public ActionResult PrintAll()
        {
            var print = new ActionAsPdf("List");
            return print;
        }
    }
}