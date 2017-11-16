using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ASPMVC.Models
{
    public class HelpersModel
    {
        public string String { get; set; }
        public Decimal Decimal { get; set; }
        public bool Bool { get; set; }

        /// <summary>
        /// Tutaj dodaliśmy atrybut przez co jak to zwrócimy do widoku te pole bedzie zmodyfikowane jak w 
        /// ~/Views/Shared/EditorTemplateDateTimePicker.cshtml (dla EditFor())
        /// </summary>
        [UIHint("DateTimePicker")]
        public DateTime DateTime { get; set; }
    }
}