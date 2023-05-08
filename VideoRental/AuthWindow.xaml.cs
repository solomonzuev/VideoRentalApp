using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using VideoRental.Domain;
using VideoRental.Models;

namespace VideoRental
{
    /// <summary>
    /// Логика взаимодействия для AuthWindow.xaml
    /// </summary>
    public partial class AuthWindow : Window
    {
        public AuthWindow()
        {
            InitializeComponent();
        }

        private async void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            if (IsUserValid())
            {
                try
                {
                    // Сравнение строк в базе данных с учётом регистра
                    var customer = await VideoRentalDbContext.GetContext().Customers
                        .SingleOrDefaultAsync(c => c.User != null
                            && EF.Functions.Collate(c.User.Email, "Latin1_General_CS_AS") == TBoxEmail.Text
                            && EF.Functions.Collate(c.User.Password, "Latin1_General_CS_AS") == PBoxPassword.Password);


                    if (customer == null)
                    {
                        MessageBox.Show("Клиент с такой почтой и паролем не найден!", Title, MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                    else
                    {
                        Manager.CurrentCustomer = customer;

                        var mainWindow = new MainWindow();
                        mainWindow.Show();

                        Close();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private bool IsUserValid()
        {
            if (!Regex.IsMatch(TBoxEmail.Text, @"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$"))
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

            if (!Regex.IsMatch(TBoxPhone.Text, @"^\+7\d{10}$"))
            {
                MessageBox.Show("Пожалуйста, введите номер в формате +7XXXXXXXXXX, где X - любая цифра!", Title, MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            return true;
        }

        private async void BtnRegister_Click(object sender, RoutedEventArgs e)
        {
            if (IsCustomerValid() && IsUserValid() && await IsUserNotExistsAsync())
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

        private async Task<bool> IsUserNotExistsAsync()
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
    }
}
