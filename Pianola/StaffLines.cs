using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Pianola;

public class StaffLines : Canvas
{
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

    protected override void OnRenderSizeChanged(SizeChangedInfo sizeInfo)
    {
        // zsynchronizuj długość linii pięciolinii z długością elementu
        foreach (Line line in Children)
            line.X2 = Width;
        base.OnRenderSizeChanged(sizeInfo);
    }
}