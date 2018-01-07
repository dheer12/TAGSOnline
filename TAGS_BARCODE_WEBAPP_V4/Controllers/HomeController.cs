using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using TAGS_BARCODE_WEBAPP_V4.Models;
using TAGS_BARCODE_WEBAPP_V4.ViewModels;

namespace TAGS_BARCODE_WEBAPP_V4.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            return View();
        }
        [HttpPost]
        public ActionResult Index(UserVM ULV, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                string password = "";
                using (var db = new TagsDataModel())
                {
                    var user = (from login in db.TAGS_LOGIN
                                    where login.LAST_NAME == ULV.LoginName
                                    select login).FirstOrDefault();
                    if(user != null)
                    {
                        password = user.PASSWORD;
                    }
                }
               

                if (string.IsNullOrEmpty(password))
                    ModelState.AddModelError("", "The user login or password provided is incorrect.");
                else
                {
                    if (ULV.Password.Equals(password))
                    {
                        FormsAuthentication.SetAuthCookie(ULV.LoginName, false);
                        return RedirectToAction("Home", "Dashboard");
                    }
                    else
                    {
                        ModelState.AddModelError("", "The password provided is incorrect.");
                    }
                }
            }

            // If we got this far, something failed, redisplay form
            return View(ULV);
        }

        [Authorize]
        public ActionResult SignOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }

        public ActionResult UnAuthorized()
        {
            return View();
        }
    }
}
