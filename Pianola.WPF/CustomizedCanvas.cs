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
        // płótno ma dostosowywac swoje wymiary do wymiarów przestrzeni jaką okupują dodane elementy
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

    #region IsGridlinesVisibleProperty

    public static readonly DependencyProperty IsGridlinesVisibleProperty = DependencyProperty.Register(
        nameof(IsGridlinesVisible), typeof(bool), typeof(CustomizedCanvas),
        new FrameworkPropertyMetadata(
            default(bool),
            IsGridlinesVisiblePropertyChangedCallback));

    private static void IsGridlinesVisiblePropertyChangedCallback(
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

    public bool IsGridlinesVisible
    {
        get => (bool) GetValue(IsGridlinesVisibleProperty);
        set => SetValue(IsGridlinesVisibleProperty, value);
    }

    #endregion
    
    #region IsStaffLinesVisibleProperty

    public static readonly DependencyProperty IsStaffLinesVisibleProperty = DependencyProperty.Register(
        nameof(IsStaffLinesVisible), typeof(bool), typeof(CustomizedCanvas),
        new FrameworkPropertyMetadata(
            default(bool),
            IsStaffLinesVisiblePropertyChangedCallback));

    private static void IsStaffLinesVisiblePropertyChangedCallback(
        DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        // zmieniła się prarametr określający czy pięciolinia ma być wyświetlana?
        var customizedCanvas = (CustomizedCanvas) d;
        var newIsVisible = (bool) e.NewValue;
        if (newIsVisible)
        {
            // pięciolinia ma być wyświetlane

            // dodaj pięciolinię do plótna
            var staffLines = new StaffLines();
            customizedCanvas.Children.Add(staffLines);
            
            // szrokość pięciolinii ma dostosowywać swoją szerokość do szerokości płótna
            staffLines.SetBinding(WidthProperty, new Binding(nameof(Width)) {Source = customizedCanvas});
        }
        else
        {
            // linie mają nie być wyświetlane - usuń je
            var staffLines = customizedCanvas.Children.OfType<StaffLines>().First();
            customizedCanvas.Children.Remove(staffLines);
        }
    }

    public bool IsStaffLinesVisible
    {
        get => (bool) GetValue(IsGridlinesVisibleProperty);
        set => SetValue(IsGridlinesVisibleProperty, value);
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
            Fill = Brushes.LightGray
        };
        Children.Add(ellipse);
    }
}