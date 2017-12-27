using ASPMVC.Infastructure;
using ASPMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ASPMVC.Controllers
{
    public class ModelController : Controller
    {
        // GET: Model
        public ActionResult Index()
        {
            return View();
        }

        //jeśli value nie zostanie przesłana, wystąpi error
        public ActionResult ModelBinder(decimal value, int? canBeNull, int defValue= 1) 
        {
            return View();
        }

        public ActionResult Edit()
        {
            return View();
        }

        [HttpPost]
        public ActionResult BinderEdit([Bind(Exclude ="Role")] Album album) //Parametr Role nigdy nie bedzie bindowany (bezpieczenstwo)
        {
            return Content($"Nazwa: {album.Name}, id: {album.Id}, artysta: {album.Artist.Name}");
        }

        //Niskopoziomowe podejście(FormCollection)
        [HttpPost]
        public ActionResult Edit(FormCollection collection)
        {
            Album album = new Album();


            //Bindowanie na nasz model. Możemy wybrać które parametry mają być bindowane (aby użytkownik nie dopisał
            // sobie role = Administrator itp.)
            //Można też to zrobić za pomocą interfejsa:
            //TryUpdateModel<IBaseUserInfo> gdzie zostaną zdefiniowane tylko te pola, które chcemy zmienić. 
            //Metoda używana razem z argumentem FormCollection
            TryUpdateModel(album, new string[] { "Name", "Id", "Artist.Name" });
            //UpdateModel(album)    


            var formValues = $"Nazwa: {collection["Name"]}, id: {collection["Id"]}, artysta: {collection["Artist.Name"]}";
            var modeledValues = $"Nazwa: {album.Name}, id: {album.Id}, artysta: {album.Artist.Name}";

            return Content($"wartosci z zapytania: {formValues}, model: {modeledValues}");
        }

        [HttpPost]
        public ActionResult EditDeafultNull(decimal? id, string albumtitle = "as")
        {
            return Content($"Cena: {id}, nazwa albumu: {albumtitle}");
        }

        public ActionResult EditWithOwnBinder()
        {
            return View();
        }

        //Przykład użycia własnego ModelBindera do parsowania formularza na nasz model
        //Należy ten Binder dodać do listy binderów w Global.asax
        [HttpPost]
        public ActionResult EditWithOwnBinder([ModelBinder(typeof(AlbumModelBinder))] Album album)
        {
            return Content($"Nazwa: {album.Name}, id: {album.Id}, artysta: {album.Artist.Name}, data: {album.DateString}");
        }
        
        //Tutaj możemy ręcznie modyfikować dostarczone dane. FormCollection to prymitywne dane (worek parametrów)
        public ActionResult ManualBinding(FormCollection collection)
        {

            return View();
        }
    }
}