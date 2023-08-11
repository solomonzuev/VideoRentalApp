using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using VideoRental.Domain;
using VideoRental.Models;

namespace VideoRental
{
    public partial class AuthWindow : Window
    {
        private const string ADMIN_POSITION = "Администратор";
        private const string EMAIL_PATTERN = @"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$";
        private const string PHONE_PATTERN = @"^\+7\d{10}$";

        public AuthWindow()
        {
            InitializeComponent();
        }

        private async void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            if (IsUserValid())
            {
                var role = ComboRoles.SelectedItem.ToString();
                object? currentUser = null;

                try
                {
                    if (role == "Клиент")
                    {
                        currentUser = await AuthCustomerAsync();
                    }
                    else if (role == "Работник")
                    {
                        currentUser = await AuthEmployeeAsync();
                    }
                    else if (role == "Администратор")
                    {
                        currentUser = await AuthAdministratorAsync();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }

                // Проверяем, найден пользователь (клиент или работник или админ) или нет
                if (currentUser != null)
                {
                    Manager.CurrentUser = currentUser;

                    var mainWindow = new MainWindow();
                    mainWindow.Show();
                    Close();
                }
                else
                {
                    MessageBox.Show($"{role} с указанной почтой и паролем не найден!", Title, MessageBoxButton.OK, MessageBoxImage.Warning);
                }

            }
        }

        private async Task<Employee?> AuthAdministratorAsync()
        {
            // Сравнение строк в базе данных с учётом регистра
            var employee = await VideoRentalDbContext.GetContext().Employees
                .Include(emp => emp.Position)
                .Include(emp => emp.Store)
                .Include(emp => emp.User)
                .SingleOrDefaultAsync(emp => emp.User != null
                    && emp.Position != null
                    && emp.Position.Name == ADMIN_POSITION
                    && EF.Functions.Collate(emp.User.Email, "Latin1_General_CS_AS") == TBoxEmail.Text
                    && EF.Functions.Collate(emp.User.Password, "Latin1_General_CS_AS") == PBoxPassword.Password);

            return employee;
        }

        private async Task<Employee?> AuthEmployeeAsync()
        {
            // Сравнение строк в базе данных с учётом регистра
            var employee = await VideoRentalDbContext.GetContext().Employees
                .Include(emp => emp.Position)
                .SingleOrDefaultAsync(emp => emp.User != null
                    && emp.Position != null
                    && emp.Position.Name != ADMIN_POSITION
                    && EF.Functions.Collate(emp.User.Email, "Latin1_General_CS_AS") == TBoxEmail.Text
                    && EF.Functions.Collate(emp.User.Password, "Latin1_General_CS_AS") == PBoxPassword.Password);

            return employee;
        }

        private async Task<Customer?> AuthCustomerAsync()
        {
            // Сравнение строк в базе данных с учётом регистра
            var customer = await VideoRentalDbContext.GetContext().Customers
                .SingleOrDefaultAsync(c => c.User != null
                    && EF.Functions.Collate(c.User.Email, "Latin1_General_CS_AS") == TBoxEmail.Text
                    && EF.Functions.Collate(c.User.Password, "Latin1_General_CS_AS") == PBoxPassword.Password);

            return customer;
        }

        private bool IsUserValid()
        {
            if (ComboRoles.SelectedItem == null)
            {
                MessageBox.Show("Роль не выбрана!", Title, MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            if (!Regex.IsMatch(TBoxEmail.Text, EMAIL_PATTERN))
            {
                MessageBox.Show("Почта введена неверно!", Title, MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            if (string.IsNullOrWhiteSpace(PBoxPassword.Password))
            {
                MessageBox.Show("Пароль не может быть пустым!", Title, MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            return true;
        }

        private bool IsCustomerValid()
        {
            if (string.IsNullOrWhiteSpace(TBoxFullName.Text))
            {
                MessageBox.Show("ФИО не может быть пустым!", Title, MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            if (!Regex.IsMatch(TBoxPhone.Text, PHONE_PATTERN))
            {
                MessageBox.Show("Пожалуйста, введите номер в формате +7XXXXXXXXXX, где X - любая цифра!", Title, MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            return true;
        }

        private async void BtnRegister_Click(object sender, RoutedEventArgs e)
        {
            if (IsCustomerValid() && IsUserValid() && await IsNewUserAsync())
            {
                var customer = new Customer
                {
                    FullName = TBoxFullName.Text,
                    Phone = TBoxPhone.Text,
                    User = new User
                    {
                        Email = TBoxEmail.Text,
                        Password = PBoxPassword.Password,
                    }
                };

                VideoRentalDbContext.GetContext().Customers.Add(customer);

                try
                {
                    await VideoRentalDbContext.GetContext().SaveChangesAsync();
                    MessageBox.Show("Пользователь успешно зарегистрирован!", Title, MessageBoxButton.OK, MessageBoxImage.Information);

                    TBtnLoginRegister.IsChecked = false;
                    TBoxFullName.Text = string.Empty;
                    TBoxPhone.Text = string.Empty;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private async Task<bool> IsNewUserAsync()
        {
            if (await VideoRentalDbContext.GetContext().Users.FirstOrDefaultAsync(u => u.Email == TBoxEmail.Text) != null)
            {
                MessageBox.Show("Пользователь с такой почтой уже зарегистрирован!", Title, MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            return true;
        }


        private void TBtnLoginRegister_Checked(object sender, RoutedEventArgs e)
        {
            SPanelRegisterFields.Visibility = Visibility.Visible;
            BtnLogin.Visibility = Visibility.Collapsed;
            BtnRegister.Visibility = Visibility.Visible;
            TBlockHeader.Text = "Регистрация";
        }


        private void TBtnLoginRegister_Unchecked(object sender, RoutedEventArgs e)
        {
            SPanelRegisterFields.Visibility = Visibility.Collapsed;
            BtnLogin.Visibility = Visibility.Visible;
            BtnRegister.Visibility = Visibility.Collapsed;
            TBlockHeader.Text = "Вход";
        }

        private void ComboRoles_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ComboRoles.SelectedItem != null)
            {
                var role = ComboRoles.SelectedItem.ToString();

                if (role == "Клиент")
                {
                    TBtnLoginRegister.Visibility = Visibility.Visible;
                }
                else
                {
                    TBtnLoginRegister.Visibility = Visibility.Collapsed;
                    TBtnLoginRegister.IsChecked = false;
                }
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ComboRoles.ItemsSource = new List<string> { "Клиент", "Работник", "Администратор" };
            ComboRoles.SelectedIndex = 0;
        }
    }
}
