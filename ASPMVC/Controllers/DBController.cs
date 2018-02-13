using ASPMVC.DAL;
using ASPMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ASPMVC.Controllers
{
    /// <summary>
    /// Bazy danych możemy używać na dwa sposoby:
    /// ADO.NET - prymitywne łączenie z bazą i budowanie zapytań (mieszanie C# i SQL)
    /// Entity Framework, nHibernate etc. - frameworki pomagające reprezentowanie
    /// danych z bazy danych (DbTable <> Model). 
    /// Kod z Entity Framweork tworzony jest na 3 sposoby:
    /// 1. DB first - mamy utworzoną baze danych i tworzymy do tego model (Model.QuestionDb)
    /// 2. Code First - Narpierw tworzymy modele w C# a potem na tej postawie table
    /// 3. Model First - tworzymy model w designerze a potem tabele w db oraz modele w C#
    /// </summary>
    public class DbController : Controller
    {
        // GET: Db
        public ActionResult DbFirst()
        {
            /// Baza danych z question (DB First)
            DbCourseEntities db = new DbCourseEntities();

            var questions = db.Questions.ToArray();   //Wszystkie rekordy z db

            return View();
        }

        /// <summary>
        /// Code first składa się z:
        /// 1. Modelu (ASPMVC.Models.Product)
        /// 2. Klasy kontekstu danych (ASPMVC.DAL.ProductsContext)- reprezentacje DB, odczyt/zapis, 
        /// DbSet<Model>, można często tworzyć instancje
        /// 
        /// Initalizery - Automatyczne tworzenie/usuwanie/inicjowanie bazy danych
        /// Code first migration - aktualizowanie schematu bazy danych bez uszkadzania wcześniejszych danych
        /// 
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult CodeFirst()
        {
            Product product = new Product
            {
                Id = 1,
                Cost = 12,
                Country = "Poland",
                ProducerId = 1,
                Title = "Mars"
            };

            ProductsContext db = new ProductsContext();
            db.Products.Remove(product);    //Usuwanie rekordu

            //Zmiana stanu (wiadomość dla Entity Framework)
            // Gdy stan jest 'Modified', Entity Framework spróbuje zaktualizować pola danego produktu.
            // Gdy stan jest 'Deleted', Entity Framework usunie rekord.
            // Gdy stan jest 'Added', Entity Framework spróbuje dodać rekord.
            db.Entry(product).State = System.Data.Entity.EntityState.Modified; 

            return View();
        }

        //Dodaj rekord do bazy danych (Code First)
        public ActionResult AddProduct(Product product)
        {
            //Walidacja
            if(!ModelState.IsValid)
            {
                return View("CodeFirst", product);
            }
            else
            {
                //Dodawanie do DB
                ProductsContext db = new ProductsContext();
                db.Products.Add(product);
                db.SaveChanges();

                return View("CodeFirst");
            }
        }

        /// <summary>
        /// Pokazanie producenta i jego produktów. Widok został wygenerowany przez scaffolding.
        /// Template: Details
        /// Model: Producer
        /// Data context class: "ProductsContext"
        /// </summary>
        /// <param name="producerId"></param>
        /// <returns></returns>
        public ActionResult ShowProducerProducts(int producerId)
        {
            ProductsContext db = new ProductsContext();

            //Metoda find jest lepsza od zwykłago zapytania bo sprawdza w pamięci
            //context czy rekord był wcześniej wywoływany (i zwraca go bezpośrednio z Cache).
            var customer = db.Producers.Find(producerId);

            //Wersja z linq (query syntax). Zapytania są wykonywane po stronie bazy danych.
            var query = from p in db.Producers where p.Name == "Pepsi" select p;
            //Zwrócenie pierwszego albo default (method syntax)
            var producer = query.FirstOrDefault();

            //Dołaczenie powiązanych danych (virtual)
            var product = db.Products.Include("Producers");

            return View(customer);
        }
    }
}