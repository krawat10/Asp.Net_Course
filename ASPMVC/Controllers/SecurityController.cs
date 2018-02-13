using ASPMVC.Models;
using Microsoft.Security.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

/// <summary>
/// ASP.NET Indentity
/// authentication - checking id
/// authorization - checking permissions of user
/// Wiecej -> Account project
/// 
/// Cross Site Scriptiong (XSS)
/// Wstrzykiwanie kodu JAvaScript na stronie internetowej
///     Kradzież ciasteczek
///     Modyfikacja strony (przekierowanie formularza na adres atakującego)
///     
/// Typy:
///     Active Injection - np. dodanie formularza do wyszukiwarki ("mypage.pl/Search= <br><form>Login</form></br>")
///     I wysłanie tego komuś.
///     
///     Passive Injection - np. wpisanie do komentarza kodu JavaScript (<script>Coś złego</script>) 
/// 
/// NIGDY NIE UFAJ DANYM OD UŻYTKOWNIKA!!!
/// NIE WYŚWIETLAJ DANYCH UŻYTKOWNIKA BEZ ENKODOWANIA - domyślnie zwracane dane są enkodowane. Można się to wyłączyć
/// przez [ValidateInput(false)], Http.Raw, AntXSS library.
/// 
/// 
/// </summary>
namespace ASPMVC.Controllers
{
    public class SecurityController : Controller
    {
        /// In web config:
        /// <authentication mode="Forms">
        ///    <forms loginUrl="~/Account/Login" timeout="2800" />
        /// </authentication>
        /// 
        /// Windows Authentication - for intranet  
        /// Forms Authentication - for internet, login form, user DB, use SSL [RequireHttps], Uder.Indedity
        /// Provider - where db of users are, default - Entity Framework.
        /// </summary>
        /// <returns></returns>
        public ActionResult Authentication()
        {
            if(User.Identity.IsAuthenticated)
            {
                //Check if user is singed in
            }

            return View();
        }

        //Controller only for Admins and only Krawat and Ken
        //
        [Authorize(Roles = "Admin", Users = "Krawat, Ken")]
        public ActionResult Authorize()
        {

            //Check user role
            if(User.IsInRole("Contributor"))
            {

            }

            return View();
        }

        public static List<Question> Questions { get; set; } = new List<Question>();


        // GET: Security
        public ActionResult Index()
        {
            return View(SecurityController.Questions);
        }

        [ValidateInput(false)] //We can add strings like "<div><form><input name="password...."
        public ActionResult AddQuestion(Question question)
        {
            //AntiXSS library - get only safe html fragment (without <script>, <forms> etc.)
            question.QuestionText = Sanitizer.GetSafeHtmlFragment(question.QuestionText);

            SecurityController.Questions.Add(question);
            return RedirectToAction(nameof(Index));
        }

    }
}