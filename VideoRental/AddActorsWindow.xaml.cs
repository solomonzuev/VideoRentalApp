using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using VideoRental.Models;

namespace VideoRental
{
    /// <summary>
    /// Логика взаимодействия для AddActorsWindow.xaml
    /// </summary>
    public partial class AddActorsWindow : Window
    {
        private readonly Film _film;

        public AddActorsWindow(Film film)
        {
            InitializeComponent();

            _film = film;
            DataContext = _film;
            CBoxFilmCredits.ItemsSource = VideoRentalDbContext.GetContext().FilmCredits
                .Where(fc => !_film.Actors.Any(a => a == fc)).ToList();
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            if (CBoxFilmCredits.SelectedItem == null)
            {
                MessageBox.Show("Актёр не выбран! Выберите актёра из списка, которого необходимо добавить!", Title, MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else
            {
                if (CBoxFilmCredits.SelectedItem is FilmCredit actor)
                {
                    _film.Actors.Add(actor);
                    RemoveActorFromComboBox(actor);
                    RefreshActorsListBox();
                }
            }
        }

        private void RefreshActorsListBox()
        {
            LBoxActors.ItemsSource = null;
            LBoxActors.ItemsSource = _film.Actors;
        }

        private void RemoveActorFromComboBox(FilmCredit actor)
        {
            var filmCredits = (List<FilmCredit>)CBoxFilmCredits.ItemsSource;
            filmCredits.Remove(actor);
            CBoxFilmCredits.ItemsSource = null;
            CBoxFilmCredits.ItemsSource = filmCredits;
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            var selectedItems = LBoxActors.SelectedItems.Cast<FilmCredit>().ToList();

            if (selectedItems.Count > 0)
            {
                var isConfirm = MessageBox.Show($"Вы уверены, что хотите удалить выбранные {selectedItems.Count} элементов?", "Внимание",
                    MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (isConfirm == MessageBoxResult.Yes)
                {
                    AddActorsToComboBox(selectedItems);

                    foreach (var item in selectedItems)
                    {
                        _film.Actors.Remove(item);
                    }
                    RefreshActorsListBox();
                }
            }
        }

        private void AddActorsToComboBox(List<FilmCredit> selectedItems)
        {
            var filmCredits = (List<FilmCredit>)CBoxFilmCredits.ItemsSource;
            filmCredits.AddRange(selectedItems);
            CBoxFilmCredits.ItemsSource = null;
            CBoxFilmCredits.ItemsSource = filmCredits;
        }
    }
}
