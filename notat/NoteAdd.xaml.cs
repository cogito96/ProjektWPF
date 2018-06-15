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
    /// Interaction logic for NoteAdd.xaml
    /// </summary>
    public partial class NoteAdd : Window
    {
       
            public Note newNote;
            ObservableCollection<Note> list;


            public NoteAdd(ObservableCollection<Note> przekazana)
            {
                list = przekazana;
                newNote = new Note();
                InitializeComponent();
            }

            private void DodajNowaNotatke(object sender, RoutedEventArgs e)
            {
                string richtekst = new TextRange(RichtxtboxTresc.Document.ContentStart, RichtxtboxTresc.Document.ContentEnd).Text;

                newNote.title = TytulTxtbox.Text;
                newNote.desc = richtekst;

                DateTime data = DateTime.Today;

                newNote.data = data.ToString("d");

                list.Add(newNote);
                DialogResult = true;
                this.Close();
            }

            private void Button_Click(object sender, RoutedEventArgs e)
            {
                Close();
            }
        }
    }

