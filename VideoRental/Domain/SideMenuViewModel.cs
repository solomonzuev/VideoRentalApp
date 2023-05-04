using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace VideoRental.Domain
{
    public class SideMenuViewModel : ViewModelBase
    {
        private MenuItemViewModel _selectedItem;

        public MenuItemViewModel SelectedMenuItem
        {
            get => _selectedItem;
            set => SetProperty(ref _selectedItem, value);
        }

        public ObservableCollection<MenuItemViewModel> MenuItems { get; }
        
        public SideMenuViewModel()
        {
            MenuItems = new()
            {
                new MenuItemViewModel("Пункт 1", typeof(FilmsPage)),
                new MenuItemViewModel("Пункт 2", null),
            };
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
