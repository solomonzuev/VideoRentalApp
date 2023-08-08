using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using VideoRental.Domain;
using VideoRental.Models;

namespace VideoRental
{
    /// <summary>
    /// Логика взаимодействия для AddEditFilmsInMediaPage.xaml
    /// </summary>
    public partial class AddEditFilmInMediaPage : Page
    {
        private readonly FilmsInMedia _filmInMedia;

        public AddEditFilmInMediaPage(FilmsInMedia? filmInMedia = null)
        {
            InitializeComponent();

            _filmInMedia = filmInMedia ?? new FilmsInMedia();
            DataContext = _filmInMedia;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            CBoxFilms.ItemsSource = VideoRentalDbContext.GetContext().Films.ToList();
            CBoxMediaTypes.ItemsSource = VideoRentalDbContext.GetContext().MediaTypes.ToList();
            CBoxStoreLocations.ItemsSource = VideoRentalDbContext.GetContext().StoreLocations.ToList();
        }

        // TODO - Проверить все места, где могут вноситься повторяющиеся данные в базу данных
        private void BtnSave_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            _filmInMedia.Film = CBoxFilms.SelectedItem as Film;
            _filmInMedia.Store = CBoxStoreLocations.SelectedItem as StoreLocation;
            _filmInMedia.MediaType = CBoxMediaTypes.SelectedItem as MediaType;

            if (IsFilmInMediaValid())
            {
                if (_filmInMedia.Id == 0)
                {
                    VideoRentalDbContext.GetContext().Add(_filmInMedia);
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

        private static void GoBack()
        {
            Manager.MainFrame.GoBack();
            Manager.MainFrame.RemoveBackEntry();
        }

        private bool IsFilmInMediaValid()
        {
            if (CBoxFilms.SelectedItem == null)
            {
                MessageBox.Show("Выберите фильм из списка!", Title, MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            if (CBoxMediaTypes.SelectedItem == null)
            {
                MessageBox.Show("Выберите носитель фильма из списка!", Title, MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            if (CBoxStoreLocations.SelectedItem == null)
            {
                MessageBox.Show("Выберите локацию магазина из списка!", Title, MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            if (_filmInMedia.Units < 0)
            {
                MessageBox.Show("Введите верное количество копий фильма на носителе!", Title, MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            if (_filmInMedia.IsAvailable == null)
            {
                MessageBox.Show("Необходимо выбрать, доступен ли товар для аренды или нет!", Title, MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            if (_filmInMedia.Units == 0 && _filmInMedia.IsAvailable == true)
            {
                MessageBox.Show("Товар не может быть доступен, если количество копий равно 0!", Title, MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            if (CheckIfDuplicate())
            {
                MessageBox.Show("Товар с выбранным названием уже выпущен на выбранном носителе!", Title, MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            return true;
        }

        private bool CheckIfDuplicate()
        {
            return VideoRentalDbContext.GetContext().FilmsInMedia
                .Any(fm => fm.Id != _filmInMedia.Id 
                    && fm.Film == _filmInMedia.Film 
                    && fm.MediaType == _filmInMedia.MediaType);
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            GoBack();
        }
    }
}
