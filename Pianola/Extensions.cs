using System.Globalization;
using System.Windows;
using System.Windows.Media;

namespace Pianola;

public static class Extensions
{
    private static readonly Brush BrushColor = Brushes.Gray;
    private static readonly Point OriginOfCoordinates = new(0, 0);

    public static void AddVisualHelpers(this FrameworkElement fe, DrawingContext dc)
    {
        var text = !string.IsNullOrEmpty(fe.Name)
            ? $"{fe.GetType().Name}+{fe.Name}"
            : fe.GetType().Name;
        var formattedText = new FormattedText(
            text,
            CultureInfo.GetCultureInfo("en-us"),
            FlowDirection.LeftToRight,
            new Typeface("Verdana"),
            12,
            BrushColor,
            VisualTreeHelper.GetDpi(fe).PixelsPerDip);
        dc.DrawText(formattedText, OriginOfCoordinates);

        dc.DrawRectangle(Brushes.Transparent, new Pen(BrushColor, 0.5),
            new Rect(0, 0, fe.ActualWidth, fe.ActualHeight));
    }

    public static T FindVisualParent<T>(this DependencyObject childObject)
        where T : DependencyObject
    {
        if (childObject == null) return null;
        var parentObj = VisualTreeHelper.GetParent(childObject);
        return parentObj switch
        {
            null => null,
            T parent => parent,
            _ => FindVisualParent<T>(parentObj)
        };
    }
}