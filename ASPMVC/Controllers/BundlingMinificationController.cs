using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ASPMVC.Controllers
{
    public class BundlingMinificationController : Controller
    {
        /// Najlepiej jest dodawać skrypty typu jQuery, Ajax przez CDN, ponieważ w cache
        /// jeśli użytkownik posiada taką biblioteke, nie musi jej ponownie pobierać
        /// a strony Google i Microsoft są bardzo szybkie
        /// 
        /// Skrypty dodawaj na końcu HTML (przed </body>), ponieważ jak przeglądarka pobiera skrypt
        /// to nie może dalej wyświetlać strony. Najlepiej ustawić @Rendersection na końcu layout'a
        /// 
        /// Najlepiej dodać jeden wielki plik (1 rządanie). Tutaj stosuj bundling i minification (pakowanie w 1 duży plik bez 
        /// zbędnych spacji)
        /// 
        public ActionResult Index()
        {
            return View();
        }
    }
}