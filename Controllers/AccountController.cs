using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace MVCProject.Controllers
{
    [AllowAnonymous]
    public class AccountController : Controller
    {
        
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(tbl_User user)
        {
            SampleMVCEntities db = new SampleMVCEntities();
            var check = db.tbl_User.Where(x => x.UserName == user.UserName && x.Password == user.Password).Count();

            if (check > 0)
            {
                FormsAuthentication.SetAuthCookie(user.UserName, false);
                return this.RedirectToAction("List","Home");
            }
            else
            {
                return View();
            }
        }

        

    }
}