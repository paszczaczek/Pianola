using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Pianola;

public class CustomizedCanvas : Canvas
{
    protected CustomizedCanvas()
    {
        // płótno ma dostosowywac swoje wymiary do wymiarów przestrzeni jaką okupują dodaneelementy
        SetBinding(WidthProperty, new Binding()
        {
            Source = Children,
            Converter = new ChildrenBoundaryConverter(),
            ConverterParameter = WidthProperty
        });
        SetBinding(HeightProperty, new Binding()
        {
            Source = Children,
            Converter = new ChildrenBoundaryConverter(),
            ConverterParameter = HeightProperty
        });
    }

    protected override void OnVisualChildrenChanged(DependencyObject visualAdded, DependencyObject visualRemoved)
    {
        base.OnVisualChildrenChanged(visualAdded, visualRemoved);
        
        // po dodaniu elementu do płótna wymuś uaktualnienie wymiarów płótna
        GetBindingExpression(WidthProperty)?.UpdateTarget();
        GetBindingExpression(HeightProperty)?.UpdateTarget();
    }

    #region IsGridLinesVisibleProperty

    public static readonly DependencyProperty IsGridLinesVisibleProperty = DependencyProperty.Register(
        nameof(IsGridLinesVisible), typeof(bool), typeof(CustomizedCanvas),
        new FrameworkPropertyMetadata(
            default(bool),
            IsGridLinesVisiblePropertyChangedCallback));

    private static void IsGridLinesVisiblePropertyChangedCallback(
        DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        // zmieniła się prarametr określający - czy linie pomocnicze mają być wyświetlana?
        var customizedCanvas = (CustomizedCanvas) d;
        var newIsVisible = (bool) e.NewValue;
        if (newIsVisible)
        {
            // linie mają być wyświetlane

            // dodaj ramkę wokół plótna
            var border = new Border
            {
                BorderBrush = Brushes.BlueViolet,
                BorderThickness = new Thickness(1),
                SnapsToDevicePixels = true
            };
            customizedCanvas.Children.Add(border);

            // ramka ma dostosowywać swoje wymiary do wymiarów płótna
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

public class TestCustomizedCanvas : CustomizedCanvas
{
    public TestCustomizedCanvas()
    {
        var ellipse = new Ellipse
        {
            Width = 50,
            Height = 50,
            Fill = Brushes.Gray
        };
        Children.Add(ellipse);
    }
}