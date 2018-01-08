using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace TAGS_BARCODE_WEBAPP_V4.Controllers
{
    public class DashboardController : Controller
    {
        // GET: Dashboard
        [Authorize]
        public ActionResult Home()
        {
            return View();
        }

        [Authorize]
        public ActionResult Station1()
        {
            return View();
        }

        [Authorize]
        public ActionResult AddUser()
        {
            return View();
        }

        [Authorize]
        public ActionResult UpdateMember()
        {
            return View();
        }


    }
}