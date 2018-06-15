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
    /// Interaction logic for NoteEdit.xaml
    /// </summary>
    public partial class NoteEdit : Window
    {
        public Note newNote;
        public NoteEdit(Note selected)
        {
            InitializeComponent();
            newNote = selected;

            titleEdit.Text = newNote.title;
            descEdit.Document.Blocks.Clear();
            descEdit.Document.Blocks.Add(new Paragraph(new Run(newNote.desc)));
        }

           
           

            private void Zapisywanie(object sender, RoutedEventArgs e)
            {
                string richtekst = new TextRange(descEdit.Document.ContentStart, descEdit.Document.ContentEnd).Text;

                newNote.title = titleEdit.Text;
                newNote.desc = richtekst;

                DialogResult = true;
                this.Close();
            }

            private void AnulujBttn_Click(object sender, RoutedEventArgs e)
            {
                this.Close();
            }


        }
    }
    

