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
    public partial class StoreLocationsPage : Page
    {
        public StoreLocationsPage()
        {
            InitializeComponent();
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
            IQueryable<StoreLocation> query = VideoRentalDbContext.GetContext().StoreLocations;

            // Фильтруем сущности при необходимости
            if (!string.IsNullOrWhiteSpace(TextToSearch.Text))
            {
                var textToSearch = TextToSearch.Text;

                query = query.Where(s => s.Address.Contains(textToSearch)
                        || s.Phone.Contains(textToSearch)
                        || s.Email.Contains(textToSearch)
                        || s.OpeningTime.ToString().Contains(textToSearch)
                        || s.ClosingTime.ToString().Contains(textToSearch));
            }

            // Возвращаем результат
            DGridStoreLocations.ItemsSource = query.ToList();
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

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            var selectedItems = DGridStoreLocations.SelectedItems.Cast<StoreLocation>().ToList();

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
                            VideoRentalDbContext.GetContext().StoreLocations.RemoveRange(selectedItems);
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
                        MessageBox.Show("Некоторые из выбранных магазинов связаны с сотрудниками или товарами!",
                            "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }

                }
            }
        }

        private static bool CanAllBeDeleted(List<StoreLocation> storeLocations)
        {
            return storeLocations.All(s => HasNoDependencies(s));
        }

        private static bool HasNoDependencies(StoreLocation store)
        {
            return !store.Employees.Any() && !store.FilmsInMedia.Any();
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new AddEditStoreLocationPage());
        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button { DataContext: StoreLocation storeLocation })
            {
                Manager.MainFrame.Navigate(new AddEditStoreLocationPage(storeLocation));
            }
        }
    }
}
