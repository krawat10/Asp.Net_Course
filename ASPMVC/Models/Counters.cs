using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ASPMVC.Models
{
    public class Counters
    {
        private static int staticCounter;

        public int StaticCounter {
            get
            {
                return Counters.staticCounter;
            }
            set
            {
                Counters.staticCounter = value;
            }
        }

        public int ApplicationCounter { get; set; }
        public int SessionCounter { get; set; }
        public int CookieCounter { get; set; }
        public int CacheCounter { get; set; }

    }
}