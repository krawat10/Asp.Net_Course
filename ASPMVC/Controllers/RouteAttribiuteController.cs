using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ASPMVC.Controllers
{
    /// Controller to urls www.mypage.com\prefix\{my
    [RoutePrefix("prefix")]
    public class RouteAttribiuteController : Controller
    {
        /// Action which uses Attribiute routing. If we want this type of routing
        /// we need to add routes.MapMVCAttribiuteRoutes() in RouteConfig.cs
        [Route("this/is/{my}/route")]
        public string Index(string my)
        {
            return "Attribute routing. Attribiute equal: " + my;
        }


        [Route("{param:int}")]
        public string Attr(string param)
        {
            return param;
        }
    }
}