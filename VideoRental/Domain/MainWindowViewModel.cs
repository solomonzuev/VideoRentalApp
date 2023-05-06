using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace VideoRental.Domain
{
    public class MainWindowViewModel : ViewModelBase
    {
        private int _selectedIndex;
        private MenuItemViewModel _selectedItem;
        private bool _isFilterPanelOpen;

        public int SelectedIndex
        {
            get => _selectedIndex;
            set => SetProperty(ref _selectedIndex, value);
        }

        public MenuItemViewModel SelectedMenuItem
        {
            get => _selectedItem;
            set => SetProperty(ref _selectedItem, value);
        }

        public bool IsFilterPanelOpen
        {
            get => _isFilterPanelOpen;
            set => SetProperty(ref _isFilterPanelOpen, value);
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
                SelectedIndex = 0;
            });

            MovePrevCommand = new RelayCommand(
                _ =>
                {
                    SelectedIndex--;
                },
                _ => SelectedIndex > 0);

            MoveNextCommand = new RelayCommand(
               _ =>
               {
                   SelectedIndex++;
               },
               _ => SelectedIndex < MenuItems.Count - 1);
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
