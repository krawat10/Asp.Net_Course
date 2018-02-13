using Account.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Account.Controllers
{
    public class HomeController : Controller
    {
        /// <summary>
        /// If is not authorized ASP.NET automatically redirect us to Login action in Account controller.
        /// </summary>
        /// <returns></returns>
        [Authorize]
        public ActionResult Index()
        {
            //Get user id
            var currentUserId = User.Identity.GetUserId();

            //Find user in our manager
            var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
            var currentUser = manager.FindById(currentUserId);

            var r = currentUser.Roles;
            ViewBag.ProfileInformation = currentUser.UserInfo.FirstName;
            
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}