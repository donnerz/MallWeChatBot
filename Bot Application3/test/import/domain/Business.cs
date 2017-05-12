using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Test
{
    public class Business
    {
        public string Business1 { get; set; }


        public string Business2 { get; set; }


        public string Business3 { get; set; }

        public string Brand { get; set; }


        public override string ToString()
        {
            return "Business1 = " + Business1 + " ; Business2 = " + Business2 + " ; Business3 = " + Business3 + " ; Brand = " + Brand ;
        }

    }
}