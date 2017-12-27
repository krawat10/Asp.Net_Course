using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ASPMVC.Validation
{
    /// <summary>
    /// Własny atrybut do validacji czy jeśli jedno pole jest true to drugie jest uzupełnione. (Wystaw fakture VAT -> podaj NIP firmy)
    /// Musimy dziedziczyć po 'ValidationAttribute', a jeśli chcemy walidacje po stronie klienta to musimy dodatkowo 
    /// zaimplementować interfejs 'IClientValidatable'
    /// </summary>
    public class RequiredIfTrueAttribute : ValidationAttribute, IClientValidatable
    {
        public string BooleanPropertyName { get; set; }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            //Pobranie i sprawdzenie propercji o nazwie która jest w stringu 'BooleanPropertyName'
            if (GetValue<bool> (validationContext.ObjectInstance, BooleanPropertyName))
            {
                return new RequiredAttribute().IsValid(value) ? //Domyślne sprawdzenie naszego pola (czy required itp). 
                    ValidationResult.Success : new ValidationResult(FormatErrorMessage(validationContext.DisplayName)); 
            }
            return ValidationResult.Success;
        }

        /// <summary>
        /// Pobieranie instancji naszego modelu (Question), próba znalezienia pola z nazwą jaka znajduję się w argumencie 'propertyName'
        /// i zwrócenie tej wartości (z rzutowaniem na właściwy typ);
        /// </summary>
        /// <typeparam name="T">Typ naszego pola</typeparam>
        /// <param name="objectInstance">Instancja klasy która posiada to pole.</param>
        /// <param name="propertyName">Nazwa propertcji</param>
        /// <returns>Wartość propercji</returns>
        private static T GetValue<T>(object objectInstance, string propertyName)
        {
            if (objectInstance == null) throw new ArgumentNullException("objectInstance");
            if (string.IsNullOrWhiteSpace(propertyName))
                throw new ArgumentNullException("propertyName");

            var propertyInfo = objectInstance.GetType().GetProperty(propertyName); //Pobranie położenia propercji

            return (T)propertyInfo.GetValue(objectInstance);//Pobranie wartości propercji z konkretnej instancji klasy (objectInstance)
        }

        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {
            var modelClientValidationRule = new ModelClientValidationRule
            {
                ValidationType = "requirediftrue",//Nazwa walidatora w JS oraz w atrybutach data-
                ErrorMessage = FormatErrorMessage(metadata.DisplayName)//Jak ma wyglądać komunikat o błędzie
            };

            //dodatkowy parameter który zostanie wpisany do htmla pod data-boolprop
            modelClientValidationRule.ValidationParameters.Add("boolprop", BooleanPropertyName);

            yield return modelClientValidationRule;
        }
    }
}