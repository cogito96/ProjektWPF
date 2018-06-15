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
using System.Collections.ObjectModel;
using System.IO;


namespace notat
{
    public partial class MainWindow : Window
    {
        public ObservableCollection<Note> ListaNotatek;
        bool czySprawdzacWyszukiwanie = false;
        private string sciezka2;
        

        private ListCollectionView Widok2
        {
            get
            {
                return (ListCollectionView)CollectionViewSource.GetDefaultView(ListaNotatek);
            }
        }

        void otwieranienotatnika(string sciezka)
        {
            sciezka2 = sciezka;
            if (File.Exists(sciezka))
            {
                StreamReader odczyt = new StreamReader(sciezka);
                string odczytanie = odczyt.ReadToEnd();
                ListaNotatek = JsonConvert.DeserializeObject<ObservableCollection<Note>>(odczytanie);
                odczyt.Close();
            }
            else
            {
                ListaNotatek = new ObservableCollection<Note>();
            }
        }

        void User(string sciezka)
        {

            otwieranienotatnika(sciezka);
            WidocznaLista.ItemsSource = ListaNotatek;
            WidocznaLista.Items.Refresh();

        }

        void zamykanienotatnika(string sciezka)
        {

            StreamWriter writer = new StreamWriter(sciezka);
            string zapis;
            zapis = JsonConvert.SerializeObject(ListaNotatek);
            writer.Write(zapis);
            writer.Close();

        }



        private FlowDocument CreateFlowDocument(int index)
        {

            FlowDocument doc = new FlowDocument();


            Section sec = new Section();


            Paragraph p1 = new Paragraph();

            Bold bld = new Bold();
            bld.Inlines.Add(ListaNotatek[index].title);
            Italic italicBld = new Italic();
            italicBld.Inlines.Add(bld);
            p1.Inlines.Add(ListaNotatek[index].title);

            Paragraph p2 = new Paragraph();
            p2.Inlines.Add(ListaNotatek[index].desc);

            sec.Blocks.Add(p1);
            sec.Blocks.Add(p2);

            doc.Blocks.Add(sec);

            return doc;
        }


        private void DodajNotatke_Click(object sender, RoutedEventArgs e)
        {
            NoteAdd NowaNotatka = new NoteAdd(ListaNotatek);

            if (NowaNotatka.ShowDialog() == true)
            {

                zamykanienotatnika(NoteFile);
            }

        }

        public void Edit(object sender, ExecutedRoutedEventArgs e)
        {
            NoteEdit oknoEdycji = new NoteEdit((Note)WidocznaLista.SelectedItem);
            var tmp = WidocznaLista.SelectedItem;
            if (oknoEdycji.ShowDialog() == true)
            {
                int n = WidocznaLista.SelectedIndex;

                ListaNotatek[n].title = oknoEdycji.newNote.title;
                ListaNotatek[n].desc = oknoEdycji.newNote.desc;


                WidocznaLista.SelectedItem = WidocznaLista.Items.GetItemAt(n);

                WidocznaLista.Items.Refresh();
                WidocznaLista.SelectedItem = null;
                WidocznaLista.SelectedItem = tmp;
                zamykanienotatnika(NoteFile);
            }


        }



        private void czyEdytowac(object sender, CanExecuteRoutedEventArgs e)
        {
            if (WidocznaLista.SelectedItem != null)
                e.CanExecute = true;
            else e.CanExecute = false;
            
            
        }


        private void Usun(object sender, ExecutedRoutedEventArgs e)
        {
            int index = WidocznaLista.SelectedIndex;
            ListaNotatek.RemoveAt(index);

            zamykanienotatnika(NoteFile);
        }

        private void czyUsunac(object sender, CanExecuteRoutedEventArgs e)
        {
            if (WidocznaLista.SelectedItem != null)
                e.CanExecute = true;
            else e.CanExecute = false;
        }


        private void Drukuj(object sender, ExecutedRoutedEventArgs e)
        {
            PrintDialog pDialog = new PrintDialog();

            FlowDocument doc = CreateFlowDocument(WidocznaLista.SelectedIndex);
            doc.Name = "Notatka";



            Nullable<Boolean> drukowanie = pDialog.ShowDialog();
            if (drukowanie == true)
            {
                IDocumentPaginatorSource idpSource = doc;
                pDialog.PrintDocument(idpSource.DocumentPaginator, "Drukowanie notatki");
            }
        }

        private void czyDrukowac(object sender, CanExecuteRoutedEventArgs e)
        {
            if (WidocznaLista.SelectedItem != null)
                e.CanExecute = true;
            else e.CanExecute = false;
        }
        private void WyszukianieTxtbox_IsMouseCapturedChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            WyszukianieTxtbox.Text = "";
            czySprawdzacWyszukiwanie = true;
        }

        private void WyszukianieTxtbox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (czySprawdzacWyszukiwanie == true)
            {
                Widok2.Filter = delegate (object item)
                {
                    Note filtrowanieNotatek = item as Note;
                    if (filtrowanieNotatek != null)
                    {
                        if (WyszukiwanieTytul.IsChecked == true)
                        { /*return (filtrowanieNotatek.title.Contains(WyszukianieTxtbox.Text)); */
                            return (filtrowanieNotatek.title.Contains(WyszukianieTxtbox.Text)); }
                        else
                        {
                            if (WyszukiwanieData.IsChecked == true)
                            {
                                return (filtrowanieNotatek.data.Contains(WyszukianieTxtbox.Text));
                            }
                        }
                    }
                    return false;
                };
            }
        }

    }
}
