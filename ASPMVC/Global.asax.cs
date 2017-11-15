using ASPMVC.Infastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace ASPMVC
{
    //
    //Definiowanie tablicy routingu (początek aplikacji)
    //appstar - zewnetrzny pl
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            //Enginów może być dużo. Pierwszy w liscie (jeśli dane pasują)
            //bedzie wykonywany, jeśli nie da rady - następny. Mogą to być NAML(Ruby), Spark, Nvelocity(Java) itp.
            //Czasami jest do tego potrzeba, gdy np. musimy zwrócić dane które wymagają pewnego przekształcenia (strona w XML).
            //Jeśli nie chcemy używać renderowania przez RazorEngine:
            //ViewEngines.Engines.Clear();
            //ViewEngines.Engines.Add(new MyViewEngine());

            //Zaaplikowanie filtra [TimerAttribiute] do każdej strony
            GlobalFilters.Filters.Add(new TimerAttribiute());     
        }

        
    }
}
