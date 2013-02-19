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
            return View();
        }

        [HttpPost]
        public ActionResult FacebookLogin()
        {
            var accessToken =Request["accessToken"];
            Session["AccessToken"] = accessToken;

            var client = new FacebookClient(accessToken);
            dynamic result = client.Get("me");

            string stopHere = "";

            if (result != null)
            {
                //first, lets make sure that it is not an old user
                string FBemail = result.email;
                var CheckUser = _db.Users.Where(x => x.Email.Equals(FBemail)).FirstOrDefault();

                if (CheckUser != null)
                {
                    //FormsAuthentication.Authenticate();
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
                    _db.UserProperties.Add(ThisUserProperties);
                    _db.SaveChanges();
                }
            }

            //Membership.CreateUser("bewebdev", "one5four","bewebdev@gmail.com");
            return Redirect("/");
        }

    }
}
