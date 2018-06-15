using notat.Registry;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace notat
{
    public partial class MainWindow : Window
    {
        int i, j, k, l;

        RegistryAndLogin logi;
        uzytkownik Uzytkownik;
        ObservableCollection<User> lista_kontaktow;
        private void LogingStart()
        {
            
            logi = new RegistryAndLogin();
            if (logi.ShowDialog() == true)
            {
                Uzytkownik = logi.lolo;
            }
            if (!logi.Zamkniencie) Close();
            FileRead();
        }

        //string sciezka = System.IO.Path.GetDirectoryName(System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName) + @"\galeriaZdjec\brak.jpg";
        string nazwa_pliku;
     
       
        

      


         
        
       

       



        



    }
}
