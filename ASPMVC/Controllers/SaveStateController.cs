using ASPMVC.DAL;
using ASPMVC.Models;
using DevTrends.MvcDonutCaching;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Caching;
using System.Web.Mvc;

namespace ASPMVC.Controllers
{
    public class SaveStateController : Controller
    {
        private object staticCounterLock = new object();

        /// <summary>
        /// Korzystanie z pól stycznych nie jest dobrym rozwiązaniem, bo często na serwerach
        /// co kilkadziesiąt minut aplikacja jest resetowana (application pool). Należy
        /// używać tego ostrożnie (albo używać wolniejszej bazy danych).
        /// </summary>
        private static int StaticCounter = 1;

        /// <summary>
        /// Funkcja zwracająca inkrementatory.
        /// </summary>
        /// <returns></returns>
        private Counters GetCounters()
        {
            var counters = new Counters();

            //Globalne pole HttpContext.Application[]. Współdzielone przez wszystkich.
            if (HttpContext.Application["counter"] != null)
            {
                counters.ApplicationCounter = (int)HttpContext.Application["counter"];
            }
            else
            {
                counters.ApplicationCounter = 0;
            }

            //Sesja dla 1 użytkownika. Lepiej robić sprawdzanie, bo sesja może zanikać po pewnym czasie.
            if (Session["counter"] != null)
            {
                counters.SessionCounter = (int)Session["counter"];
            }
            else
            {
                counters.SessionCounter = 0;
            }

            //Pobieranie wartości z odesłanego pliku cache
            if (Request.Cookies["counter"] != null)
            {
                counters.CookieCounter = int.Parse(Request.Cookies["counter"].Value);
            }
            else
            {
                counters.CookieCounter = 0;
            }

            if (HttpRuntime.Cache["counter"] != null)
            {
                counters.CacheCounter = (int)HttpRuntime.Cache["counter"];
            }
            else
            {
                counters.CacheCounter = 0;
            }

            return counters;
        }

        public void SetCounters(Counters counters)
        {
            //Przepisanie do globalnego pola aktualnego licznika
            HttpContext.Application["counter"] = counters.ApplicationCounter;

            //Przepisanie wartości do sesji dla danego użytkpownika
            Session["counter"] = counters.SessionCounter;

            //Ustawianie coasteczka 
            HttpCookie cookie = new HttpCookie("counter", counters.CookieCounter.ToString());
            cookie.Expires = DateTime.Now.AddDays(1);   //Ustawienie ważnośći na 24 godziny, domyślnie - do zamknięcia przeglądarki
            //cookie.Expires = DateTime.Now.AddDays(-1); //Jeśli chcemy usunąć ciasteczko, ustawiamy przeterminowaną date
            Response.SetCookie(cookie);

            //HttpRuntime.Cache.Remove("counter");  //usuwanie cache
            //Dodawanie cache (z ustawieniami)
            HttpRuntime.Cache.Add(
                "counter",  //klucz
                counters.CacheCounter,  //wartość
                null,   //zależność (np. od bazy danych)
                DateTime.Now.AddSeconds(5), //absoluty czas do wygasnięcia
                TimeSpan.Zero,  //czas do wygasnięcia od ostatniej aktywności
                CacheItemPriority.Default,  //Piorytet
                null    //delegat do funkcji wykonywany po usunięciu
            );
            //HttpRuntime.Cache["counter"] = counters.CacheCounter; //Szybki sposób na kożystanie z cache
        }

        // Post-Reditect-Get (IncrementForm -Get-> Increment -Redirect-> (client) -Get-> IncrementForm
        public ActionResult IncrementForm()
        {
            var model = GetCounters();
            return View("IncrementForm", model);
        }

        [HttpPost]
        public ActionResult IncrementStaticParallel()
        {
            var counters = GetCounters();
            //Gdy dwóch użytkowników pobierze wartość licznika 1 i ją zwiększy jednoczesnie to licznik nie bedzie wynosił 3, tylko
            //2 (pobranie jedynki z StaticCounter-> powiększe do dwóch -> zapisanie w StaticCounter dwójki przez 1 i 2 użytkownika)
            //Rozwiązanie -> akcja 'Increment()'
            counters.StaticCounter++;

            return RedirectToAction("IncrementForm");
        }

        [HttpPost]
        public ActionResult IncrementStatic()
        {
            //Tutaj może wejść tylko jeden wątek jednocześnie. Inny wątek musi poczekać.
            lock (staticCounterLock)
            {
                var counters = GetCounters();

                counters.StaticCounter++;
            }

            return RedirectToAction("IncrementForm");   //Post-Redirect-Get
        }

        [HttpPost]
        public ActionResult IncrementApplication()
        {
            //Blokowanie dostępu tylko na 1 wątek (aby jednocześnie 2 użytkowników nie inkrementowalo).
            //Podejście nie do końca dobre bo blokuje trochę dostęp do zmiennej
            //(zawężenie przepływu tylko do 1 wątka jednocześnie).
            //HttpContext.Application.Lock();

            var counters = GetCounters();
            //Inkrementuj stan zapisany w HttpContext.Application[] 
            counters.ApplicationCounter++;
            SetCounters(counters);

            //HttpContext.Application.UnLock();

            return RedirectToAction("IncrementForm");   //Post-Redirect-Get
        }

