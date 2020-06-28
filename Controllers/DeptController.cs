using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCProject.Controllers
{
    public class DeptController : Controller
    {
        // GET: Dept
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult List()
        {
            SampleMVCEntities db = new SampleMVCEntities();
            List<tbl_User> user = db.tbl_User.ToList();
            return Json(new { data= user},JsonRequestBehavior.AllowGet);
        }
    }
}