using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace notat.Registry
{
    /// <summary>
    /// Interaction logic for RegistryAndLogin.xaml
    /// </summary>
    public partial class RegistryAndLogin : Window
    {
        public string MyData { get; set; }

        public bool Zamkniencie = false;
        public RegistryData lel;
        public uzytkownik lolo;
        private const int GWL_STYLE = -16;
        private const int WS_SYSMENU = 0x80000;

        [DllImport("user32.dll", SetLastError = true)]
        private static extern int GetWindowLong(IntPtr hWnd, int nIndex);

        [DllImport("user32.dll")]
        private static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var hwnd = new WindowInteropHelper(this).Handle;
            SetWindowLong(hwnd, GWL_STYLE, GetWindowLong(hwnd, GWL_STYLE) & ~WS_SYSMENU);
        }

        public RegistryAndLogin()
        {
            InitializeComponent();

        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Zamkniencie = false;
            Close();
        }

        private void LogButt_Click(object sender, RoutedEventArgs e)
        {
            lel = new RegistryData();
            lolo = lel.login(LoginBox.Text, PassBox.Text);
            if (lolo.User == "Error!") MessageBox.Show("Błąd logowania", "Error");
            else
            {
                Zamkniencie = true;
                DialogResult = true;
            }
        }

        private void CreateButt_Click(object sender, RoutedEventArgs e)
        {
            if (PassAddBox.Text == PassRepBox.Text)
            {
                RegistryData nowy = new RegistryData();

                lolo = nowy.CreateLogin(LoginAddBox.Text, PassAddBox.Text);
                if (lolo.User != "Error!")
                {
                    System.IO.Directory.CreateDirectory(System.IO.Path.GetDirectoryName(System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName) + @"\galeriaZdjec\" + lolo.User);
                    Zamkniencie = true;
                    DialogResult = true;
                }
            }
            else MessageBox.Show("Hasła nie zgadzają się", "Błąd");
        }

    }
}

