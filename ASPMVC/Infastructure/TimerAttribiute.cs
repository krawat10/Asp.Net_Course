using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ASPMVC.Infastructure
{
    /// <summary>
    /// Przykład klasy którą można implementować jako filter w akcjach kontrolera [TimerAttribiute]
    /// </summary>
    public class TimerAttribiute : ActionFilterAttribute
    {
        public TimerAttribiute()
        {
            //Kiedy ma być wykonywany. W tym wypadku tuż przed wykonywaniem akcji
            this.Order = int.MaxValue;
        }

        private Stopwatch _stopwatch;

        /// Przed wykonaniem akcji inicjowany jest stoper do mierzenia czasu
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var controller = filterContext.Controller;

            if(controller != null)
            {
                var stopwatch = new Stopwatch();
                _stopwatch = stopwatch;
                stopwatch.Start();
            }
        }

        /// Po wykonaniu akcji stoper zatrzymuje się i czas zapisywany jest do ViewBag'u, który jest pobrany z filterContext
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            var controller = filterContext.Controller;
            
            if(controller != null)
            {
                var stopwatch = (Stopwatch)_stopwatch;

                stopwatch.Stop();
                controller.ViewBag.Duration = stopwatch.Elapsed.TotalMilliseconds;
            }
        }
    }
}