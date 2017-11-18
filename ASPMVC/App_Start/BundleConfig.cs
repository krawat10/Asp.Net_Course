using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;

namespace ASPMVC.App_Start
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            //Takie bundle łączą kilka plików w jeden oraz usuwają zbędne spacje (kompresuje)
            //na widokach możemy je dodawać jako Scripts.Render("~/bundles/jquery") itp.
            //jednak w trybie debugowania te pliki nie będą łączone. Aby to zmienić idz do Web.config i zmień
            //<compilation debug="true" targetFramework="4.6.1" /> na false
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                "~/Scripts/jquery-{version}.js" //Doda jquery niezależnie od wersji
                ));

            //To będzie jeden plik połączony z dwóch 
            bundles.Add(new StyleBundle("~/bundles/css").Include(
                "~/Content/bootstrap.css",
                "~/Content/Site.css"
                ));
        }
    }
}