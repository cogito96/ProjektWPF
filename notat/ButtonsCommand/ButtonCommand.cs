using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace notat.ButtonsCommand
{
    public static class ButtonCommand
    {

        public static RoutedUICommand logowanie = new RoutedUICommand(" ", "Loguj", typeof(ButtonCommand));
        public static RoutedUICommand Logowanie { get { return logowanie; } }

        public static RoutedUICommand dodawanieKonta = new RoutedUICommand(" ", "Dodaj", typeof(ButtonCommand));
        public static RoutedUICommand DodawanieKonta { get { return dodawanieKonta; } }

        public static RoutedUICommand wyjscie = new RoutedUICommand(" ", "Wyjście", typeof(ButtonCommand));
        public static RoutedUICommand Wyjscie { get { return wyjscie; } }

    }
}
