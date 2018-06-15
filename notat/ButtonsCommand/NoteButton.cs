using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace notat.ButtonsCommand
{
    public class NoteButton
    {
        public static RoutedUICommand edytuj = new RoutedUICommand("", "Edytuj", typeof(NoteButton));
        public static RoutedUICommand Edytuj { get { return edytuj; } }

        public static RoutedUICommand usun = new RoutedUICommand("", "Usun", typeof(NoteButton));

        public static RoutedUICommand Usun { get { return usun; } }

        public static RoutedUICommand drukuj = new RoutedUICommand("", "Drukuj", typeof(NoteButton));

        public static RoutedUICommand Drukuj { get { return drukuj; } }

    }
}
