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
using System.Windows.Shapes;

namespace notat
{
    /// <summary>
    /// Interaction logic for okno_edycja_wydarzenia.xaml
    /// </summary>
    public partial class okno_edycja_wydarzenia : Window
    {
        Wydarzenie wydarzenie_do_zmiany;
        public okno_edycja_wydarzenia(Wydarzenie wydarzenie_przekazane)
        {
            InitializeComponent();
            wydarzenie_do_zmiany = wydarzenie_przekazane;
            textbox_tytul_edycja.Text = wydarzenie_do_zmiany.tytul.ToString();
            textbox_opis_edycja.Text = wydarzenie_do_zmiany.opis.ToString();
            textbox_czas_trwania_edycja.Text = wydarzenie_do_zmiany.czas_trwania.ToString();
            kalendarzyk_edycja.SelectedDate = wydarzenie_do_zmiany.data;
            textbox_rozpoczecie_edycja.Text = wydarzenie_do_zmiany.data.Hour.ToString();
            textbox_rozpoczecie_minuty_edycja.Text = wydarzenie_do_zmiany.data.Minute.ToString();
            Walidacja walidacja = new Walidacja();
            walidacja.tytul_walidacja = wydarzenie_do_zmiany.tytul;
            walidacja.godzina_walidacja = wydarzenie_do_zmiany.data.Hour;
            walidacja.minuty_walidacja = wydarzenie_do_zmiany.data.Minute;
            walidacja.czas_trwania_walidacja = wydarzenie_do_zmiany.czas_trwania;
            walidacja.opis_walidacja = wydarzenie_do_zmiany.opis;
            this.DataContext = walidacja;
        }

        private void przycisk_potwierdzenia_edycji_Click(object sender, RoutedEventArgs e)
        {
            int godzina, minuty;


            if (textbox_tytul_edycja.Text != "")
            {

                if (textbox_tytul_edycja.GetLineLength(0) <= 30)
                    wydarzenie_do_zmiany.tytul = textbox_tytul_edycja.Text;
                else
                {
                    MessageBox.Show("Tytuł może posiadać maksymalnie 30 znaków!!");
                    return;
                }
            }
            else
            {
                MessageBox.Show("Wpisz poprawnie tytul!");
                return;
            }


            if (textbox_opis_edycja.Text != "")
                wydarzenie_do_zmiany.opis = textbox_opis_edycja.Text;
            else
            {
                MessageBox.Show("Wpisz poprawnie opis!");
                return;
            }


            try
            {
                godzina = Int32.Parse(textbox_rozpoczecie_edycja.Text);
                minuty = Int32.Parse(textbox_rozpoczecie_minuty_edycja.Text);
            }
            catch
            {
                MessageBox.Show("Nieprawidłowy format godziny.(HH:MM)");
                return;
            }
            if (godzina > 23 || godzina < 0 || minuty > 60 || minuty < 0)
            {
                MessageBox.Show("Nieprawidłowa godzina");
                return;
            }





            if (kalendarzyk_edycja.SelectedDate == null)
            {
                MessageBox.Show("Wybierz date");
                return;
            }
            else
                wydarzenie_do_zmiany.data = new DateTime(kalendarzyk_edycja.SelectedDate.Value.Year, kalendarzyk_edycja.SelectedDate.Value.Month, kalendarzyk_edycja.SelectedDate.Value.Day, godzina, minuty, 0);








            try
            {
                wydarzenie_do_zmiany.czas_trwania = Int32.Parse(textbox_czas_trwania_edycja.Text);
            }
            catch
            {
                MessageBox.Show("Niepoprawny format czasu trwania");
                return;
            }




            wydarzenie_do_zmiany.tytul = textbox_tytul_edycja.Text;
            wydarzenie_do_zmiany.opis = textbox_opis_edycja.Text;
            wydarzenie_do_zmiany.czas_trwania = Int32.Parse(textbox_czas_trwania_edycja.Text);


            /*if (radiobutton_pow_wlaczone_edycja.IsChecked == true)
                wydarzenie_do_zmiany.powiadomienie = true;
            else
                wydarzenie_do_zmiany.powiadomienie = false;*/


            int[] rozpoczecie = new int[2];
            rozpoczecie[0] = godzina;
            rozpoczecie[1] = minuty;
            wydarzenie_do_zmiany.godzina_rozpoczecia = rozpoczecie;
            wydarzenie_do_zmiany.cale_wydarzenie = "Tytuł: " + wydarzenie_do_zmiany.tytul.ToString() + Environment.NewLine + Environment.NewLine + "Opis: " + wydarzenie_do_zmiany.opis.ToString() + Environment.NewLine + Environment.NewLine + "Data i godzina rozpoczęcia: " + wydarzenie_do_zmiany.data.ToString() + Environment.NewLine + Environment.NewLine + "Czas trwania: " + wydarzenie_do_zmiany.czas_trwania.ToString();
            this.DialogResult = true;
            this.Close();

        }
    }
}
