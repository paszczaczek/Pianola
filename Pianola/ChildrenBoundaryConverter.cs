using System;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace Pianola;

public class ChildrenBoundaryConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        var children = (UIElementCollection) value;
        if (children.Count == 0) return double.NaN;
        var frameworkElements = children.OfType<FrameworkElement>().ToList();
        if (parameter == FrameworkElement.WidthProperty) return frameworkElements.Max(fe => fe.Width);
        if (parameter == FrameworkElement.HeightProperty) return frameworkElements.Max(fe => fe.Height);
        throw new ArgumentOutOfRangeException(nameof(parameter), parameter, null);
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}