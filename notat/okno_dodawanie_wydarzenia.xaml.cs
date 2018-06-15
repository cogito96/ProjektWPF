using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for okno_dodawanie_wydarzenia.xaml
    /// </summary>
    public partial class okno_dodawanie_wydarzenia : Window
    {
        ObservableCollection<Wydarzenie> lista_wydarzen_aktualizacja;
        public okno_dodawanie_wydarzenia(ObservableCollection<Wydarzenie> lista)
        {
            InitializeComponent();
            DateTime aktualna = DateTime.Today;
            kalendarzyk.SelectedDate = aktualna;

            lista_wydarzen_aktualizacja = lista;
            Walidacja walidacja = new Walidacja();
            this.DataContext = walidacja;
            textbox_rozpoczecie.Text = aktualna.Hour.ToString();
            textbox_rozpoczecie_minuty.Text = aktualna.Minute.ToString();

            //walidacja.godzina_walidacja
            //;;walidacja.minuty_walidacja=
        }

        private void przycisk_dodanie_wydarzenia_Click(object sender, RoutedEventArgs e)
        {
            int godzina, minuty;
            string opis;
            string tytul;
            int czas_trwania;
            bool powiadomienie;
            DateTime data;
            int[] rozpoczecie = new int[2];

            if (textbox_tytul.Text != "")
            {
                if (textbox_tytul.GetLineLength(0) <= 30)
                    tytul = textbox_tytul.Text;
                else
                {
                    MessageBox.Show("Tytuł może posiadać maksymalnie 30 znaków!!");
                    return;
                }
            }
            else
            {
                MessageBox.Show("Wpisz poprawnie tytuł!");
                return;
            }


            if (textbox_opis.Text != "")
                opis = textbox_opis.Text;
            else
            {
                MessageBox.Show("Wpisz poprawnie opis!");
                return;
            }

            if (kalendarzyk.SelectedDate == null)
            {
                MessageBox.Show("Wybierz date");
                return;
            }
            else
                data = kalendarzyk.SelectedDate.Value;



            try
            {

                godzina = Int32.Parse(textbox_rozpoczecie.Text);
                minuty = Int32.Parse(textbox_rozpoczecie_minuty.Text);
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



            rozpoczecie[0] = godzina;
            rozpoczecie[1] = minuty;



            try
            {
                czas_trwania = Int32.Parse(textbox_czas_trwania.Text);
            }
            catch
            {
                MessageBox.Show("Niepoprawny format czasu trwania");
                return;
            }



            /*if (radiobutton_pow_wlaczone.IsChecked == true)
                powiadomienie = true;
            else
                powiadomienie = false;*/

            Wydarzenie pomoc_dodanie = new Wydarzenie(tytul, opis, data, rozpoczecie, czas_trwania);
            lista_wydarzen_aktualizacja.Add(pomoc_dodanie);

            this.DialogResult = true;
            this.Close();
        }
    }
}
