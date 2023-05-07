using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace VideoRental.Domain
{
    /*
        TODO:
        1. Решить проблему загрузки аутентификации самой первой
        2. Решить проблему с передачей Customer в классы
        3. Решить проблему с меню при аутентификации
     */

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
                while (Manager.MainFrame.CanGoBack)
                {
                    Manager.MainFrame.GoBack();
                }
            });

            MovePrevCommand = new RelayCommand(
                _ =>
                {
                    Manager.MainFrame.GoBack();
                },
                _ => Manager.MainFrame.CanGoBack);

            MoveNextCommand = new RelayCommand(
               _ =>
               {
                    Manager.MainFrame.GoForward();
               },
               _ => Manager.MainFrame.CanGoForward);
        }

        public void OpenSelectedPage()
        {
            if (SelectedMenuItem != null)
            {
                Manager.MainFrame.Navigate(SelectedMenuItem.Page);
            }
        }
    }
}
