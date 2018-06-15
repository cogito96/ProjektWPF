using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace notat
{
    
        public class Note
        {
            public string title { get; set; }
            public string data { get; set; }
            public string desc { get; set; }

            public string wyswietlany;



            public override string ToString()
            {
                this.wyswietlany = this.title + " " + this.data;
                return wyswietlany;
            }


        }

    }

