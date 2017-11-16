using ASPMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ASPMVC.Controllers
{
    public class AjaxController : Controller
    {
        /// Ajax bazuje na XMLHtmlRequest "XHR"
        /// 
        /// 

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Json()
        {
            var result = new Album
            {
                Artist = new Artist() { Name = "U2"},
                Id = 1,
                Name = "Some title"

            };
            return Json()
        }
    }
}