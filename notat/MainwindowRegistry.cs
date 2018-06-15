using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Newtonsoft.Json;
using notat.Registry;

namespace notat
{
    public partial class MainWindow : Window
    {
        private void StartUser()
        {
            try
            {
                ImieTextBox.Text = Uzytkownik.Name;
                NazwiskoTextBox.Text = Uzytkownik.Sname;
                TelefonTextBox.Text = Uzytkownik.Phone;
                EmailTextBox.Text = Uzytkownik.Email;
            }
            catch (NullReferenceException) { }
        }

        private void ZapiszButt_Click(object sender, RoutedEventArgs e)
        {
            RegistryData Tmp = new RegistryData();
            if (HasloStareTextBox.Text != "")
            {
                if (HasloStareTextBox.Text != Uzytkownik.Pass)
                {
                    MessageBox.Show("Błędne hasło");
                    return;
                }
                else if (HasloNoweTextBox.Text == HasloPowtTextBox.Text)
                {
                    Uzytkownik.Pass = HasloNoweTextBox.Text;
                }
                else
                {
                    MessageBox.Show("Nowe hasła nie są takie same");
                }
            }
            try
            {
                Uzytkownik.Name = ImieTextBox.Text;
                Uzytkownik.Sname = NazwiskoTextBox.Text;
                Uzytkownik.Phone = TelefonTextBox.Text;
                Uzytkownik.Email = EmailTextBox.Text;
            }
            catch (NullReferenceException) { }
            Tmp.EditLogin(Uzytkownik);


            MessageBox.Show("zmieniono haslo");
            HasloStareTextBox.Text = "";
            HasloNoweTextBox.Text = "";
            HasloPowtTextBox.Text = "";

        }

    }
}
