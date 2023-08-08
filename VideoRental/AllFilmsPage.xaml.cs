using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using VideoRental.Domain;
using VideoRental.Models;

namespace VideoRental
{
    public partial class AllFilmsPage : Page
    {
        public AllFilmsPage()
        {
            InitializeComponent();
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            var selectedItems = DGridFilms.SelectedItems.Cast<Film>().ToList();

            if (selectedItems.Any())
            {
                var isConfirmRemoving =
                    MessageBox.Show($"Вы уверены, что хотите удалить выбранные {selectedItems.Count} элементов?",
                        "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (isConfirmRemoving == MessageBoxResult.Yes)
                {
                    if (AllCanBeDeleted(selectedItems))
                    {
                        TryDeleteFromDatabase(selectedItems);
                    }
                    else
                    {
                        MessageBox.Show("Операция не может быть завершена, так как существуют пользователи, которые выполнили аренду фильмов!", 
                            "Удаление данных", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                }
            }
        }

        private void TryDeleteFromDatabase(IEnumerable<Film> filmsToRemove)
        {
            foreach (var film in filmsToRemove)
            {
                film.Actors.Clear();
                VideoRentalDbContext.GetContext().FilmsInMedia.RemoveRange(film.FilmsInMedia);
            }
            
            VideoRentalDbContext.GetContext().Films.RemoveRange(filmsToRemove);

            try
            {
                VideoRentalDbContext.GetContext().SaveChanges();
                RefreshDataGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private static bool AllCanBeDeleted(IEnumerable<Film> films)
        {
            return films.All(f => HasNoTransactions(f.FilmsInMedia));
        }

        private static bool HasNoTransactions(IEnumerable<FilmsInMedia> filmMedia)
        {
            return filmMedia.All(fm => !fm.Transactions.Any());
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

        private void RefreshDataGrid()
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
            DGridFilms.ItemsSource = query
                    .Include(f => f.FilmsInMedia)
                    .Include(f => f.Director)
                    .Include(f => f.Author)
                    .Include(f => f.Genre)
                    .Include(f => f.Actors)
                    .Include(f => f.Transactions)
                    .ToList();
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
