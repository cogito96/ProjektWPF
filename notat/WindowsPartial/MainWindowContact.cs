using Newtonsoft.Json;
using notat.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;

namespace notat
{
    public partial class MainWindow : Window
    {



        #region Commands Contact Panel
        private void SearchContact(object sender, ExecutedRoutedEventArgs e)
        {
            SetVisibleButtonsContactPanel("searchButton");
        }

        private void AddContact(object sender, ExecutedRoutedEventArgs e)
        {
            SetVisibleButtonsContactPanel("addButton");
        }

        private void EditContact(object sender, ExecutedRoutedEventArgs e)
        {
            SetVisibleButtonsContactPanel("editButton");
        }

        private void ShowContact(object sender, ExecutedRoutedEventArgs e)
        {
            SetVisibleButtonsContactPanel("showButton");
        }

        #endregion


        #region Enable Buttons Contact Panel
        private void DeleteContact_Set(object sender, CanExecuteRoutedEventArgs e)
        {
            if (listbox_contact.SelectedItem != null)
                e.CanExecute = true;
            else e.CanExecute = false;
        }

        private void EditContact_Set(object sender, CanExecuteRoutedEventArgs e)
        {
            if (listbox_contact.SelectedItem != null)
                e.CanExecute = true;
            else e.CanExecute = false;
        }

        private void ShowContact_Set(object sender, CanExecuteRoutedEventArgs e)
        {
            if (listbox_contact.SelectedItem != null)
                e.CanExecute = true;
            else e.CanExecute = false;
        }
        #endregion


        #region Button Click Contact Panel
        private void AddContact_Click(object sender, RoutedEventArgs e)
        {
            string firstName;
            string lastName;
            string nick;
            string email;
            int phoneNumber;
           // string login = Uzytkownik.User;

            if (DodawanieImie.Text != "")
            {
                firstName = DodawanieImie.Text;
            }
            else
            {
                MessageBox.Show("Wpisz imie!");
                return;
            }

            if (DodawanieNazwisko.Text != "")
            {
                lastName = DodawanieNazwisko.Text;
            }
            else
            {
                MessageBox.Show("Wpisz nazwisko!");
                return;
            }

            if (DodawanieKsywka.Text != "")
            {
                nick = DodawanieKsywka.Text;
            }
            else
            {
                nick = "";
            }

            if (DodawanieEmail.Text != "")
            {
                email = DodawanieEmail.Text;
            }
            else
            {
                email = "";
            }

            try
            {
                phoneNumber = Int32.Parse(DodawanieTelefon.Text);
            }
            catch
            {
                MessageBox.Show("Niepoprawny format numeru telefonu");
                return;
            }
            contactsList.Add(new Contact(firstName, lastName, nick, email, phoneNumber));
            DodawanieImie.Text = "";
            DodawanieNazwisko.Text = "";
            DodawanieKsywka.Text = "";
            DodawanieEmail.Text = "";
            DodawanieTelefon.Text = "";

            SetVisibleButtonsContactPanel("addButton");
            zapis_kontaktow(KontaktyPlik);
        }

        private void EditContact_Click(object sender, RoutedEventArgs e)
        {
            Contact do_edycji;
            do_edycji = (Contact)listbox_contact.SelectedItem;
            string imie;
            string nazwisko;
            string ksywa;
            string mail;
            int telefon;
            string login = Uzytkownik.User;
            if (EdytowanieImie.Text != "")
            {
                imie = EdytowanieImie.Text;
            }
            else
            {
                MessageBox.Show("Wpisz imie!");
                return;
            }

            if (EdytowanieNazwisko.Text != "")
            {
                nazwisko = EdytowanieNazwisko.Text;
            }
            else
            {
                MessageBox.Show("Wpisz nazwisko!");
                return;
            }

            if (EdytowanieKsywka.Text != "")
            {
                ksywa = EdytowanieKsywka.Text;
            }
            else
            {
                ksywa = "";
            }

            if (EdytowanieEmail.Text != "")
            {
                mail = EdytowanieEmail.Text;
            }
            else
            {
                mail = "";
            }
            try
            {
                telefon = Int32.Parse(EdytowanieTelefon.Text);
            }
            catch
            {
                MessageBox.Show("Niepoprawny format numeru telefonu");
                return;
            };

            do_edycji.FirstName = imie;
            do_edycji.LastName = nazwisko;
            do_edycji.Nick = ksywa;
            do_edycji.Email = mail;
            do_edycji.PhoneNumber = telefon;
            Contact zaznaczony = (Contact)listbox_contact.SelectedItem;
            listbox_contact.SelectedItem = null;
            listbox_contact.SelectedItem = zaznaczony;
            listbox_contact.Items.Refresh();

            zapis_kontaktow(KontaktyPlik);
            ClearButtons();

        }

