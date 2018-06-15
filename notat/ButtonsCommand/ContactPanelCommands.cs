using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace notat.ButtonsCommand
{
    public class ContactPanelCommands
    {
        static RoutedUICommand add = new RoutedUICommand("", "add", typeof(ContactPanelCommands));
        public static RoutedUICommand Add { get { return add; } }


        static RoutedUICommand edit = new RoutedUICommand("", "edit", typeof(ContactPanelCommands));
        public static RoutedUICommand Edit { get { return edit; } }


        static RoutedUICommand delete = new RoutedUICommand("", "delete", typeof(ContactPanelCommands));
        public static RoutedUICommand Delete { get { return delete; } }


        static RoutedUICommand show = new RoutedUICommand("", "show", typeof(ContactPanelCommands));
        public static RoutedUICommand Show { get { return show; } }


        static RoutedUICommand search = new RoutedUICommand("", "search", typeof(ContactPanelCommands));
        public static RoutedUICommand Search { get { return search; } }
    }
}
