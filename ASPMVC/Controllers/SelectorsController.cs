using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ASPMVC.Controllers
{
    public class SelectorsController : Controller
    {
        /// Tej metody nie wywołasz z poziomu URL
        [NonAction]
        public ActionResult NoAction()
        {
            return View();
        }

        /// Zmiana nazwy akcji na inną "/Selectors/NewAction"
        [ActionName("NewAction")]
        public ActionResult OldName()
        {
            return View();
        }

        /// Tylko akcja POST
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult AllowPost()
        {
            return Content("only post");
        }

        ///Normalna metoda (np. otwarcie formularza)
        public ActionResult Edit()
        {
            return Content("NormalContent");
        }
        
        //Jeżeli wyślemy zapytanie POST /Selectors/Edit?name=elo&id=1
        //Wywołamy tą metode (czesto używa sie w formularzach)
        [HttpPost]
        public ActionResult Edit(string name, int id)
        {
            return Content("SpecialContent(Form)");
        }
    }
}