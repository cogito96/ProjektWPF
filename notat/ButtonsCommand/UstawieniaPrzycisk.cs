using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace notat.ButtonsCommand
{
    public static class UstawieniaPrzycisk
    {
        public static RoutedUICommand ZmianaUstawien = new RoutedUICommand(" ", "Zapisz zmiany", typeof(UstawieniaPrzycisk));
        public static RoutedUICommand Zmiana_Ustawien { get { return ZmianaUstawien; } }

    }

}
