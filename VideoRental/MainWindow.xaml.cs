using MaterialDesignThemes.Wpf;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using VideoRental.Domain;

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

            // Устанавливаем контекст окна
            DataContext = new MainWindowViewModel(new()
            {
                new MenuItemViewModel("Фильмы для аренды", typeof(FilmsPage)),
                new MenuItemViewModel("Арендованные фильмы", typeof(RentedFilmsPage)),
            });

            SetCustomerFullName();
        }

        private void SetCustomerFullName()
        {
            // Устанавливаем контекст для панели с ФИО и кнопкой выхода
            TBlockFullName.DataContext = Manager.CurrentCustomer;
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
