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
using System.Windows.Shapes;
using VideoRental.Domain;
using VideoRental.Models;

namespace VideoRental
{
    /// <summary>
    /// Логика взаимодействия для MoreDetailsWindow.xaml
    /// </summary>
    public partial class MoreDetailsWindow : Window
    {
        public MoreDetailsWindow(Film selectedFilm)
        {
            InitializeComponent();
            DataContext = selectedFilm;
        }

        private void BtnRent_Click(object sender, RoutedEventArgs e)
        {
            NavigationManager.Navigate(new RentPage(DataContext as Film));
            this.Close();
        }
    }
}
