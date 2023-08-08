using Microsoft.EntityFrameworkCore;
using System;
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
    /// <summary>
    /// Логика взаимодействия для CustomersPage.xaml
    /// </summary>
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
            if (sender is Button { DataContext : Customer customer })
            {
                Manager.MainFrame.Navigate(new RentedFilmsPage(customer));
            }
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
