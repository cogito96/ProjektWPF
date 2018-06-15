using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace notat.ButtonsCommand
{
    public static class PrzyciskiWydarzenia
    {
        public static RoutedUICommand edytuj = new RoutedUICommand("", "edytuj", typeof(PrzyciskiWydarzenia));
        public static RoutedUICommand Edytuj { get { return edytuj; } }


        public static RoutedUICommand usun = new RoutedUICommand("", "usun", typeof(PrzyciskiWydarzenia));
        public static RoutedUICommand Usun { get { return usun; } }
    }
}