using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace notat.Registry
{
    public class uzytkownik
    {
        public String User { get; set; }
        public String Pass { get; set; }
        public String Files { get; set; }
        public String Name { get; set; }
        public String Phone { get; set; }
        public String Email { get; set; }
        public String Sname { get; set; }

        public uzytkownik() { }
    }
    
    /// <summary>
    /// Class that uses logins
    /// Create them
    /// Checked them
    /// </summary>
    public class RegistryData
    {

        public List<uzytkownik> pliki = new List<uzytkownik>();

        public RegistryData()
        {
            odczyt();
        }

        ~RegistryData()
        {
            zapis();
        }

        /// <summary>
        /// Creating user from login name if there are no other like it
        /// </summary>
        /// <param name="Users"></param>
        /// <param name="Passwo"></param>
        public uzytkownik CreateLogin(String Users, String Passwo)
        {
            if (!SprawdzanieLogin(Users))
            {

                MessageBox.Show("Podany login już istnieje");
                uzytkownik lel = new uzytkownik();
                lel.User = "Error!";
                return lel;
            }
            if (!ZnakiZakazne(Users))
            {
                MessageBox.Show("Podany nick ma zakazane znaki");
                uzytkownik lel = new uzytkownik();
                lel.User = "Error!";
                return lel;
            }
            if (!PasswordCheck(Passwo))
            {
                MessageBox.Show("Nie podałeś hasła");
                uzytkownik lel = new uzytkownik();
                lel.User = "Error!";
                return lel;
            }
            uzytkownik nowy = new uzytkownik();
            nowy.User = Users;
            nowy.Pass = Passwo;
            nowy.Files = Users;
            pliki.Add(nowy);
            zapis();
            return nowy;
        }

        private bool PasswordCheck(string pass)
        {
            if (pass == "") return false;
            else return true;
        }

        /// <summary>
        /// Login to app if login and password are correct
        /// </summary>
        /// <param name="Login"></param>
        /// <param name="Password"></param>
        /// <returns></returns>
        public uzytkownik login(string Login, string Password)
        {
            foreach (uzytkownik e in pliki)
            {
                if (e.User == Login)
                    if (e.Pass == Password)
                        return e;
            }
            uzytkownik lel = new uzytkownik();
            lel.User = "Error!";
            return lel;
        }

        //unchecked
        /// <summary>
        /// Edytuje użytkownika pod względem jedo nazwy
        /// </summary>
        /// <param name="ten"></param>
        public void EditLogin(uzytkownik ten)
        {
            foreach (uzytkownik e in pliki)
            {
                if (e.User == ten.User)
                {
                    e.Pass = ten.Pass;
                    e.Name = ten.Name;
                    e.Phone = ten.Phone;
                    e.Sname = ten.Sname;
                    e.Email = ten.Email;
                }
            }
            zapis();
        }

        ///Metods inside class
        /// <summary>
        /// 
        /// </summary>
        /// <param name="users"></param>
        /// <returns>true if users name is avalible</returns>
        bool SprawdzanieLogin(string users)
        {
            foreach (uzytkownik e in pliki)
            {
                if (e.User == users) return false;
            }
            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Users"></param>
        /// <returns>true if in name there is no chars locked</returns>
        bool ZnakiZakazne(string Users)
        {
            foreach (char s in Users)
            {
                if (s == '!') return false;
                if (s == '?') return false;
                if (s == '/') return false;
                if (s == '"') return false;
                if (s == '\'') return false;
                if (s == ' ') return false;
                if (s == '*') return false;
            }
            return true;
        }

        /// <summary>
        /// Create Users list from file
        /// </summary>
        void odczyt()
        {
            if (File.Exists(@"users\Uzytkownicy.bin"))
            {
                StreamReader odczyt = new StreamReader(@"users\Uzytkownicy.bin");
                string odczytanie = odczyt.ReadToEnd();
                pliki = JsonConvert.DeserializeObject<List<uzytkownik>>(odczytanie);
                odczyt.Close();
            }
            else
            {
                pliki = new List<uzytkownik>();
            }
        }

        /// <summary>
        /// Write Users list to file
        /// </summary>
        void zapis()
        {
            StreamWriter writer = new StreamWriter(@"users\Uzytkownicy.bin");
            string zapis;
            zapis = JsonConvert.SerializeObject(pliki);
            writer.Write(zapis);
            writer.Close();

        }
    }
}
