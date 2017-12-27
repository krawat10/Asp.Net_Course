using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ASPMVC.Models
{
    //Przy kontrolerach parametr Role nigdy nie bedzie bindowane
    [Bind(Exclude = "Role")]
    public class Album
    {
        
        public int Id { get; set; }
        public string Name { get; set; }
        public Artist Artist { get; set; }
        public string DateString { get; set; }

        public string Role { get; set; }
    }

    public class Artist
    {
        public string Name { get; set; }
    }
}