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
    }
}
