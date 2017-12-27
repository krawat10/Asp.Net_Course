using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ASPMVC.Models
{
    public partial class PalsValidation
    {
        //Użycie adnotacji do walidacji modelu (System.ComponentModel.DataAnnotations)
        [Required()]    //Pole wymagane
        [Range(33,99)]  //Przedział inta
        public float Height { get; set; }

        [Required()]    //Pole wymagane
        [StringLength(7)]   //Max. długość stringa
        public string Name { get; set; }

        [ScaffoldColumn(false)] //Ignoruj kolumny przez helpery
        public int ID { get; set; }
        
        public bool Cool { get; set; }

        [DataType(DataType.EmailAddress)]   //Typ danych
        [RegularExpression(@"^[0-9a-zA-Z]([-.\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,9})$")]  //Wyrażenie reguralne
        public string Email { get; set; }

        [DataType(DataType.MultilineText)]
        public string Bio { get; set; }

        [HiddenInput]   //Wyświetlaj jako pole ukryte
        [UIHint("szablon")] //Używany szablon dla tego elementu (zdefiniowany w "Shared")
        public string Other { get; set; }

        public int AttributeFromBudyClass { get; set; }
    }

    //Jeśli chcemy zostawić czystą klase modeu (bez zaciemniania jej atrybutami), możemy stworzyć "Buddy class" które posiada te same pola
    //co nasza czysta klasa ale ma atrybuty walidacyjne. Aby te atrybuty działały w czystej klasie musimy do niej dodać atrybut:
    //
    //[MetadataType(typeof(KlasaZAtrybutami))]
    //public class CzystaKlasa
    //{ ...
    [MetadataType(typeof(PalsValidationMetadata))]
    public partial class PalsValidation
    {
        class PalsValidationMetadata
        {
            [Required(ErrorMessage = "Field is required.")] //Jeśli nie waliduje - dodaj error
            public int AttributeFromBudyClass { get; set; }
        }

    }
}