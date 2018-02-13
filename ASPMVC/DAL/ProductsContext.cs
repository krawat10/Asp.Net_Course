using ASPMVC.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ASPMVC.DAL
{
    public class ProductsContext : DbContext 
    {
        public ProductsContext()
            :base("ProductsConnectionString") //Podanie nazwy connection stringa
        {

        }

        public DbSet<Product> Products { get; set; }    //Encja modelu (reprezentacja tabeli bazy danych)
        public DbSet<Producer> Producers { get; set; } //Producenci produktów (jeden klient do wielu produktów)

        public System.Data.Entity.DbSet<ASPMVC.Models.Customer> Customers { get; set; }
    }
}