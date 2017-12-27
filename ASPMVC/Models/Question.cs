using ASPMVC.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ASPMVC.Models
{
    //Dodanie specjalnego interfejsu 'IValidatableObject' do sprawdzania poprawości manualnie.
    public class Question : IValidatableObject
    {
        [ScaffoldColumn(false)]
        public int QuestionId { get; set; }

        [Display(Name = "Question:")]   //Input formularza będzie się nazywał "Question:" a nie "QuestionText"
        [DataType(DataType.MultilineText)]
        public string QuestionText { get; set; }

        [Required(ErrorMessage = "Email field is empty.")]  //Wyświetlany error przy inpucie
        [EmailAddress]  //zmienia typ inputu na email (przeglądarka werfikuje pole)
        [Display(Name = "Email adress:")]
        public string Email { get; set; }

        [Display(Name = "Do you want to contact by phone:")]
        public bool PhonePreferred { get; set; }

        [RequiredIfTrue(BooleanPropertyName = "PhonePreferred", ErrorMessage = "If you preffer phone number, type it")]
        [Phone] //Zmienia typ inputa(do wprowadzania numeru)
        [RegularExpression(@"[\+]{0,1}([0-9]{2})?[\-\s]?[-]?([0-9]{3})\-?([0-9]{3})[-\s]\-?([0-9]{3})$", 
            ErrorMessage = "Invalid phone format")]
        [Display(Name = "Phone number:")]
        public string PhoneNumber { get; set; }

        /// <summary>
        /// Sprawdzanie poprawnośći modelu. Można tu wprowdzić logikę że np. (jeśli 2 pola są wypełnione to 3 pole ma być puste).
        /// </summary>
        /// <param name="validationContext"></param>
        /// <returns></returns>
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (this.Email.Contains("spam"))
            {
                yield return new ValidationResult("Email doesn't look like a correct one.", new string[] { "Email" });
            }
        }
    }   
}