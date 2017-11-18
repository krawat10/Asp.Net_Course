using ASPMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace ASPMVC.Controllers
{
    public class AjaxController : Controller
    {
        /// Ajax bazuje na XMLHtmlRequest "XHR"
        /// najpopularniejsze ajax'y po stronie javascriptu to $.getJSON $.ajax
        /// 

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult JavaScriptExamples()
        {
            return View();
        }

        public ActionResult AjaxHelpery()
        {
            return View();
        }

        public ActionResult Json()
        {
            var result = new Album
            {
                Artist = new Artist() { Name = "U2" },
                Id = 1,
                Name = "Some title"

            };
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public string GetTime()
        {
            Thread.Sleep(2000);
            return DateTime.Now.ToLongTimeString();
        }

        public ActionResult AjaxForm()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AjaxForm(Album a)
        {
            //return Content($"Id:{a.Id}, Name:{a.Name}, Artist:{a.Artist.Name}."); //Zwracanie zwykłego stringa
            return Json(a); //Wysłanie obiektu JSON
        }

        //Przykład jakiegoś przesłanego formularza
        [HttpPost]
        public ActionResult SeoFormTrick(string searchQuery)
        {
            var content = new { Id = 1, Name = "Nazwa", Query = searchQuery };

            //Jeśli działa kod Javascript (i ajaxowy) zostanie zwrócony widok cząstkowy
            if (Request.IsAjaxRequest())
            {
                return PartialView(content);
            }

            //Jeśli nie działa Javascript (wyszukiwarki jak indeksują to nie używają) to zwraca cały widok
            return View(content);
        }

        public ActionResult AjaxDemo(string searchQuery = null)
        {
            IEnumerable<Person> personList;

            if (searchQuery != null)
            {
                personList = Person.GetPersonList().Where(p =>
                p.FirstName.Contains(searchQuery) ||
                p.LastName.Contains(searchQuery) ||
                p.LastName == searchQuery ||
                p.FirstName == searchQuery ||
                $"{p.FirstName} {p.LastName}".Contains(searchQuery)
                ).ToArray();
            }
            else
            {
                personList = Person.GetPersonList().ToArray();
            }

            if (Request.IsAjaxRequest())
            {
                return PartialView("_PersonList", personList);
            }

            return View(personList);
        }

        public ActionResult PersonSuggestion(string term)
        {
            var personList = Person.GetPersonList().Where(p =>
                p.FirstName.Contains(term) ||
                p.LastName.Contains(term) ||
                p.LastName == term ||
                p.FirstName == term ||
                $"{p.FirstName} {p.LastName}".Contains(term)
                ).Take(5)
                .Select(p => $"{p.FirstName} {p.LastName}")
                .ToArray();

            return Json(personList, JsonRequestBehavior.AllowGet);
        }
    }
}