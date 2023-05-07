using MaterialDesignThemes.Wpf;
using System.Windows;
using System.Windows.Controls;
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

            var firstMenuItem = new MenuItemViewModel("Фильмы для аренды", typeof(FilmsPage));
            var secondMenuItem = new MenuItemViewModel("Арендованные фильмы", null);

            var mainWindowViewModel = new MainWindowViewModel(new()
            {
               firstMenuItem, secondMenuItem
            });

            mainWindowViewModel.SelectedMenuItem = firstMenuItem;

            DataContext = mainWindowViewModel;
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
    }
}
