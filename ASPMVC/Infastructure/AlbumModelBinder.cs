using ASPMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ASPMVC.Infastructure
{
    /// <summary>
    /// Przykład klasy która może być użyta do parsowania wysłanego rządania na własny model. Klasa musi implementować
    /// interfejs IModelBinder, lub dziedziczyć z DefaultModelBinder. Potem może być ustawiona w Global.asax jako domyślny
    /// Binder (ModelBinders.Binders.Add(typeof(AlbumModelBinder), new AlbumModelBinder());)
    /// lub jako atrybut w kontrolerze (patrz ModelController)
    /// </summary>
    public class AlbumModelBinder :  IModelBinder
    {
        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            HttpRequestBase request = controllerContext.HttpContext.Request; //żadanie wysłane do kontrolera

            string day = request.Form.Get("Day");
            string month = request.Form.Get("Month");
            string year = request.Form.Get("Year");

            string issuedString = $"Wydano dnia: {day}/{month}/{year}.";

            return new Album
            {
                Artist = new Artist { Name = request.Form.Get(@"Artist.Name") },
                Id = int.Parse(request.Form.Get("Id")),
                Name = request.Form.Get("Name"),
                DateString = issuedString
            };
        }
    }
}