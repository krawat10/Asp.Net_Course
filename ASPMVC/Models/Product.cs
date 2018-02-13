using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ASPMVC.Models
{
    public class Product
    {
        //Atrybuty walidacji modelu będą również wykorzystane w Entity Framework (wymagane kolumny)
        [Required(ErrorMessage = "Id is necessary.")]   
        [Key]   //Oznaczenie tej propercji jako klucz główny w DB
        public int Id { get; set; }

        //Pole w db max 250 długości.
        [StringLength(maximumLength: 250, ErrorMessage = "Title is to long.")]
        public string Title { get; set; }
    
        public decimal Cost { get; set; }

        //Jeśli dodany nowe pole i mamy stare dane w bazie danych (bez nowego pola) należy:
        // 1. Open Package Manager Console 
        //
        // 2. 'Enable-Migrations -ContextTypeName ASPMVC.DAL.ProductsContext'
        //
        // 3. W folderze Migration zostanie utworzone '{data}_InitialCreate.cs' oraz 'Configuration.cs'.
        // Są to początkowe schematy bazy danych.
        //
        // 4. Migracja danych 'Add-Migration AddProductCountry' - Utworzy sie w folderze migration
        // '{data}_AddProductCountry.cs' - definiowanie jak ma migracja przebiegać (jakie pole itp.)
        //
        // 5. 'Update-Database' - zainicjowanie zmian w rzeczywistej bazie danych
        // Jeśli chcemy cofnąć zmiany należy:
        // Update-Database -TargetMigration:"InitialCreate"
        public string Country { get; set; }

        /// <summary>
        /// Klucz obcy do utworzenia powiązania w bazie danych pomiedzy produkami a klientem.
        /// Nazwa jest taka sama jak [Key] w klasie Producer.cs.
        /// </summary>
        public int ProducerId { get; set; }
    }


}