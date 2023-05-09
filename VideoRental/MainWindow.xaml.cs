using MaterialDesignThemes.Wpf;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using VideoRental.Domain;
using VideoRental.Models;

namespace VideoRental
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame = MainFrame;

            SetDataContext();
            SetCustomerFullName();
        }

        private void SetDataContext()
        {
            // Устанавливаем контекст для всей страницы
            if (Manager.CurrentUser is Customer)
            {
                DataContext = new MainWindowViewModel(new()
                {
                    new MenuItemViewModel("Фильмы для аренды", typeof(FilmsPage)),
                    new MenuItemViewModel("Арендованные фильмы", typeof(RentedFilmsPage)),
                });
            }
            else if (Manager.CurrentUser is Employee)
            {
                DataContext = new MainWindowViewModel(new()
                {
                    new MenuItemViewModel("Фильмы", typeof(AllFilmsPage)),
                    new MenuItemViewModel("Клиенты", typeof(CustomersPage)),
                });
            }
        }

        private void SetCustomerFullName()
        {
            // Устанавливаем контекст для панели с ФИО и кнопкой выхода
            if (Manager.CurrentUser is Customer customer)
            {
                TBlockFullName.DataContext = customer;
            }
            else if (Manager.CurrentUser is Employee employee)
            {
                TBlockFullName.DataContext = employee;
            }
        }

        private void LBoxMenu_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (LBoxMenu.DataContext is MainWindowViewModel sideMenuVM)
            {
                sideMenuVM.OpenSelectedPage();
            }
        }

        private void MenuToggleButton_OnClick(object sender, RoutedEventArgs e)
        {
            DrawerHost.OpenDrawerCommand.Execute(null, null);
            MenuToggleButton.IsChecked = false;
        }

        private void BtnLogout_Click(object sender, RoutedEventArgs e)
        {
            var authWindow = new AuthWindow();
            authWindow.Show();
            Close();
        }
    }
}
