using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using VideoRental.Domain;
using VideoRental.Models;

namespace VideoRental
{
    public partial class AddEditFilmPage : Page
    {
        private const int MIN_RELEASE_YEAR = 1900;
        private readonly Film _film;
        private readonly List<FilmCredit> _actors;

        public AddEditFilmPage(Film? film = null)
        {
            InitializeComponent();

            _film = film ?? new Film();
            _actors = _film.Actors.ToList();
            DataContext = _film;
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            _film.Actors = _actors;
            GoBack();
        }

        private static void GoBack()
        {
            Manager.MainFrame.GoBack();
            Manager.MainFrame.RemoveBackEntry();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            CBoxAuthors.ItemsSource = VideoRentalDbContext.GetContext().FilmCredits.ToList();
            CBoxDirectors.ItemsSource = VideoRentalDbContext.GetContext().FilmCredits.ToList();
            CBoxGenres.ItemsSource = VideoRentalDbContext.GetContext().Genres.ToList();
        }

        private async void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            if (IsFilmValid())
            {
                if (_film.Id == 0)
                {
                    VideoRentalDbContext.GetContext().Add(_film);
                }

                try
                {
                    await VideoRentalDbContext.GetContext().SaveChangesAsync();
                    MessageBox.Show("Информация сохранена!", Title, MessageBoxButton.OK, MessageBoxImage.Information);
                    GoBack();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private bool IsFilmValid()
        {
            if (string.IsNullOrWhiteSpace(_film.Name))
            {
                MessageBox.Show("Название фильма не может быть пустым!", Title, MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            if (_film.Genre == null)
            {
                MessageBox.Show("Выберите жанр фильма из списка!", Title, MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            if (_film.Author == null)
            {
                MessageBox.Show("Выберите автора фильма из списка!", Title, MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            if (_film.Director == null)
            {
                MessageBox.Show("Выберите режиссера фильма из списка!", Title, MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            if (_film.ReleaseDate > DateTime.Now || _film.ReleaseDate.Year < MIN_RELEASE_YEAR)
            {
                MessageBox.Show("Дата выпуска не может быть в будущем или быть раньше 1900 года!", Title, MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            if (_film.Price3Days < 0)
            {
                MessageBox.Show("Цена аренды фильма не может быть отрицательной!", Title, MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            return true;
        }

        private void BtnActors_Click(object sender, RoutedEventArgs e)
        {
            var wnd = new AddActorsWindow(_film);
            wnd.ShowDialog();
        }
    }
}
