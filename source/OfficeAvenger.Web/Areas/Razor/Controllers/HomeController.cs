using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace OfficeAvenger.Web.Areas.Razor.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string username, string password)
        {
            //FormsAuthentication.SetAuthCookie(username, true);

            return new RedirectResult("Index");
        }

    }
}
