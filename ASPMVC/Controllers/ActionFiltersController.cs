using ASPMVC.Infastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ASPMVC.Controllers
{
    public class ActionFiltersController : Controller
    {

        //[Authorize] - tak wstawimy to dostaniemy 401 (Unauthorized)
        //Tutaj gdy wystąpi określony wątek (DivideByZeroException), filter przekeirowuje 
        //do odpowiedniej stony błędu (DivideException.cshtml)
        [HandleError(ExceptionType = typeof(DivideByZeroException), View = "DivideException")]
        public ActionResult Index()
        {
            var zero = 0;
            float f = 2 / zero;

            return View();
        }

        /// Przykład implementacji własnej klasy (infastucture/TimerAttribiute.cs)
        /// która będzie mierzyć czas. Klasa TimerAttribiute mierzy czas od czasu wykonywania akcji do jej zakonczenia
        /// oraz zapisuje do ViewBag.Duration czas wykonywania, wiec można go wyświetlić na stronie
        /// <returns></returns>
        [TimerAttribiute]
        public ActionResult ElapsedTime()
        {
            return View();
        }

        /// Jeśli w kontrolerze wystąpi wyjątek (tutaj jeśli name == null)
        /// to zostanie wykonane przekierowanie na inną strone (ArgError.cshtml)
        [HandleError(ExceptionType = typeof(ArgumentException), View = "ArgError")]
        public ActionResult GetProduct(string name)
        {
            if(name == null)
            {
                throw new ArgumentException("name");
            }
            return View();
        }

        /// Tutaj wysłane do przeglądarki cache będzie poprawne tylko przez 60 sekund
        [OutputCache(Duration = 60)]
        ///Do tej metody (lub nawet kontrolera) mają dostęp tylko użytkownicy z rolą Admin
        [Authorize(Roles = "Admins")]
        public ActionResult OtherFilters()
        {
            return View();
        }
    }

    //Action filters można tworzyć własne. Dziedziczą one po ActionFilterAttribiute/FilterAttribiute
    //Musimy wtedy zaimplementować 4 metody.
    //Można też dodawać interfejsy związane z filtrowaniem (IAuthorizationFilter)
    public class ExampleFilterAttribiute : ActionFilterAttribute, IAuthorizationFilter
    {
        //Po wykonaniu akcji
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            base.OnActionExecuted(filterContext);
        }

        //Tuż przed wykonaniem akcji
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);
        }

        //Metoda z interfejsu IAuthorizationFilter
        public void OnAuthorization(AuthorizationContext filterContext)
        {
            throw new NotImplementedException();
        }

        //Po wykonaniu rezultatu (po wróceniu View)
        public override void OnResultExecuted(ResultExecutedContext filterContext)
        {
            base.OnResultExecuted(filterContext);
        }

        //Przed zwróceniem rezultatu
        public override void OnResultExecuting(ResultExecutingContext filterContext)
        {
            base.OnResultExecuting(filterContext);
        }
    }
}