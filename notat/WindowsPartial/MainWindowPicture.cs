using Microsoft.Win32;
using Newtonsoft.Json;
using notat.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace notat
{
    public partial class MainWindow : Window
    {

        string sourcePicture;
        ObservableCollection<Picture> pictureList;


        private void AddPicture(object sender, ExecutedRoutedEventArgs e)
        {
            SetVisibleButtonsPicturePanel();
        }

        private void DeletePicture_Click(object sender, RoutedEventArgs e)
        {
            addPictureDetails.Visibility = Visibility.Hidden;
            MessageBoxResult potwierdzenie = MessageBox.Show("Chcesz usunąć wybrane zdjecie?", "Potwierdzenie", MessageBoxButton.YesNo);
            switch (potwierdzenie)
            {
                case MessageBoxResult.Yes:

                    Picture removePicture = ((Picture)listbox_picture.SelectedItem);
                    string removePictureInFolderSource = removePicture.SourcePicture;
                    pictureList.Remove(removePicture);
                    zapis_zdjec(ZdjeciaPlik);
                    DeleteDirectory(removePictureInFolderSource);
                    break;
            }
        }

        private void DeleteDirectory(string path)
        {
            if (Directory.Exists(path))
            {
                foreach (string file in Directory.GetFiles(path))
                {
                    File.Delete(file);
                }
                foreach (string directory in Directory.GetDirectories(path))
                {
                    DeleteDirectory(directory);
                }
                Directory.Delete(path);
            }
        }

        private void DeletePicture_Set(object sender, CanExecuteRoutedEventArgs e)
        {
            if (listbox_picture.SelectedItem != null)
                e.CanExecute = true;
            else e.CanExecute = false;
        }

        private void AddPictureLink_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "JPG Files (*.jpg)|*.jpg|JPEG Files (*.jpeg)|*.jpeg|PNG Files (*.png)|*.png";
            if (openFileDialog.ShowDialog() == true)
            {
                PictureLink.Content = openFileDialog.SafeFileName;

                sourcePicture = openFileDialog.FileName;
                nazwa_pliku = openFileDialog.SafeFileName;
            }
        }

        private void ClearActivityButtonsPicturePanel(object sender, MouseButtonEventArgs e)
        {
            addPictureDetails.Visibility = Visibility.Hidden;
        }
        
 



        private void AddPicture_Click(object sender, RoutedEventArgs e)
        {
            var login = Uzytkownik.User;
            var aa = System.IO.Path.GetDirectoryName(System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName) + @"\galeriaZdjec\" + login + @"\" + nazwa_pliku;
            if (!string.IsNullOrEmpty(sourcePicture))
            {
                try
                {
                    System.IO.File.Copy(sourcePicture, System.IO.Path.GetDirectoryName(System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName) + @"\galeriaZdjec\" + login + @"\" + nazwa_pliku);
                    pictureList.Add( new Picture(DescPictureTextBox.Text, sourcePicture));
                    PictureLink.Content = "";
                    DescPictureTextBox.Clear();

                    addPictureDetails.Visibility = Visibility.Hidden;
                }
                catch (System.IO.IOException)
                {
                    MessageBox.Show("Zmień nazwę zdjecia!");
                    return;
                }
                sourcePicture = System.IO.Path.GetDirectoryName(System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName) + @"\galeriaZdjec\" + login + @"\" + nazwa_pliku;
            }
            else
            {
                 MessageBox.Show("Nie wybrales zdjecia");
            }

            zapis_zdjec(ZdjeciaPlik);

        }


        private void wczytanie_zdjec(string sciezka)
        {
            if (File.Exists(sciezka))
            {   
                System.IO.StreamReader odczyt = new System.IO.StreamReader(sciezka);
                string odczytanie = odczyt.ReadToEnd();
                pictureList = JsonConvert.DeserializeObject<ObservableCollection<Picture>>(odczytanie);
                odczyt.Close();
            }
            else
            {
                pictureList = new ObservableCollection<Picture>();
            }
        }

        private void zapis_zdjec(string sciezka)
        {
            StreamWriter writer = new StreamWriter(sciezka);
            string zapis;
            zapis = JsonConvert.SerializeObject(pictureList);
            writer.Write(zapis);
            writer.Close();
        }

        private void SetVisibleButtonsPicturePanel()
        {
            if (addPictureDetails.Visibility == Visibility.Hidden)
                addPictureDetails.Visibility = Visibility.Visible;
            else
                addPictureDetails.Visibility = Visibility.Hidden;

        }
    }
}
