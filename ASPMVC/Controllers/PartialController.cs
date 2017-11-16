using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ASPMVC.Controllers
{
    public class PartialController : Controller
    {
        // GET: Partial
        public ActionResult Index()
        {
            return View("ChangedIndex");
        }

        //Filter na partial viewsy
        [ChildActionOnly]
        public ActionResult PartialViewSample()
        {
            var time = DateTime.Now.ToShortDateString();
            return PartialView("_PartialViewSample", time);
        }
    }
}