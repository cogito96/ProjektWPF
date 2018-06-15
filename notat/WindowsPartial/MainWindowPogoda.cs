using Newtonsoft.Json;
using notat.Models;
using notat.Models;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Net;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace notat
{
    public partial class MainWindow : Window
    {
        ObservableCollection<Contact> contactsList;
        PogodaInformacje.root pogodaInformacje;

        void getPogoda(string miasto)
        {
            using (WebClient web = new WebClient())
            {
                string url = string.Format("http://api.openweathermap.org/data/2.5/weather?q={0}&appid={1}&units=metric&cnt=6", miasto, APPID);
                try
                {
                    var json = web.DownloadString(url);
                    var wynik = JsonConvert.DeserializeObject<PogodaInformacje.root>(json);
                    pogodaInformacje = wynik;

                    tb_predkosc_wiatru.Text = string.Format("{0}" + "m/s", pogodaInformacje.wind.speed);
                    tb_zachmurzenie.Text = string.Format("{0}" + "%", pogodaInformacje.clouds.all);
                    tb_wilgotnosc.Text = string.Format("{0}" + "%", pogodaInformacje.main.humidity);
                    tb_cisnienie.Text = string.Format("{0}" + "hPa", pogodaInformacje.main.pressure);
                    tb_miasto.Text = string.Format("{0}", pogodaInformacje.name);
                    tb_temperatura.Text = string.Format("{0}\u00B0" + "C", pogodaInformacje.main.temp);
                    tb_max_temperatura.Text = string.Format("{0}\u00B0" + "C", pogodaInformacje.main.temp_max);
                    tb_min_temperatura.Text = string.Format("{0}\u00B0" + "C", pogodaInformacje.main.temp_min);
                    img_obrazek_pogody.Source = setIcon(pogodaInformacje.weather[0].icon);

                    lbl_pogoda_informacja_o_bledzie.Visibility = Visibility.Hidden;
                    pogoda.DataContext = pogodaInformacje.main;
                }
                catch
                {
                    MessageBox.Show("Nie ma takiego miasta");
                    textBox_szukaneMiasto.Clear();
                }
            }
        }


        BitmapImage setIcon(string ikonkaID)
        {
            string url = string.Format("http://openweathermap.org/img/w/{0}.png", ikonkaID);
            BitmapImage pogodaObrazek = new BitmapImage();
            pogodaObrazek.BeginInit();
            pogodaObrazek.UriSource = new System.Uri(url);
            pogodaObrazek.EndInit();
            return pogodaObrazek;
        }

        private void SzukajMiasta(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(textBox_szukaneMiasto.Text))
            {
                getPogoda(textBox_szukaneMiasto.Text);
            }
        }

    }


}

