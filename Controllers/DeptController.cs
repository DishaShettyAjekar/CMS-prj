using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCProject.Controllers
{
    public class DeptController : Controller
    {
        [Authorize(Roles = "User")]
        // GET: Dept
        public ActionResult Index()
        {
            SampleMVCEntities db = new SampleMVCEntities();
            var skill = db.tbl_skills.ToList();
            return View(skill);
        }
    }
}