﻿using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using VideoRental.Models;

namespace VideoRental
{
    public partial class FilmsPage : Page
    {
        public FilmsPage()
        {
            InitializeComponent();
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            DGridFilms.ItemsSource = await GetFilmsAsync();
        }

        private void BtnMoreDetails_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button { DataContext: Film selectedFilm })
            {
                var wnd = new MoreDetailsWindow(selectedFilm)
                {
                    Owner = Window.GetWindow(this)
                };
                wnd.ShowDialog();
            }
        }

        private async void BtnSearch_Click(object sender, RoutedEventArgs e)
        {
            DGridFilms.ItemsSource = await GetFilmsAsync();
        }

        private async Task<List<Film>> GetFilmsAsync()
        {
            // Выбираем все фильмы, которые доступны хотя бы на одном носителе
            var query = VideoRentalDbContext.GetContext().Films
                    .Include(f => f.FilmsInMedia)
                    .Include(f => f.Director)
                    .Include(f => f.Author)
                    .Include(f => f.Genre)
                    .Include(f => f.Actors)
                    .Where(f => f.FilmsInMedia.Any(fm => fm.IsAvailable == true));

            // Фильтрация по введённому тексту
            if (!string.IsNullOrWhiteSpace(TextToSearch.Text))
            {
                string textToSearch = TextToSearch.Text;

                query = query.Where(f => f.FilmsInMedia.Any(fm => fm.IsAvailable == true))
                    .Where(f => f.Name.Contains(textToSearch)
                        || f.Genre.Name.Contains(textToSearch)
                        || f.Author.FullName.Contains(textToSearch)
                        || f.LimitAge.ToString().Contains(textToSearch)
                        || f.Price3Days.ToString().Contains(textToSearch));
            }

            // Подключаем все необходимые сущности и возвращаем результат
            return await query.ToListAsync();
        }

        private void TextToSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                BtnSearch_Click(null, null);
            }
        }
    }
}
