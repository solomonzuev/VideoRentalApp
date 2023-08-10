using System;
using System.Windows;
using System.Windows.Controls;
using VideoRental.Domain;
using VideoRental.Models;

namespace VideoRental
{
    public partial class AddEditCelebrityPage : Page
    {
        private readonly FilmCredit _celebrity;

        public AddEditCelebrityPage(FilmCredit? celebrity = null)
        {
            InitializeComponent();

            _celebrity = celebrity ?? new FilmCredit();

            DataContext = _celebrity;
            SetCelebritySex();
        }

        private void SetCelebritySex()
        {
            if (_celebrity.Sex == "М" || string.IsNullOrWhiteSpace(_celebrity.Sex))
            {
                RBtnMale.IsChecked = true;
            }
            else
            {
                RBtnFemale.IsChecked = true;
            }
        }

        private void RBtnMale_Checked(object sender, RoutedEventArgs e)
        {
            _celebrity.Sex = "М";
        }

        private void RBtnFemale_Checked(object sender, RoutedEventArgs e)
        {
            _celebrity.Sex = "Ж";
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            if (IsCelebrityValid())
            {
                if (_celebrity.Id == 0)
                {
                    VideoRentalDbContext.GetContext().Add(_celebrity);
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

        private bool IsCelebrityValid()
        {
            if (string.IsNullOrWhiteSpace(_celebrity.FullName))
            {
                MessageBox.Show("ФИО знаменитости не может быть пустым!", Title, MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            if (string.IsNullOrWhiteSpace(_celebrity.Sex))
            {
                MessageBox.Show("Пол знаменитости не может быть не указан!", Title, MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            // Рост необязателен, но если указывается, то должен быть в корректных пределах
            if (_celebrity.Height != 0 && (_celebrity.Height < 50 || _celebrity.Height > 300))
            {
                MessageBox.Show("Рост знаменитости должен быть в пределах от 50 до 300 см!", Title, MessageBoxButton.OK, MessageBoxImage.Warning);
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
