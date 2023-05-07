using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace VideoRental.Domain
{
    public class MainWindowViewModel : ViewModelBase
    {
        private MenuItemViewModel _selectedItem;

        public MenuItemViewModel SelectedMenuItem
        {
            get => _selectedItem;
            set => SetProperty(ref _selectedItem, value);
        }

        public ObservableCollection<MenuItemViewModel> MenuItems { get; }
        public RelayCommand HomeCommand { get; }
        public RelayCommand MovePrevCommand { get; }
        public RelayCommand MoveNextCommand { get; }
        
        public MainWindowViewModel(ObservableCollection<MenuItemViewModel> menuItems)
        {
            MenuItems = menuItems;

            HomeCommand = new RelayCommand(
            _ =>
            {
                while (NavigationManager.Frame.CanGoBack)
                {
                    NavigationManager.Frame.GoBack();
                }
            });

            MovePrevCommand = new RelayCommand(
                _ =>
                {
                    NavigationManager.Frame.GoBack();
                },
                _ => NavigationManager.Frame.CanGoBack);

            MoveNextCommand = new RelayCommand(
               _ =>
               {
                    NavigationManager.Frame.GoForward();
               },
               _ => NavigationManager.Frame.CanGoForward);
        }

        public void OpenSelectedPage()
        {
            if (SelectedMenuItem != null)
            {
                NavigationManager.Navigate(SelectedMenuItem.Page);
            }
        }
    }
}
