using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;

namespace Pianola;

// TODO czy tego kodu nie da się zamienić na xaml?
public class CustomizedCanvas : Canvas
{
    #region IsGridLinesVisibleProperty

    public static readonly DependencyProperty IsGridLinesVisibleProperty = DependencyProperty.Register(
        nameof(IsGridLinesVisible), typeof(bool), typeof(CustomizedCanvas),
        new FrameworkPropertyMetadata(
            default(bool),
            IsGridLinesVisiblePropertyChangedCallback));

    private static void IsGridLinesVisiblePropertyChangedCallback(
        DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        // zmieniła się prarametr określający czy wyświetlać liniie pomocnicze
        var customizedCanvas = (CustomizedCanvas) d;
        var newIsVisible = (bool) e.NewValue;
        if (newIsVisible)
        {
            // linie mają być wyświetlane

            // dodaj ramkę wokół
            var border = new Border
            {
                // Width = customizedCanvas.Width,
                // Height = customizedCanvas.Height,
                BorderBrush = Brushes.BlueViolet,
                BorderThickness = new Thickness(1),
                SnapsToDevicePixels = true
            };
            customizedCanvas.Children.Add(border);
            
            SetLeft(border, 0);
            SetTop(border, 0);
            
            // zbinduj wymiary ramki z wymiarami płótna
            border.SetBinding(WidthProperty, new Binding(nameof(Width)) {Source = customizedCanvas});
            border.SetBinding(HeightProperty, new Binding(nameof(Height)) {Source = customizedCanvas});
        }
        else
        {
            // linie mają nie być wyświetlane - usuń je
            var border = customizedCanvas.Children.OfType<Border>().First();
            customizedCanvas.Children.Remove(border);
        }
    }

    public bool IsGridLinesVisible
    {
        get => (bool) GetValue(IsGridLinesVisibleProperty);
        set => SetValue(IsGridLinesVisibleProperty, value);
    }

    #endregion
}