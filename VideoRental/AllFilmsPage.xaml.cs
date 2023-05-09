using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using VideoRental.Models;

namespace VideoRental
{
    /// <summary>
    /// Логика взаимодействия для AllFilmsPage.xaml
    /// </summary>
    public partial class AllFilmsPage : Page
    {
        public AllFilmsPage()
        {
            InitializeComponent();
        }

        private async void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            var selectedItems = DGridFilms.SelectedItems.Cast<Film>().ToList();

            if (selectedItems.Count > 0 )
            {
                var isConfirmRemoving = 
                    MessageBox.Show("Вы уверены, что хотите удалить выбранные {selectedItems.Count} элементов?", 
                        "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Question);
            
                if (isConfirmRemoving == MessageBoxResult.Yes)
                {
                    try
                    {
                        VideoRentalDbContext.GetContext().Films.RemoveRange(selectedItems);
                        await VideoRentalDbContext.GetContext().SaveChangesAsync();
                        DGridFilms.ItemsSource = await RefreshDataGridAsync();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
        }

        private async Task ReloadEntriesAsync()
        {
            // Обновляем сущности
            foreach (var entry in VideoRentalDbContext.GetContext().ChangeTracker.Entries())
            {
                await entry.ReloadAsync();
            }
        }

        private async Task<List<Film>> RefreshDataGridAsync()
        {
            // Формируем запрос на получение сущностей из БД
            IQueryable<Film> query = VideoRentalDbContext.GetContext().Films
                    .Include(f => f.FilmsInMedia)
                    .Include(f => f.Director)
                    .Include(f => f.Author)
                    .Include(f => f.Genre)
                    .Include(f => f.Actors);

            // Фильтруем сущности при необходимости
            if (!string.IsNullOrWhiteSpace(TextToSearch.Text))
            {
                var textToSearch = TextToSearch.Text;

                query = query.Where(f => f.Name.Contains(textToSearch)
                        || f.Genre.Name.Contains(textToSearch)
                        || f.Author.FullName.Contains(textToSearch)
                        || f.LimitAge.ToString().Contains(textToSearch)
                        || f.Price3Days.ToString().Contains(textToSearch));
            }

            // Возвращаем результат
            return await query.ToListAsync();
        }

        private async void Page_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (Visibility == Visibility.Visible)
            {
                await ReloadEntriesAsync();
                DGridFilms.ItemsSource = await RefreshDataGridAsync();
            }
        }

        private async void BtnSearch_Click(object sender, RoutedEventArgs e)
        {
            DGridFilms.ItemsSource = await RefreshDataGridAsync();
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
