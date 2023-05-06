using System;
using System.Collections;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
using VideoRental.Models;

namespace VideoRental
{
    /// <summary>
    /// Логика взаимодействия для FilmsPage.xaml
    /// </summary>
    public partial class FilmsPage : Page
    {
        public FilmsPage()
        {
            InitializeComponent();
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            DGridFilms.ItemsSource = await VideoRentalDbContext.GetContext().Films
                .Where(f => f.FilmsInMedia.Any(fm => fm.IsAvaliable == true))
                .Include(f => f.Author)
                .Include(f => f.Genre)
                .ToListAsync();
        }

        private void BtnMoreDetails_Click(object sender, RoutedEventArgs e)
        {
        //    if (sender is Button { DataContext: Video video })
        //    {
        //        // Создать новое окно MoreDetailsWindow
        //    }
        }

        private async void BtnSearch_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TextToSearch.Text))
            {
                DGridFilms.ItemsSource = await VideoRentalDbContext.GetContext().Films
                    .Where(f => f.FilmsInMedia.Any(fm => fm.IsAvaliable == true))
                    .Include(f => f.Author)
                    .Include(f => f.Genre)
                    .ToListAsync();
            }
            else
            {
                string textToSearch = TextToSearch.Text.ToUpper();

                DGridFilms.ItemsSource = await VideoRentalDbContext.GetContext().Films
                    .Where(f => f.FilmsInMedia.Any(fm => fm.IsAvaliable == true))
                    .Where(f => f.Name.ToUpper().Contains(textToSearch)
                        || f.Genre.Name.ToUpper().Contains(textToSearch)
                        || f.Author.FullName.ToUpper().Contains(textToSearch)
                        || f.LimitAge.ToString().Contains(textToSearch)
                        || f.Price3Days.ToString().Contains(textToSearch))
                    .ToListAsync();
            }
        }

        private void TextToSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                BtnSearch_Click(null, null);
            }
        }
    }
}
