using ASPMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ASPMVC.Controllers
{
    public class HTMLHelpersController : Controller
    {
        // GET: HTMLHelpers
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult DynamicHelpers()
        {
            var result = new HelpersModel
            {
                Bool = true,
                DateTime = new DateTime(2001, 12, 21),
                Decimal = 123.1231112312312m,
                String = "Helpery w ASP.NET"
            };

            return View(result);
        }
        
    }
}