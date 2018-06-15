using System;
using System.Windows;
using System.Windows.Input;

namespace notat
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        const string APPID = "65501c41938c0adfeffa7abf0eb6efb1";

        String NoteFile;
        String WydarzeniaPlik;
        String KontaktyPlik;
        String ZdjeciaPlik;
        public MainWindow()
        {
            InitializeComponent();
            LogingStart();
            listbox_contact.ItemsSource = contactsList;
            listbox_picture.ItemsSource = pictureList;

            //FileRead();
            getPogoda("Bialystok");
        }


        private void FileRead()
        {
            StartUser();
            try
            {
                ZdjeciaPlik = @"zdjeciaInformacje\" + Uzytkownik.Files + ".bin";
                NoteFile = @"notatki\" + Uzytkownik.Files + ".bin";
                WydarzeniaPlik = @"wydarzenia\" + Uzytkownik.Files + ".bin";
                KontaktyPlik = @"kontakty\" + Uzytkownik.Files + ".bin"; 
                inicjalizacja_systemu_wydarzen(WydarzeniaPlik);
                wczytanie_kontaktow(KontaktyPlik);
                wczytanie_zdjec(ZdjeciaPlik);
                User(NoteFile);
            }
            catch (NullReferenceException) { Close(); }


        }
    }
}