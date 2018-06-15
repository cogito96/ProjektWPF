﻿using Microsoft.Win32;
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
            addPictureDetails.Visibility = Visibility.Visible;
        }

        private void DeletePicture_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult potwierdzenie = MessageBox.Show("Chcesz usunąć wybrane zdjecie?", "Potwierdzenie", MessageBoxButton.YesNo);
            switch (potwierdzenie)
            {
                case MessageBoxResult.Yes:

                    //using(Picture removePicture = ((Picture)listbox_picture.SelectedItem))
                    //{

                    //}
                    //string removePictureInFolderSource = removePicture.SourcePicture;
                    //pictureList.Remove(removePicture);
                    zapis_zdjec(ZdjeciaPlik);
                    wczytanie_zdjec(ZdjeciaPlik);
                    
                    System.IO.File.Delete(removePictureInFolderSource);
                    break;
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
            if (sourcePicture != "")
            {
                try
                {
                    System.IO.File.Copy(sourcePicture, System.IO.Path.GetDirectoryName(System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName) + @"\galeriaZdjec\" + login + @"\" + nazwa_pliku);
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
            Picture picture = new Picture();

            picture.DescPicture = DescPictureTextBox.Text;
            picture.SourcePicture = sourcePicture;
            pictureList.Add(picture);

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


    }
}