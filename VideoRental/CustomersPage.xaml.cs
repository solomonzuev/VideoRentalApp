using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using VideoRental.Domain;
using VideoRental.Models;

namespace VideoRental
{
    public partial class CustomersPage : Page
    {
        public CustomersPage()
        {
            InitializeComponent();
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

        private void Page_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if ((bool)e.NewValue)
            {
                ReloadEntries();
                RefreshDataGrid();
            }
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

        private void RefreshDataGrid()
        {
            // Формируем запрос на получение сущностей из БД
            IQueryable<Customer> query = VideoRentalDbContext.GetContext().Customers;

            // Фильтруем сущности при необходимости
            if (!string.IsNullOrWhiteSpace(TextToSearch.Text))
            {
                var textToSearch = TextToSearch.Text;

                query = query.Where(c => c.FullName.Contains(textToSearch)
                    || c.Phone == null || c.Phone.Contains(textToSearch)
                    || c.User == null || c.User.Email.Contains(textToSearch));
            }

            // Обновляем данные в DataGrid
            DGridCustomers.ItemsSource = query
                .Include(c => c.User)
                .ToList();
        }

        private void BtnShowRented_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button { DataContext: Customer customer })
            {
                Manager.MainFrame.Navigate(new RentedFilmsPage(customer, GetEmployeeStoreLocation()));
            }
        }

        private static StoreLocation? GetEmployeeStoreLocation()
        {
            var emp = Manager.CurrentUser as Employee;
            var store = VideoRentalDbContext.GetContext().StoreLocations.FirstOrDefault(s => s.Employees.Any(e => e.Id == emp.Id));
            return store;
        }

        private void BtnChangeBlackList_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button { DataContext: Customer customer })
            {
                customer.InBlackList = !customer.InBlackList;

                try
                {
                    VideoRentalDbContext.GetContext().SaveChanges();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
    }
}
