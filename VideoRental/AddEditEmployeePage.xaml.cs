using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using VideoRental.Domain;
using VideoRental.Models;

namespace VideoRental
{
    public partial class AddEditEmployeePage : Page
    {
        private const string PHONE_PATTERN = @"^\+7\d{10}$";
        private readonly Employee _employee;

        public AddEditEmployeePage(Employee? employee = null)
        {
            InitializeComponent();

            _employee = employee ?? new Employee();
            DataContext = _employee;
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            if (IsEmployeeValid())
            {
                _employee.User = GetOrCreateUser();
                _employee.Position = CBoxPositions.SelectedItem as Position;

                if (CBoxStoreLocations.SelectedItem != null)
                {
                    _employee.Store = CBoxStoreLocations.SelectedItem as StoreLocation;
                }

                if (_employee.Id == 0)
                {
                    VideoRentalDbContext.GetContext().Employees.Add(_employee);
                }

                if (_employee.User.Id == 0)
                {
                    VideoRentalDbContext.GetContext().Users.Add(_employee.User);
                }

                try
                {
                    VideoRentalDbContext.GetContext().SaveChanges();
                    MessageBox.Show("Информация сохранена!", Title, MessageBoxButton.OK, MessageBoxImage.Information);
                    GoBack();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }

            }
        }

        private User GetOrCreateUser()
        {
            var user = VideoRentalDbContext.GetContext().Users.FirstOrDefault(u =>
                    EF.Functions.Collate(u.Email, "Latin1_General_CS_AS") == TBoxEmail.Text
                    && EF.Functions.Collate(u.Password, "Latin1_General_CS_AS") == TBoxPassword.Text);

            return user ??= new User
            {
                Email = TBoxEmail.Text,
                Password = TBoxPassword.Text
            };
        }

        private bool IsEmployeeValid()
        {
            if (string.IsNullOrWhiteSpace(_employee.FullName))
            {
                MessageBox.Show("ФИО работника не может быть пустым!", Title, MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            if (_employee.Phone != null && !Regex.IsMatch(_employee.Phone, PHONE_PATTERN))
            {
                MessageBox.Show("Пожалуйста, введите номер телефона в формате +7XXXXXXXXXX, где X - любая цифра!", Title, MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            if (string.IsNullOrWhiteSpace(TBoxEmail.Text))
            {
                MessageBox.Show("Почта для входа не может быть пустой!", Title, MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            if (string.IsNullOrWhiteSpace(TBoxPassword.Text))
            {
                MessageBox.Show("Пароль не может быть пустым!", Title, MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            if (CBoxPositions.SelectedItem == null)
            {
                MessageBox.Show("Должность не может быть не указана!", Title, MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            return true;
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            GoBack();
        }

        private static void GoBack()
        {
            Manager.MainFrame.GoBack();
            Manager.MainFrame.RemoveBackEntry();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            CBoxPositions.ItemsSource = VideoRentalDbContext.GetContext().Positions.ToList();
            CBoxStoreLocations.ItemsSource = VideoRentalDbContext.GetContext().StoreLocations.ToList();
        }
    }
}
