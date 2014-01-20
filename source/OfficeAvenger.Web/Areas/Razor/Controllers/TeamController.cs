using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OfficeAvenger.Web.Areas.Razor.Models;

namespace OfficeAvenger.Web.Areas.Razor.Controllers
{
    public class TeamController : Controller
    {
        [HttpGet]
        public ActionResult Add()
        {
            return View("Edit");
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            return View();
        }

        [HttpPost]
        public ActionResult Edit(EditTeamModel model)
        {
            return View();
        }

    }
}
