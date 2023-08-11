using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace VideoRental.Converters
{
    public class TimeSpanToDateTimeConverter : IValueConverter
    {
        private static readonly DateTime _baseDate = new (2000, 1, 1);

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is TimeSpan timeSpan)
            {
                return _baseDate.Add(timeSpan);
            }

            return DependencyProperty.UnsetValue;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is DateTime dateTime)
            {
                return dateTime.Subtract(_baseDate);
            }

            return DependencyProperty.UnsetValue;
        }
    }
}
