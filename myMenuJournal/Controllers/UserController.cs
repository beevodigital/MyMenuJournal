using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Facebook;
using myMenuJournal.Entities;

namespace myMenuJournal.Controllers
{
    public class UserController : Controller
    {
        DataContext _db;

        public UserController()
        {
            _db = new DataContext();
        }

        public ActionResult Index()
        {
            var CastedUser = (User)Session["ThisUser"];
            var ThisUser = _db.Users.Where(x => x.UserId.Equals(CastedUser.UserId)).FirstOrDefault();

            ViewBag.ThisUser = ThisUser;
            ViewBag.ThisUserProperties = _db.UserProperties.Where(x => x.User.UserId.Equals(ThisUser.UserId)).FirstOrDefault();

            //get todays log
            var CurrentDOY = DateTime.Now.DayOfYear;
            var CheckDay = _db.UserDays.Where(x => x.UserID.Equals(ThisUser.UserId)).Where(x => x.DOYIntake.Equals(CurrentDOY)).FirstOrDefault();

            ViewBag.CheckDay = CheckDay;
            return View();
        }

        public ActionResult Logout()
        {
            Session["ThisUser"] = null;
            Session["ThisUserProperties"] = null;
            Session.Clear();

            return View();
        }

        [HttpPost]
        public ActionResult FacebookLogin()
        {
            var accessToken =Request["accessToken"];
            Session["AccessToken"] = accessToken;

            var client = new FacebookClient(accessToken);
            dynamic result = client.Get("me");

            if (result != null)
            {
                //first, lets make sure that it is not an old user
                string FBemail = result.email;
                var CheckUser = _db.Users.Where(x => x.Username.Equals(FBemail)).FirstOrDefault();

                if (CheckUser != null)
                {
                    Membership.ValidateUser(CheckUser.Username,accessToken);
                    var ThisUserProperties = _db.UserProperties.Where(x => x.User.UserId.Equals(CheckUser.UserId)).FirstOrDefault();
                    Session["ThisUser"] = CheckUser;
                }
                else
                {
                    //lets create the user
                    var NewUser = Membership.CreateUser(FBemail, accessToken, FBemail);
                    //create their properties too
                    var ThisUser = _db.Users.Where(x => x.Username.Equals(FBemail)).FirstOrDefault();
                    var ThisUserProperties = new UserProperties();
                    ThisUserProperties.UserPropertyId = Guid.NewGuid();
                    ThisUserProperties.User = ThisUser;
                    ThisUserProperties.FacebookToken = accessToken;
                    ThisUserProperties.FirstName = result.first_name;
                    ThisUserProperties.LastName = result.last_name;
                    _db.UserProperties.Add(ThisUserProperties);
                    _db.SaveChanges();

                    Session["ThisUser"] = ThisUser;
                }
            }

            //Membership.CreateUser("bewebdev", "one5four","bewebdev@gmail.com");
            return Redirect("/user");
        }

    }
}
