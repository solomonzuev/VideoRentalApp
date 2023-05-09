using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using VideoRental.Domain;
using VideoRental.Models;

namespace VideoRental
{
    /// <summary>
    /// Логика взаимодействия для AllFilmsPage.xaml
    /// </summary>
    public partial class AllFilmsPage : Page
    {
        public AllFilmsPage()
        {
            InitializeComponent();
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            var selectedItems = DGridFilms.SelectedItems.Cast<Film>().ToList();

            if (selectedItems.Count > 0)
            {
                var isConfirmRemoving =
                    MessageBox.Show($"Вы уверены, что хотите удалить выбранные {selectedItems.Count} элементов?",
                        "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (isConfirmRemoving == MessageBoxResult.Yes)
                {
                    try
                    {
                        VideoRentalDbContext.GetContext().Films.RemoveRange(selectedItems);
                        VideoRentalDbContext.GetContext().SaveChanges();
                        DGridFilms.ItemsSource = RefreshDataGrid();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
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

        private List<Film> RefreshDataGrid()
        {
            // Формируем запрос на получение сущностей из БД
            IQueryable<Film> query = VideoRentalDbContext.GetContext().Films;

            // Фильтруем сущности при необходимости
            if (!string.IsNullOrWhiteSpace(TextToSearch.Text))
            {
                var textToSearch = TextToSearch.Text;

                query = query.Where(f => f.Name.Contains(textToSearch)
                        || f.Genre.Name.Contains(textToSearch)
                        || f.Author.FullName.Contains(textToSearch)
                        || f.LimitAge.ToString().Contains(textToSearch)
                        || f.Price3Days.ToString().Contains(textToSearch));
            }

            // Возвращаем результат
            return query
                    .Include(f => f.FilmsInMedia)
                    .Include(f => f.Director)
                    .Include(f => f.Author)
                    .Include(f => f.Genre)
                    .Include(f => f.Actors)
                    .ToList();
        }

        private void Page_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (Visibility == Visibility.Visible)
            {
                ReloadEntries();
                DGridFilms.ItemsSource = RefreshDataGrid();
            }
        }

        private void BtnSearch_Click(object sender, RoutedEventArgs e)
        {
            DGridFilms.ItemsSource = RefreshDataGrid();
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
            if (sender is Button { DataContext: Film film })
            {
                Manager.MainFrame.Navigate(new AddEditFilmPage(film));
            }
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new AddEditFilmPage());
        }
    }
}
