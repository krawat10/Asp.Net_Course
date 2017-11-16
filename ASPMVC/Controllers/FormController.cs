using ASPMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ASPMVC.Controllers
{
    public class FormController : Controller
    {
        // GET: Form
        public ActionResult Binding()
        {
            return View();
        }
        
        /// W Naszej formie użyliśmy inputów o parametrach 'name':
        /// - Name
        /// - Id
        /// - Artist.Name
        /// ASP.NET automatycznie parsuje (Model Binders) 
        /// je i możemy użyć argumentu akcji już jako rozbudowanej klasy
        /// UWAGA - działa to na propercjach
        public void GetAlbum(Album a)
        {
            ///to można na przykład sprawdzić w Fidlerze. Response jest primitywną formą zwracania Requestu
            Response.Write(a.ToString());
        }
    }
}