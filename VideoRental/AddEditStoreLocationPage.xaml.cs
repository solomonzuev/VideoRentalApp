using System;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using VideoRental.Domain;
using VideoRental.Models;

namespace VideoRental
{
    public partial class AddEditStoreLocationPage : Page
    {
        private readonly StoreLocation _storeLocation;
        private const string PHONE_PATTERN = @"^(?:\+7|8)[-\s]?\(?\d{3}\)?[-\s]?\d{3}[-\s]?\d{2}[-\s]?\d{2}$";
        private const string EMAIL_PATTERN = @"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$";

        public AddEditStoreLocationPage(StoreLocation? storeLocation = null)
        {
            InitializeComponent();

            _storeLocation = storeLocation ?? new StoreLocation();
            DataContext = _storeLocation;
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            if (IsStoreLocationValid())
            {
                if (_storeLocation.Id == 0)
                {
                    VideoRentalDbContext.GetContext().Add(_storeLocation);
                }

                try
                {
                    VideoRentalDbContext.GetContext().SaveChanges();
                    MessageBox.Show("Информация сохранена!", Title, MessageBoxButton.OK, MessageBoxImage.Information);
                    Manager.MainFrame.GoBack();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private bool IsStoreLocationValid()
        {
            if (string.IsNullOrWhiteSpace(_storeLocation.Address))
            {
                MessageBox.Show("Адрес магазина должен быть указан!", Title, MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            if (!string.IsNullOrEmpty(_storeLocation.Phone) && !Regex.IsMatch(_storeLocation.Phone, PHONE_PATTERN))
            {
                MessageBox.Show("Номер телефона указан неверно!", Title, MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            if (!string.IsNullOrEmpty(_storeLocation.Email) && !Regex.IsMatch(_storeLocation.Email, EMAIL_PATTERN))
            {
                MessageBox.Show("Электронная почта указана неверно!", Title, MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            if (_storeLocation.OpeningTime >= _storeLocation.ClosingTime && _storeLocation.ClosingTime != default)
            {
                MessageBox.Show("Время работы магазина указано неверно!", Title, MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            return true;
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.GoBack();
        }
    }
}
