using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace notat
{

    public class Wydarzenie
    {
        public string tytul { get; set; }
        public string opis { get; set; }
        public DateTime data { get; set; }
        public int[] godzina_rozpoczecia { get; set; }
        public int czas_trwania { get; set; }
        public string cale_wydarzenie { get; set; }


        public Wydarzenie(string tytul_wydarzenia, string opis_wydarzenia, DateTime data_wydarzenia, int[] godzina_rozpoczecia_wydarzenia, int czas_trwania_wydarzenia)
        {
            this.tytul = tytul_wydarzenia;
            this.opis = opis_wydarzenia;
            this.data = new DateTime(data_wydarzenia.Year, data_wydarzenia.Month, data_wydarzenia.Day, godzina_rozpoczecia_wydarzenia[0], godzina_rozpoczecia_wydarzenia[1], 0);
            this.godzina_rozpoczecia = godzina_rozpoczecia_wydarzenia;
            this.czas_trwania = czas_trwania_wydarzenia;
            this.cale_wydarzenie = "Tytuł: " + tytul.ToString() + Environment.NewLine + Environment.NewLine + "Opis: " + opis.ToString() + Environment.NewLine + Environment.NewLine + "Data i godzina rozpoczęcia: " + data.ToString() + Environment.NewLine + Environment.NewLine + "Czas trwania: " + czas_trwania.ToString();
        }
        public Wydarzenie()
        {

        }
    }
}
