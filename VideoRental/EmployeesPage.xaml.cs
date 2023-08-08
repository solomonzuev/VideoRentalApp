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
using VideoRental.Domain;
using VideoRental.Models;

namespace VideoRental
{
    /// <summary>
    /// Логика взаимодействия для EmployeesPage.xaml
    /// </summary>
    public partial class EmployeesPage : Page
    {
        public EmployeesPage()
        {
            InitializeComponent();
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            var selectedItems = DGridEmployees.SelectedItems.Cast<Employee>().ToList();

            if (selectedItems.Count > 0)
            {
                var isConfirmRemoving =
                    MessageBox.Show($"Вы уверены, что хотите удалить выбранные {selectedItems.Count} элементов?",
                        "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (isConfirmRemoving == MessageBoxResult.Yes)
                {
                    try
                    {
                        VideoRentalDbContext.GetContext().Employees.RemoveRange(selectedItems);
                        VideoRentalDbContext.GetContext().SaveChanges();
                        RefreshDataGrid();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
        }

        private void Page_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if ((bool)e.NewValue)
            {
                ReloadEntries();
                RefreshDataGrid();
            }
        }

        private void ReloadEntries()
        {
            // Обновляем сущности
            var entries = VideoRentalDbContext.GetContext().ChangeTracker.Entries().ToList();

            foreach (var entry in entries)
            {
                entry.Reload();
            }
        }

        private void RefreshDataGrid()
        {
            // Формируем запрос на получение сущностей из БД
            IQueryable<Employee> query = VideoRentalDbContext.GetContext().Employees;

            // Фильтруем сущности при необходимости
            if (!string.IsNullOrWhiteSpace(TextToSearch.Text))
            {
                var textToSearch = TextToSearch.Text;

                query = query.Where(emp => emp.FullName.Contains(textToSearch)
                    || emp.Phone == null || emp.Phone.Contains(textToSearch)
                    || emp.User.Email == null || emp.User.Email.Contains(textToSearch)
                    || emp.Position.Name.Contains(textToSearch)
                    || emp.Store.Address.Contains(textToSearch));
            }

            // Возвращаем результат
            DGridEmployees.ItemsSource = query
                    .Include(emp => emp.User)
                    .Include(emp => emp.Position)
                    .Include(emp => emp.Store)
                    .ToList();
        }

        private void TextToSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                BtnSearch_Click(null, null);
            }
        }

        private void BtnSearch_Click(object sender, RoutedEventArgs e)
        {
            RefreshDataGrid();
        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button { DataContext: Employee employee })
            {
                Manager.MainFrame.Navigate(new AddEditEmployeePage(employee));
            }
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new AddEditEmployeePage());
        }
    }
}
