using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ASPMVC.Controllers
{
    public class PostRedirectGetController : Controller
    {
        
        /// <summary>
        /// Zwrócenie strony z formularzem.
        /// </summary>
        /// <returns>Strona z formularzem</returns>
        public ActionResult FormPage()
        {
            return View();
        }

        /// <summary>
        /// Wyświetlenie wyniku formularza (po naciśnięciu submit). Jeśli tą strone odświerzymy w przeglądarce,
        /// to akcja ponownie zostanie wykonana co spowoduje ponowne dodanie parametru do bazy danych. Jest to nieporządane
        /// działanie.
        /// </summary>
        /// <param name="paramtopass"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult PassTempDataNormal(string paramtopass)
        {
            ViewBag.TempParam = paramtopass;
            
            //database.Add(paramtopass)

            return View("FormResult");
        }

        /// <summary>
        /// Akcja wykonywana po nacisnięciu 'submit'. Tutaj akcja po dodaniu parametru do bazy danych, przekierowuje nas
        /// do innej akcji (302 - Redirect). W tym przypadku przekierowani zostaniemy do 'FormResult' przez GET (przeglądarka 
        /// dostanie informacje o przekierowaniu). W tym przypadku gdy użytkownik na stronie wynikowej kliknie odświerz,
        /// wykona się akcja 'FormResult' i nic nie bedzie dodawane do bazy danych (Odświezymy tylko strone wynikową).
        /// 
        /// (client)FormPage -----------POST------> (server)
        /// (client)         <--------Redirect----- (server)PassTempDataPRG(add data)
        /// (client)302      ------------Get------> (server)
        /// (client)         <--------Response----- (server)FormResult
        /// </summary>
        /// <param name="paramtopass"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult PassTempDataPRG(string paramtopass)
        {
            TempData["param"] = paramtopass;    //Przekazywanie danych pomiędzy akcjami (na max. 2 akcje)

            //database.Add(paramtopass)

            return RedirectToAction("FormResult");
        }

        /// <summary>
        /// Strona do wyświetlania wyników.
        /// </summary>
        /// <returns></returns>
        public ActionResult FormResult()
        {
            ViewBag.TempParam = TempData["param"];  //Pobranie przekazanych danych.
            return View();
        }
    }
}