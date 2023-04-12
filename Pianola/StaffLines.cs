using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Pianola;

public class StaffLines : Canvas
{
    #region IsHelperLinesVisibleProperty

    public static readonly DependencyProperty IsGuidLinesVisibleProperty = DependencyProperty.Register(
        nameof(IsGuidLinesVisible), typeof(bool), typeof(StaffLines),
        new FrameworkPropertyMetadata(
            default(bool),
            (d, e) =>
            {
                // zmieniła się prarametr określający czy wyświetlać linie pomocnicze
                var staffLines = (StaffLines) d;
                var newIsVisible = (bool) e.NewValue;
                if (newIsVisible)
                {
                    // linie mają być wyświetlane
                    // dodaj ramkę wokół pięciolinii
                    var border = new Border
                    {
                        Width = staffLines.Width,
                        Height = staffLines.Height,
                        BorderBrush = Brushes.BlueViolet,
                        BorderThickness = new Thickness(1),
                        SnapsToDevicePixels = true
                    };
                    staffLines.Children.Add(border);
                }
                else
                {
                    // linie mają nie być wyświetlane - usuń je
                    foreach (Line line in staffLines.Lines)
                        staffLines.Lines.Remove(line);
                }
            }));


    public bool IsGuidLinesVisible
    {
        get => (bool) GetValue(IsGuidLinesVisibleProperty);
        set => SetValue(IsGuidLinesVisibleProperty, value);
    }

    #endregion

    
    public StaffLines()
    {
        // utworz linie pięciolinii
        var lineSpacing = Sign.HeadHeight;
        const int lineCount = 5;
        
        for (var i = 0; i < lineCount; i++)
        {
            var line = new Line
            {
                X1 = 0,
                Y1 = i * lineSpacing,
                X2 = 10, // prawdziwą wartość ustawi OnRenderSizeChanged
                Y2 = i * lineSpacing,
                Stroke = Brushes.Gray,
                StrokeThickness = 1,
                SnapsToDevicePixels = true
            };
            Children.Add(line);
        }

        Height = lineSpacing * (lineCount - 1);
    }

    private UIElementCollection Lines => Children;

    
    protected override void OnRenderSizeChanged(SizeChangedInfo sizeInfo)
    {
        // zsynchronizuj długość linii pięciolinii z długością elementu
        foreach (Line line in Lines)
            line.X2 = Width;
        base.OnRenderSizeChanged(sizeInfo);
    }
}