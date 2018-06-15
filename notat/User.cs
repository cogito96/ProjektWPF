using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace notat
{
    class User
    {
        public string imie { get; set; }
        public string nazwisko { get; set; }
        public string ksywa { get; set; }
        public string mail { get; set; }
        public int telefon { get; set; }
        public string sciezka_zdjecie { get; set; }
        public User(string imie_osoby, string nazwisko_osoby, string ksywa_osoby, string mail_osoby, int telefon_osoby, string sciezka_foto)
        {
            this.imie = imie_osoby;
            this.nazwisko = nazwisko_osoby;
            this.ksywa = ksywa_osoby;
            this.mail = mail_osoby;
            this.telefon = telefon_osoby;
            this.sciezka_zdjecie = sciezka_foto;//@"D:\gowno.jpg";

        }
    }
}