        private ListCollectionView ContactView
        {
            get
            {
                return (ListCollectionView)CollectionViewSource.GetDefaultView(contactsList);
            }
        }

        private void SearchContact_Click(object sender, RoutedEventArgs e)
        {
            ContactView.Filter = delegate (object item)
            {
                Contact kontakt_filtrowanie = item as Contact;
                if (kontakt_filtrowanie != null)
                {
                    return (kontakt_filtrowanie.FirstName.StartsWith(WyszukiwanieImie.Text) && kontakt_filtrowanie.LastName.StartsWith(WyszukiwanieNazwisko.Text) && kontakt_filtrowanie.Nick.StartsWith(WyszukiwanieKsywka.Text));
                }
                return false;
            };
            searchContactDetails.Visibility = Visibility.Hidden;
        }

        private void DeleteContact_Click(object sender, RoutedEventArgs e)
        {
            ClearButtons();
            MessageBoxResult potwierdzenie = MessageBox.Show("Chcesz usunąć wybrany kontakt?", "Potwierdzenie", MessageBoxButton.YesNo);
            switch (potwierdzenie)
            {
                case MessageBoxResult.Yes:

                    Contact removeContactItem = ((Contact)listbox_contact.SelectedItem);
                    contactsList.Remove(removeContactItem);

                    //   zapis_kontaktow(KontaktyPlik);
                    break;
            }
        }
        #endregion


        private void SetVisibleButtonsContactPanel(string buttonName)
        {
            if (buttonName == "showButton")
            {
                if (showContactDetails.Visibility == Visibility.Hidden)
                {
                    ClearButtons();
                    showContactDetails.Visibility = Visibility.Visible;
                    //   showContactActiveButton = treu;
                }
                else
                    showContactDetails.Visibility = Visibility.Hidden;
            }

            if (buttonName == "editButton")
            {
                if (editContactDetails.Visibility == Visibility.Hidden)
                {
                    ClearButtons();
                    editContactDetails.Visibility = Visibility.Visible;
                }
                else
                    editContactDetails.Visibility = Visibility.Hidden;
            }

            if (buttonName == "addButton")
            {
                if (addContactDetails.Visibility == Visibility.Hidden)
                {
                    ClearButtons();
                    addContactDetails.Visibility = Visibility.Visible;

                }
                else
                    addContactDetails.Visibility = Visibility.Hidden;
            }

            if (buttonName == "searchButton")
            {
                if (searchContactDetails.Visibility == Visibility.Hidden)
                {
                    ClearButtons();
                    searchContactDetails.Visibility = Visibility.Visible;
                }
                else
                    searchContactDetails.Visibility = Visibility.Hidden;
            }
        }

        private void ClearButtons()
        {
            searchContactDetails.Visibility = Visibility.Hidden;
            addContactDetails.Visibility = Visibility.Hidden;
            editContactDetails.Visibility = Visibility.Hidden;
            showContactDetails.Visibility = Visibility.Hidden;
        }

        private void ClearActivityButtonsContactPanel(object sender, MouseButtonEventArgs e)
        {
            ClearButtons();
        }



        private void wczytanie_kontaktow(string sciezka)
        {
            if (File.Exists(sciezka))
            {
                System.IO.StreamReader odczyt = new System.IO.StreamReader(sciezka);
                string odczytanie = odczyt.ReadToEnd();
                contactsList = JsonConvert.DeserializeObject<ObservableCollection<Contact>>(odczytanie);
                odczyt.Close();
            }
            else
            {
                contactsList = new ObservableCollection<Contact>();
            }
        }

        private void zapis_kontaktow(string sciezka)
        {
            StreamWriter writer = new StreamWriter(sciezka);
            string zapis;
            zapis = JsonConvert.SerializeObject(contactsList);
            writer.Write(zapis);
            writer.Close();
        }
    }
}
