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
    public partial class RentedFilmsPage : Page
    {
        private readonly Customer _customer;

        public RentedFilmsPage(Customer customer)
        {
            InitializeComponent();
            _customer = customer;
        }

        public RentedFilmsPage()
        {
            InitializeComponent();
            _customer = Manager.CurrentUser as Customer;
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            DGridTransactions.ItemsSource = await GetTransactionsAsync();

            if (Manager.CurrentUser is Employee)
            {
                DGColumnStatus.Visibility = Visibility.Visible;
                BtnCancel.Visibility = Visibility.Visible;
            }
        }

        private async void BtnSearch_Click(object sender, RoutedEventArgs e)
        {
            DGridTransactions.ItemsSource = await GetTransactionsAsync();
        }

        private async Task<List<Transaction>> GetTransactionsAsync()
        {
            var query = VideoRentalDbContext.GetContext().Transactions
                .Include(t => t.Film)
                .Include(t => t.VideosInMedia)
                .Include(t => t.VideosInMedia.MediaType)
                .Include(t => t.VideosInMedia.Store)
                .Where(t => t.Customer == _customer);

            // Фильтрация по введённому тексту
            if (!string.IsNullOrWhiteSpace(TextToSearch.Text))
            {
                string textToSearch = TextToSearch.Text;

                query = query.Where(t => t.Film.Name.Contains(textToSearch)
                    || t.StartDate.ToString().Contains(textToSearch)
                    || t.EndDate.ToString().Contains(textToSearch)
                    || t.VideosInMedia.MediaType.Name.Contains(textToSearch)
                    || t.VideosInMedia.Store.Address.Contains(textToSearch));
            }

            // Подключаем все необходимые сущности и возвращаем результат
            return await query.ToListAsync();
        }

        private void TextToSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                BtnSearch_Click(null, null);
            }
        }

        private void BtnChangeStatus_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button { DataContext: Transaction transaction })
            {
                // Пока клиент не вернул фильм или пока дата окончания аренды меньше, чем текущая
                if (transaction.IsIssuied || transaction.EndDate >= DateTime.Now)
                {
                    transaction.IsIssuied = !transaction.IsIssuied;

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

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.GoBack();
        }
    }
}
