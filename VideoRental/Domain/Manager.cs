using System;
using System.Windows.Controls;
using VideoRental.Models;

namespace VideoRental.Domain
{
    public static class Manager
    {
        public static Frame MainFrame { get; set; }
        public static Customer CurrentCustomer { get; set; }
        public static Employee CurrentEmployee { get; set; }
    }
}