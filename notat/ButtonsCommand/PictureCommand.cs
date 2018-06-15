using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace notat.ButtonsCommand
{
    class PictureCommand
    {
        public static RoutedUICommand add = new RoutedUICommand(" ", "dodaj", typeof(ButtonCommand));
        public static RoutedUICommand Add { get { return add; } }

        public static RoutedUICommand delete = new RoutedUICommand(" ", "Dodaj", typeof(ButtonCommand));
        public static RoutedUICommand Delete { get { return delete; } }

    }
}