        /// <summary>
        /// Sesja działa po stronie serwera (domyślnie 20 min.). Działa tylko na jedną przeglądarke (nie jest współdzielona)
        /// Ustawiana w web.config (provider):
        /// InProc - domyślna, w pamięci serwera, szybka, nie działa na kilku serwerach (niewspółdzielona)
        /// State Server - oddzielny serwer do współdzielenia stanu sesji. Po restarcie znikają wszystkie sesje. 
        /// SQL - sesja w bazie danych, może przetrwać restart.
        /// Własny.
        /// Sesja tworzona jest po stronie serwera i wysyłana do kliena w Cookies (ASP.NET SessionId).
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult IncrementSession()
        {
            var counters = GetCounters();

            counters.SessionCounter++;

            SetCounters(counters);

            return RedirectToAction("IncrementForm");   //Post-Redirect-Get
        }

        public ActionResult IncrementCookie()
        {
            var counters = GetCounters();

            counters.CookieCounter++;

            SetCounters(counters);

            return RedirectToAction("IncrementForm");   //Post-Redirect-Get
        }

        /// <summary>
        /// Data Caching - globalna struktura, współdzielona
        /// Ustawienia:
        /// Cache Depencies - powiązanie z bazą danych (jak bedzie zmiana w DB, cache wygasa). Zależności do tabelki pliku.
        /// Absolute Expiration - (data ważności)
        /// Sliding Expiration - ważność od ostatniego użycia.
        /// Piorytet - które danę mają być usuwane jako pierwsze (gdy zabraknie pamięci).
        /// Metoda wywoławcza gdy cache bedzie usuwane.
        /// Wiele providerów.
        /// </summary>
        /// <returns></returns>
        public ActionResult IncrementCache()
        {
            var counters = GetCounters();

            counters.CacheCounter++;

            SetCounters(counters);

            return RedirectToAction("IncrementForm");   //Post-Redirect-Get
        }

        public ActionResult Multithreating()
        {
            ProductsContext db = new ProductsContext();

            // Zabezpieczenie przed wielowątkowością. Jeśli używamy pól statycznych, dwóch 
            // użytkowników może na raz modyfikować jakieś pole (co może powodować problemy).
            // Konstrukcja lock pozwala wejść 'do środka' tylko jednemu wątkowi.
            lock (staticCounterLock)
            {
                var row = db.Products.Find(StaticCounter);
                db.Products.Remove(row);

                //W tym momenice drugi wątek mógłby spróbować usunąć rekord bazując na obecnym StaticCounter 
                //(próba usunięcia usuniętego przed chwilą rekordu z niezinkrementowanym 'StaticCounter' -> Exception)

                StaticCounter++;
            }

            // Podobna sytuacja jak powyżej, pole 'Application' jest polem globalnym aplikacji i aby uniknąć
            // sytuacji gdy 2 użytkowników modyfikuje pole globalne, blokujemy pole 'Application' na dostęp pojedyńczych wątków.
            HttpContext.Application.Lock();

            var counter = HttpContext.Application["counter"];

            var product = db.Products.Find(counter);
            db.Products.Remove(product);

            //Tutaj drugi wątek mógłby spróbować usunąć rekord bazując na obecnej zmiennej globalnej
            //(próba usunięcia usuniętego przed chwilą rekordu z niezinkrementowanym pola globalnego -> Exception)
            HttpContext.Application["counter"] = (int)HttpContext.Application["counter"] + 1;

            HttpContext.Application.UnLock();

            return View();
        }

        //
        /// <summary>
        /// Zapamiętywanie zwróconej strony. Po odświerzeniu strony zwrócona zostanie kopia strony. Kopia będzie 
        /// utrzymywana w pamięci przez pięć sekund. Przez to zwracana strona po odświerzeniu nie bedzie ponownie renderowana.
        /// 'VaryByParam' pomaga wymusić ponowne renderowanie gdy w np. Query Stringu zmienia sie jakaś wartość (inne id - renderuj
        /// ponownie strone dla innego produktu / to samo id - nie renderuj i zwróć tą samą strone produktu (z pamięci)).
        /// Gdy strona jest w cache i zostanie odświerzona, akcja kontrolera nie jest wykonywana, tylko zwracana jest cała stona.
        /// Cache działa globalnie.
        /// </summary>
        /// <returns></returns>
        /// 
        //OutputCache - Całkowite cachowanie strony, VaryByParam = "none" - nie patrz na zmieniające się argumenty 
        //[OutputCache(Duration = 5, VaryByParam = "none")] 
        //DonutOutputCache - pozwalanie na umieszanie elementów niecachowanych (Html.Action(...))
        [DonutOutputCache(Duration = 5, VaryByParam = "id")]    
        public ActionResult CacheAttribiute(int id = 1)
        {

            return View();
        }

        /// <summary>
        /// Wymagania:
        /// install-package MvcDonutCaching
        /// 
        /// Przykład użycia Dount caching. Metoda ta pozwala wybierać obszar strony który ma używać cachingu (zwracanie elementu
        /// z cache). Można zwracać element który ma być cachowany(ten przykład), albo całą stone cachowaną z wyjątkiem elementu.
        /// [ChildActionOnly] powoduje że dostęp do tej akcji jest możliwy tylko z poziomu Html helpera. Dzikei temu możemy na 
        /// stronie niecachowanej umieścić cachowany element lub odwrotnie. 
        /// </summary>
        /// <returns></returns>
        //[OutputCache(Duration = 5)]   //Jeśli chcesz cachować tylko jeden element na niecachowanej stronie
        [ChildActionOnly]   //Można do niej się odwoływać tylko z poziomu HTML Helper Action
        public string DonutCaching()
        {
            return DateTime.Now.ToLongTimeString();
        }
    }


}