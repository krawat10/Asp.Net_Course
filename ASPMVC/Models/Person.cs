using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ASPMVC.Models
{
    public class Person
    {
        public int PersonId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public static IEnumerable<Person> GetPersonList()
        {
            var persons = new List<Person>
            {
                new Person { FirstName = "Mateusz", LastName = "Kaminski"},
                new Person { FirstName = "Ewa", LastName = "Kulas"},
                new Person { FirstName = "Maciej", LastName = "Nowal"},
                new Person { FirstName = "Lukasz", LastName = "Wojcik"}
            };

            return persons;
        }
    }
}