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
    public partial class CelebritiesPage : Page
    {
        public CelebritiesPage()
        {
            InitializeComponent();
        }

        private void TextToSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                BtnSearch_Click(null, null);
            }
        }

        private void BtnSearch_Click(object sender, RoutedEventArgs e)
        {
            RefreshDataGrid();
        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button { DataContext: FilmCredit celebrity })
            {
                Manager.MainFrame.Navigate(new AddEditCelebrityPage(celebrity));
            }
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new AddEditCelebrityPage());
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            var selectedItems = DGridCelebrities.SelectedItems.Cast<FilmCredit>().ToList();

            if (selectedItems.Any())
            {
                var isConfirmRemoving =
                    MessageBox.Show($"Вы уверены, что хотите удалить выбранные {selectedItems.Count} элементов?",
                        "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (isConfirmRemoving == MessageBoxResult.Yes)
                {
                    if (AllCanBeDeleted(selectedItems))
                    {
                        VideoRentalDbContext.GetContext().FilmCredits.RemoveRange(selectedItems);

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
                    else
                    {
                        MessageBox.Show("Операция не может быть завершена, так как существуют фильмы, с которыми связана знаменитость!",
                            "Удаление данных", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                }
            }
        }

        private static bool AllCanBeDeleted(List<FilmCredit> celebrities)
        {
            return celebrities.All(c => HasNoDependencies(c));
        }

        private static bool HasNoDependencies(FilmCredit celebrity)
        {
            return !celebrity.Films.Any() && !celebrity.FilmAuthors.Any() && !celebrity.FilmDirectors.Any();
        }

        private void Page_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if ((bool)e.NewValue)
            {
                ReloadEntries();
                RefreshDataGrid();
            }
        }

        private void RefreshDataGrid()
        {
            // Формируем запрос на получение сущностей из БД
            IQueryable<FilmCredit> query = VideoRentalDbContext.GetContext().FilmCredits;

            // Фильтруем сущности при необходимости
            if (!string.IsNullOrWhiteSpace(TextToSearch.Text))
            {
                var textToSearch = TextToSearch.Text;

                query = query.Where(c => c.FullName.Contains(textToSearch)
                        || c.Sex.Contains(textToSearch)
                        || c.Height.ToString().Contains(textToSearch)
                        || c.Birthdate.ToString().Contains(textToSearch)
                        || c.Deathdate.ToString().Contains(textToSearch));
            }

            // Возвращаем результат
            DGridCelebrities.ItemsSource = query.ToList();
        }

        private static void ReloadEntries()
        {
            // Обновляем сущности
            var entries = VideoRentalDbContext.GetContext().ChangeTracker.Entries().ToList();

            foreach (var entry in entries)
            {
                entry.Reload();
            }
        }
    }
}
