using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace ASPMVC.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        //routes.MapRoute("simple", "{controller}/{action}/{id}")
        //../Home/Index/12
        public ActionResult Index()
        {
            //Definiowanie URL na podstawie takiego routingu
            //Metoda powinna być gdzieś w kontrolerze
            //param - utworzenie Querystringa:
            //../Home/Index/1?param=Bartek
            VirtualPathData vp = RouteTable.Routes.GetVirtualPath(null,
                new RouteValueDictionary(new
                {
                    action = "Home",
                    controller = "Index",
                    id = "1",
                    param = "Bartek"
                }));
         
            return View();
        }

        /// Przykład kontrolera który zwraca plik
        public ActionResult GetFile()
        {
            var physicalPath = Server.MapPath("~/Files/File.txt");
            return File(physicalPath, "application/text");
        }


        //////////////////////////////////////////////////////////////////
        /// Można definiować własne typy (klasy ActionResult)
        public class MyResult : ActionResult
        {
            public override void ExecuteResult(ControllerContext context)
            {
                throw new NotImplementedException();
            }
        }
        public ActionResult GetCsvFile()
        {
            return new MyResult();
        }
        //////////////////////////////////////////////////////////////////

        /// Przykład kontrolera który przekierowuje do innej akcji
        public ActionResult RedirectExample()
        {
            return RedirectToAction("Index", "Home");
        }

        //Zamiast opisywać w RouteConfig, można tu dać atrybut
        //[Route("api/todo/{argument?}")] - parametr opcjonalny (moze byc bez)
        //[Route("api/todo/{argument="elo"}")] - parametr domyślny
        //[Route("api/todo/{argument:int}")] - parametr liczbowy
        [Route("api/todo/{argument}/sds")]
        public ActionResult Info(string argument)
        {
            return View();
        }

        public ActionResult Response()
        {
            EmptyResult result;
            ContentResult content;  //string etc
            JsonResult json;  //do notacji JS
            RedirectResult redirect; //przekierowuje (302)
            RedirectToRouteResult reirectResult; //jak wyeżej
            ViewResult view;  //zwykły view
            PartialViewResult partial;  //kontrolki, ajax
            FileContentResult file;  //pliki
            JavaScriptResult javaScript; //zwraca plik js i go wykonuje

            return JavaScript("console.log('hello')");  //Usuwamy 'Result'  
            //return new ViewResult() { *** };         //gorszy sposób
        }
    }
}