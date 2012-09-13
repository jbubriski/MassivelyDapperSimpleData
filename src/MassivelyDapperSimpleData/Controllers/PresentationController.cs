using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MassivelyDapperSimpleData.Controllers
{
    public class PresentationController : Controller
    {
        public ActionResult Index(string viewName)
        {
            if (!string.IsNullOrWhiteSpace(viewName))
            {
                return View(viewName);
            }

            return View("Intro");
        }
    }
}
