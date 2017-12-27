using ASPMVC.Models;
using System.Web.Mvc;

namespace ASPMVC.Controllers
{
    /// <summary>
    /// Sprawdz: Validation/PriceAttribiute, Validation/RequiredIfTrueAttribute, Models/Questions, Models/PalsValidation
    /// </summary>
    public class ValidationController : Controller
    {
        //
        public ActionResult Index()
        {
            return View();
        }

        //Przykład walidacji formularza 
        //Używamy klasy Question, która ma atrybuty walidacyjne
        public ActionResult FormValidation()
        {
            return View();
        }

        //Walidacja danych formularza
        [HttpPost]
        public ActionResult AddQuestion(Question question)
        {
            //Manualne walidowanie (i zwracanie błędu). Błąd bedzie zwrócony w widoku
            //obok innych błędów w helperze 'Html.ValidationSummary()' oraz obok inputa 'QuestionText'
            if (question.QuestionText == "?")
                ModelState.AddModelError("QuestionText", "Nie może być tego znaku");

            //Jeśli formularz (FormValidation) nie został wykonany poprawnie
            if (!ModelState.IsValid)
            {
                //Zwracamy użytkownikowi ten sam formularz (doajemy model) ale z wypełnionymi wcześniej danymi (żeby je poprawił)
                return View(nameof(FormValidation), question);
            }
            else
            {
                return View(nameof(FormValidation));
            }
        }

        public ActionResult GetForm(PalsValidation pals)
        {
            //Sprawdzanie atrybutów modelu (atrybuty pól w klasie modelu PalsValidation)
            //sprawdzany jest przekazany do kontrolera argument (pals). Można też samemu go edytować.
            if (!ModelState.IsValid)
            {
                return Content("Model is not valid");
            }

            return Content("asdasd");
        }
    }
}