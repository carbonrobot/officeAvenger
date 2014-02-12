using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OfficeAvenger.Web.Controllers;

namespace OfficeAvenger.Web.Areas.Angular.Controllers
{
    public class HomeController : UnderworldController
    {
        // GET: /Angular/Home/
        public ActionResult Index()
        {
            return View();
        }
	}
}