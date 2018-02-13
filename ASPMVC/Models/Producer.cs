using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ASPMVC.Models
{
    public class Producer
    {
        [Key]
        public int ProducerId { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        
        /// <summary>
        /// Na potrzeby bazy danych możemy oznaczyć to pole jako 'virtual'. Chcemy stworzyć połączenie pomiędzy 
        /// tablekami (NestleProducts -> [Cookies, Mars, Pepsi] i w modelu nie musimy trzymać w każdym obiekcie
        /// tabeli wszystkich produktów.
        /// </summary>
        public virtual ICollection<Product> Products { get; set; }
    }
}