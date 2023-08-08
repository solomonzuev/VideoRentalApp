using Microsoft.EntityFrameworkCore;
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
using VideoRental.Domain;
using VideoRental.Models;

namespace VideoRental
{
    public partial class FilmsInMediaPage : Page
    {
        public FilmsInMediaPage()
        {
            InitializeComponent();
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new AddEditFilmInMediaPage());
        }

        // TODO - Сделать проверку перед удалением фильмов на носителях
        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            var selectedItems = DGridFilmsInMedia.SelectedItems.Cast<FilmsInMedia>().ToList();

            if (selectedItems.Count > 0)
            {
                var isConfirmRemoving =
                    MessageBox.Show($"Вы уверены, что хотите удалить выбранные {selectedItems.Count} элементов?",
                        "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (isConfirmRemoving == MessageBoxResult.Yes)
                {
                    if (CanAllBeDeleted(selectedItems))
                    {
                        try
                        {
                            VideoRentalDbContext.GetContext().FilmsInMedia.RemoveRange(selectedItems);
                            VideoRentalDbContext.GetContext().SaveChanges();
                            RefreshDataGrid();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Некоторые из выбранных товаров имеют совершенные транзакции и не могут быть удалены!", 
                            "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                    
                }
            }
        }

        private static bool CanAllBeDeleted(List<FilmsInMedia> filmsInMedia)
        {
            return !filmsInMedia.Any(fm => fm.Transactions.Any());
        }

        private void RefreshDataGrid()
        {
            // Формируем запрос на получение сущностей из БД
            IQueryable<FilmsInMedia> query = VideoRentalDbContext.GetContext().FilmsInMedia;

            // Фильтруем сущности при необходимости
            if (!string.IsNullOrWhiteSpace(TextToSearch.Text))
            {
                var textToSearch = TextToSearch.Text;

                query = query.Where(fm => fm.Film.Name.Contains(textToSearch)
                        || fm.MediaType.Name.Contains(textToSearch)
                        || fm.Store.Address.Contains(textToSearch)
                        || fm.Units.ToString().Contains(textToSearch));
            }

            // Возвращаем результат
            DGridFilmsInMedia.ItemsSource = query
                    .Include(f => f.Film)
                    .Include(f => f.Store)
                    .Include(f => f.MediaType)
                    .ToList();
        }

        private void ReloadEntries()
        {
            // Обновляем сущности
            var entries = VideoRentalDbContext.GetContext().ChangeTracker.Entries().ToList();

            foreach (var entry in entries)
            {
                entry.Reload();
            }
        }

        private void Page_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if ((bool)e.NewValue)
            {
                ReloadEntries();
                RefreshDataGrid();
            }
        }

        private void BtnSearch_Click(object sender, RoutedEventArgs e)
        {
            RefreshDataGrid();
        }

        private void TextToSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                BtnSearch_Click(null, null);
            }
        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button { DataContext: FilmsInMedia filmInMedia })
            {
                Manager.MainFrame.Navigate(new AddEditFilmInMediaPage(filmInMedia));
            }
        }
    }
}
