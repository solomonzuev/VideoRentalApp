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
    /// Логика взаимодействия для RentPage.xaml
    /// </summary>
    public partial class RentPage : Page
    {
        private const int RENT_LENGTH_DAYS = 3;
        private readonly Transaction _transaction;
        public RentPage(Film selectedFilm)
        {
            InitializeComponent();

            _transaction = new Transaction
            {
                Customer = Manager.CurrentCustomer,
                Film = selectedFilm,
            };

            DataContext = _transaction;
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            CBoxStoreLocations.ItemsSource = await VideoRentalDbContext.GetContext().FilmsInMedia
                .Include(fm => fm.MediaType)
                .Include(fm => fm.Film)
                .Include(fm => fm.Store)
                .Where(fm => fm.Film == _transaction.Film && fm.IsAvaliable == true)
                .Select(fm => fm.Store)
                .Distinct()
                .ToListAsync();
        }

        private void TBoxRentDays_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!int.TryParse(TBoxRentDays.Text, out _))
            {
                TBoxRentDays.Text = "0";
            }

            // Выссчитываем дату аренды
            var startDate = DateTime.Now;
            var endDate = startDate.AddDays(RENT_LENGTH_DAYS * _transaction.RentCount);

            // Обновляем своства сделки
            _transaction.TotalPrice = _transaction.Film.Price3Days * _transaction.RentCount;
            _transaction.StartDate = startDate;
            _transaction.EndDate = endDate;

            // Выводим дату аренды
            TBlockRentPeriod.Text = $"с {startDate:dd.MM.yyyy} по {endDate:dd.MM.yyyy}";
        }

        private async void CBoxStoreLocations_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CBoxStoreLocations.SelectedItem != null)
            {
                GridExtraParams.Visibility = Visibility.Visible;

                // Получаем все носители, на которых доступен данный фильм по данному адресу
                var selectedStore = CBoxStoreLocations.SelectedItem as StoreLocation;

                CBoxMediaTypes.ItemsSource = await VideoRentalDbContext.GetContext().FilmsInMedia
                    .Include(fm => fm.MediaType)
                    .Where(fm => fm.IsAvaliable == true 
                        && fm.Film == _transaction.Film 
                        && fm.Store.Address == selectedStore.Address)
                    .Select(fm => fm.MediaType)
                    .Distinct()
                    .ToListAsync();
            }
            else
            {
                GridExtraParams.Visibility = Visibility.Hidden;
            }
        }

        private async void BtnRent_Click(object sender, RoutedEventArgs e)
        {
            if (IsTransactionValid())
            {
                var selectedMediaType = CBoxMediaTypes.SelectedItem as MediaType;
                var selectedStore = CBoxStoreLocations.SelectedItem as StoreLocation;

                // Пролучаем фильм на выбранном носителе по выбранному адресу
                _transaction.VideosInMedia = _transaction.Film.FilmsInMedia
                    .First(fm => fm.IsAvaliable == true
                        && fm.MediaType.Name == selectedMediaType.Name
                        && fm.Store.Address == selectedStore.Address);
                try
                {
                    VideoRentalDbContext.GetContext().Transactions.Add(_transaction);
                    await VideoRentalDbContext.GetContext().SaveChangesAsync();

                    MessageBox.Show("Ваша заявка на аренду успешно сформирована!",
                        Title, MessageBoxButton.OK, MessageBoxImage.Information);

                    Manager.MainFrame.GoBack();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private bool IsTransactionValid()
        {
            if (CBoxStoreLocations.SelectedItem == null)
            {
                MessageBox.Show("Не выбран пункт для получения фильма!", Title, MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            if (CBoxMediaTypes.SelectedItem == null)
            {
                MessageBox.Show("Не выбран носитель для фильма!", Title, MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            if (_transaction.RentCount <= 0)
            {
                MessageBox.Show("Количество дней аренды не может быть меньше 1!", Title, MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            if (_transaction.Film == null)
            {
                MessageBox.Show("Фильм для аренды не найден!", Title, MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            if (_transaction.Customer == null)
            {
                MessageBox.Show("Клиент, выполняющий аренду фильма не найден!", Title, MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            return true;
        }

        private void TBoxRentDays_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TBoxRentDays.Text))
            {
                // Ставим 0, если текст пустой
                e.Handled = true;
                TBoxRentDays.Text = "0";
            }

            if (!char.IsDigit(e.Text[0]))
            {
                // Отменяем ввод символа, если это не цифра
                e.Handled = true;
            }
        }
    }
}
