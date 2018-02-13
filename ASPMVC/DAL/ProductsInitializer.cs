using ASPMVC.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ASPMVC.DAL
{
    /// <summary>
    /// Klasa dziedziczy po 'DropCreateDatabaseAlways<DbSet>. Jest to klasa która bedzie po otwarciu aplikacji
    /// dropowała baze danych i tworzyła nową baze z danymi zdefiniowanymi w funkci 'Seed()'.
    /// W Application
    /// </summary>
    public class ProductsInitializer : DropCreateDatabaseAlways<ProductsContext>
    {
        protected override void Seed(ProductsContext context)
        {
            //Tworzenie listy producentów
            var producers = new List<Producer>
            {
                new Producer() { Category="food", Name="Nestle" },
                new Producer() { Category="pharma", Name="Olimp" },
            };

            //Dodanie producentów do bazy danych
            producers.ForEach(x => context.Producers.Add(x));
            context.SaveChanges();

            //Tworzenie listy produktów
            var products = new List<Product>
            {
                new Product() { ProducerId = 2, Title = "Vitamins", Cost=12, Country="USA", Id=1},
                new Product() { ProducerId = 1, Title = "Nesquic", Cost=4, Country="Poland", Id=2},
                new Product() { ProducerId = 2, Title = "ZMA", Cost=29, Country="Hungary", Id=3}
            };

            //Dodanie produktów do bazy danych
            products.ForEach(x => context.Products.Add(x));
            context.SaveChanges();
        }
    }
}