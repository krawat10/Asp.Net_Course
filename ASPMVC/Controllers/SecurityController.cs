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
///     Istnieje równieź wstrzyknięcie kodu JS który może dostać się do cookies i wysłać z naszej strony jakiś formularz logowania
///     z wartościami z cookies. 
///         Nowe przeglądarki mają właściwość ciasteczek po stronie HTTPonly, czyli nie można ich użyć w kodzie JS.
///         Response.Cookies["MySecretCookie"].HttpOnly=true;
/// 
/// NIGDY NIE UFAJ DANYM OD UŻYTKOWNIKA!!!
/// NIE WYŚWIETLAJ DANYCH UŻYTKOWNIKA BEZ ENKODOWANIA - domyślnie zwracane dane są enkodowane. Można się to wyłączyć
/// przez [ValidateInput(false)], Http.Raw, AntXSS library.
/// 
/// 
/// Cross Site Request Forgery (CSRF)
/// Wiele stron zapamiętuje użytkownika. Atak polega na wykorzystaniu zalogowania
/// - Wysłanie linku mailem typu http://my-bank.com?function=transfer&amount=1000&toAccount=1232332
/// - Wstawienie linku pod komentarzami banku(XSS) <img src="http://my-bank.com?function=transfer&amount=1000&toAccount=1232332"/>
/// 
/// 
/// Komunikaty o błedach - ustawić <compilation debug="false"> aby ukryć informacje o błedach serwera
/// 
/// 
/// Zabezpieczenie kontrolerów [Authorize]
/// 
/// 
/// Zabezpieczenie publicznych metod - [NonAction]
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
        /// 
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
        [HttpPost] //Should be POST request
        [ValidateAntiForgeryToken] //Check if form has hidden input with token (anti XSS/CSRF)
        public ActionResult AddQuestion(Question question)
        {
            //AntiXSS library - get only safe html fragment (without <script>, <forms> etc.)
            question.QuestionText = Sanitizer.GetSafeHtmlFragment(question.QuestionText);

            SecurityController.Questions.Add(question);
            return RedirectToAction(nameof(Index));
        }


        // CSRF - attack example. Let's suppose that user will get email with link to this page:
        //
        // <body onload="document.getElementById("fm1").submit()"> <- automatically submitted 
        //  <form id="fm1" action="http://MY-PAGE.com/Seciurity/SubmitUpdate" method="post"> <- post action to our page 
        //      <input name="email" value="hacker@email.com" /> <- hacker's email
        //      <input name="hobby" value="hacker hobby" /> <- hacker's hobby
        //  </form>
        // </body>
        //
        // It executes this action, because user is saved by broswer (remember me).
        public ActionResult SubmitUpdate()
        {
            UserProfile profile = GetUserProfile();
            profile.Email = Request.Form["Email"];
            profile.Hobby = Request.Form["Hobby"];
            SaveChanges(profile);

            ViewBag.Message = "Your profile was updated";

            return View();
        }
        // Solution - 
        //      Always use POST to this types of actions.
        //      Put hidden input in every type of login pages (hidden token)
        //          [ValidateAntiForgeryToken] in post action
        //          @HTML.AntiForgeryToken() in form
        //      Referer - check source of POST request (which page sends POST query)


        //Just example...
        public static UserProfile GetUserProfile()
        {
            return new UserProfile(); //Just example...
        }

        public static void SaveChanges(UserProfile userProfile)
        { }

    }

    public class UserProfile
    {
        public string Email { get; set; }
        public string Name { get; set; }
        public string Hobby { get; set; }


    }
}