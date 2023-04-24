using System.Globalization;
using System.Windows.Data;
using System.Windows;
using System;

namespace RxUIToolbox.Converters;

public class PositionConverter : IMultiValueConverter
{
    public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
    {
        try
        {
            var value = (double)values[0];
            var element = values[1] as FrameworkElement;
            var factor = (parameter?.ToString() == "X") ? element?.ActualWidth : element?.ActualHeight;
            return (value * factor)!;
        }
        catch (Exception)
        {
            return 0.0;
        }
    }

    public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
