using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace myMenuJournal.Controllers
{
    public class UserController : Controller
    {
        //
        // GET: /User/

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult FacebookLogin()
        {
            string stopHere = "";
            Membership.CreateUser("bewebdev", "one5four","bewebdev@gmail.com");
            return Redirect("/");
        }

    }
}
