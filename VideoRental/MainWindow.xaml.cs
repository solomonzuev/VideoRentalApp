﻿using MaterialDesignThemes.Wpf;
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

            LBoxMenu.DataContext = new SideMenuViewModel(new()
            {
                new MenuItemViewModel("Пункт 1", typeof(FilmsPage)),
                new MenuItemViewModel("Пункт 2", null),
            });
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DrawerHost.OpenDrawerCommand.Execute(null, null);
        }

        private void LBoxMenu_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (LBoxMenu.DataContext is SideMenuViewModel sideMenuVM)
            {
                sideMenuVM.OpenSelectedPage();
            }
        }
    }
}
