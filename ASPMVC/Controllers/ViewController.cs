using ASPMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ASPMVC.Controllers
{
    /// <summary>
    /// Wszystkie widoki znajdują się w folderze View. Pierwsze poszukiwane są widowki w folderze o nazwie kontrolera
    /// potem w folderze Shared(kontrolki, partial views). _ViewStart odpalane jest na początku (można tu zainicjować 
    /// jakieś wsadowe dane).
    /// </summary>
    public class ViewController : Controller
    {
        // GET: View
        public ActionResult Index()
        {
            MyModel model = new MyModel();
            model.Message = "Hello";
            return View(model);
        }

        public ActionResult SpecificView()
        {
            //Słabe typowanie, bo składnia ViewBag nie jest zdefiniowany po stronie widoku(pomyłki)
            ViewBag.oneModel = 12;
            var twoModel = new { Id = 1, Name = "Produkt" };

            //silne typowanie, bo zdefiniowany model który można typować w pliku widoku
            //@model Myapp.Model.MyModel
            return View("SomeView", twoModel); 
            return new ViewResult() {  };
        }
    }
}