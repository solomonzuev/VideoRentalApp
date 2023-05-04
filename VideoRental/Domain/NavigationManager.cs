﻿using System.Windows.Controls;

namespace VideoRental.Domain
{
    public static class NavigationManager
    {
        public static Frame? Frame { get; set; }

        public static void Navigate(Page page)
        {
            if (Frame != null && page != null)
            {
                Frame.Navigate(page);
            }
        }
    }
}