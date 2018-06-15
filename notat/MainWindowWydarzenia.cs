using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
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
        ObservableCollection<Wydarzenie> lista_wydarzen;


        private void inicjalizacja_systemu_wydarzen(string nazwa_pliku)
        {
            //lista_posortowana_datami = new List<Wydarzenie>();
            wczytanie_wydarzenia(nazwa_pliku);
            zaznaczenie_na_kalendarzu();
            kalendarz.SelectedDate = DateTime.Now;
            //czasomierz_inicjalizacja();
        }

        private void przycisk_nowe_wydarzenie_Click(object sender, RoutedEventArgs e) //tworzenie nowego wydarzenia
        {
            okno_dodawanie_wydarzenia okno_dodawanie = new okno_dodawanie_wydarzenia(lista_wydarzen);
            okno_dodawanie.ShowDialog();
            if (okno_dodawanie.DialogResult == true)
            {
                zaznaczenie_na_kalendarzu();
                listbox_wydarzenia.ItemsSource = null;
                listbox_wydarzenia.Items.Clear();
                listbox_wydarzenia.ItemsSource = Wydarzenia_z_zaznaczonego_dnia(kalendarz.SelectedDate.Value);
                //lista_posortowana_datami.Clear();
                //sortowanie_wydarzen();
                zapis_wydarzenia(WydarzeniaPlik);
            }
        }

        void wczytanie_wydarzenia(string sciezka)  // wczytanie wydarzen z pliku do kolekcji
        {

            if (File.Exists(sciezka))
            {
                System.IO.StreamReader odczyt = new System.IO.StreamReader(sciezka);
                string odczytanie = odczyt.ReadToEnd();
                lista_wydarzen = JsonConvert.DeserializeObject<ObservableCollection<Wydarzenie>>(odczytanie);
                odczyt.Close();
            }
            else
            {
                lista_wydarzen = new ObservableCollection<Wydarzenie>();
            }
        }

        void zapis_wydarzenia(string sciezka) // zapisanie kolekcji wydarzen do pliku
        {
            StreamWriter writer = new StreamWriter(WydarzeniaPlik);
            string zapis;
            zapis = JsonConvert.SerializeObject(lista_wydarzen);
            writer.Write(zapis);
            writer.Close();
        }

        private void kalendarz_SelectedDatesChanged(object sender, SelectionChangedEventArgs e) //przy zmianie daty na kalendarzu zmienia sie itemsource listboxa
        {

            zaznaczenie_na_kalendarzu();
            if (kalendarz.SelectedDate != null)
            {
                listbox_wydarzenia.ItemsSource = null;
                listbox_wydarzenia.Items.Clear();
                listbox_wydarzenia.ItemsSource = Wydarzenia_z_zaznaczonego_dnia(kalendarz.SelectedDate.Value);
            }
        }

        private void zaznaczenie_na_kalendarzu() //metoda zaznacza na kalendarzu daty
        {

            for (int i = 0; i < lista_wydarzen.Count(); i++)
            {
                kalendarz.SelectedDates.Add(lista_wydarzen[i].data);
            }
        }

        public List<Wydarzenie> Wydarzenia_z_zaznaczonego_dnia(DateTime data_wydarzenia) //zwraca liste wydarzen z zaznaczonego dnia (potrzebne do itemsource listbox'a)
        {
            List<Wydarzenie> lista_wydarzen_zwrocenie = new List<Wydarzenie>();

            if (data_wydarzenia != null)
            {
                foreach (Wydarzenie ev in lista_wydarzen)
                {
                    if (ev.data.Year == data_wydarzenia.Year && ev.data.Month == data_wydarzenia.Month && ev.data.Day == data_wydarzenia.Day)
                    {
                        lista_wydarzen_zwrocenie.Add(ev);
                    }
                }
            }
            return lista_wydarzen_zwrocenie;
        }

        private void Edycja_wydarzenia(object sender, ExecutedRoutedEventArgs e)
        {
            Wydarzenie zaznaczone = (Wydarzenie)listbox_wydarzenia.SelectedItem;
            okno_edycja_wydarzenia okno_edytowanie = new okno_edycja_wydarzenia((Wydarzenie)listbox_wydarzenia.SelectedItem);
            okno_edytowanie.ShowDialog();
            if (okno_edytowanie.DialogResult == true)
            {
                listbox_wydarzenia.ItemsSource = null;
                listbox_wydarzenia.Items.Clear();
                listbox_wydarzenia.ItemsSource = Wydarzenia_z_zaznaczonego_dnia(kalendarz.SelectedDate.Value);
                zaznaczenie_na_kalendarzu();
                listbox_wydarzenia.SelectedItem = null;
                listbox_wydarzenia.SelectedItem = zaznaczone;
                //lista_posortowana_datami.Clear();
                //sortowanie_wydarzen();
                zapis_wydarzenia(WydarzeniaPlik);
            }
        }

        private void Usuniecie_wydarzenia(object sender, ExecutedRoutedEventArgs e)
        {
            MessageBoxResult potwierdzenie = MessageBox.Show("Chcesz usunąć wybrane wydarzenie?", "Potwierdzenie", MessageBoxButton.YesNo);
            switch (potwierdzenie)
            {
                case MessageBoxResult.Yes:
                    lista_wydarzen.Remove((Wydarzenie)listbox_wydarzenia.SelectedItem);
                    listbox_wydarzenia.ItemsSource = null;
                    listbox_wydarzenia.Items.Clear();
                    listbox_wydarzenia.ItemsSource = Wydarzenia_z_zaznaczonego_dnia(kalendarz.SelectedDate.Value);
                    zaznaczenie_na_kalendarzu();
                    //lista_posortowana_datami.Clear();
                    //sortowanie_wydarzen();
                    zapis_wydarzenia(WydarzeniaPlik);
                    break;
            }
        }

        private void Edycja_aktywacja(object sender, CanExecuteRoutedEventArgs e)
        {
            if (listbox_wydarzenia.SelectedItem != null)
                e.CanExecute = true;
            else e.CanExecute = false;
        }

        private void Usuniecie_aktywacja(object sender, CanExecuteRoutedEventArgs e)
        {
            if (listbox_wydarzenia.SelectedItem != null)
                e.CanExecute = true;
            else e.CanExecute = false;
        }
    }
}
