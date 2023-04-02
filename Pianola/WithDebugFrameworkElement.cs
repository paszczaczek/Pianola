using System.Globalization;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Media;

namespace Pianola
{
    /// <summary>
    /// <see cref="FrameworkElement"/> z dodanymi wizualnymi elementami debugującymi:
    /// <list type="bullet">
    /// <item><description>nazwą klasy,</description></item>
    /// <item><description>nazwą elementu (x:Name)</description></item>
    /// <item><description>ramką okalającą.</description></item>
    /// </list>
    /// </summary>
    public class WithDebugFrameworkElement : FrameworkElement
    {
        private readonly string _typeName;
        private static readonly Brush BrushColor = Brushes.Gray;
        private static readonly Point OriginOfCoordinate = new(0, 0);

        protected WithDebugFrameworkElement()
        {
            _typeName = GetType().Name;
        }

        protected override void OnRender(DrawingContext dc)
        {
            var text = $"{_typeName}: {Name}";
            var formattedText = new FormattedText(text, CultureInfo.GetCultureInfo("en-us"),
                FlowDirection.LeftToRight, new Typeface("Verdana"), 12, BrushColor,
                VisualTreeHelper.GetDpi(this).PixelsPerDip);
            dc.DrawText(formattedText, OriginOfCoordinate);

            dc.DrawRectangle(Brushes.Transparent, new Pen(BrushColor, 0.5), 
                new Rect(0, 0, ActualWidth, ActualHeight));
        }
    }
}