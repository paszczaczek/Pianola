using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Pianola;

public class Staff : Grid
{
    #region ClefTypeProperty

    public static readonly DependencyProperty ClefTypeProperty = DependencyProperty.Register(
        nameof(ClefType), typeof(Clef.Type), typeof(Staff),
        new FrameworkPropertyMetadata(
            Clef.Type.Treble,
            FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsRender,
            propertyChangedCallback: (d, e) =>
            {
                // ustawiono typ pięciolinii (wiolinowa/basowa) - dodaj do pięciolinii właściwy klucz
                var staff = (Staff) d;
                staff._stackPanel.Children.Remove(staff._clef);
                staff._clef = e.NewValue is TrebleClef ? new TrebleClef() : new BassClef();
                SetTop(staff._clef, 6);
                staff._stackPanel.Children.Add(staff._clef);
            }));

    public Clef.Type ClefType
    {
        get => (Clef.Type) GetValue(ClefTypeProperty);
        set => SetValue(ClefTypeProperty, value);
    }

    #endregion

    private Clef _clef;
    private readonly StackPanel _stackPanel;

    public Staff()
    {
        // utworz pięciolinię
        var lines = new Canvas();
        var lineSpacing = Glyph.HeadHeight;
        for (var i = 0; i < 5; i++)
        {
            var line = new Line
            {
                X1 = 0,
                Y1 = i * lineSpacing,
                X2 = 20, // TODO automatyczna długość pięciolinii
                Y2 = i * lineSpacing,
                Stroke = Brushes.Gray,
                StrokeThickness = 1,
                SnapsToDevicePixels = true
            };
            lines.Children.Add(line);
        }

        // utwórz klucz 
        _clef = new TrebleClef();
        SetTop(_clef, LineSecond);
        
        // utwórz stack panel dla elementów umieszczanych na pięciolinii
        // i dodaj do niej niego klucz, skalę i metrum a później takty
        _stackPanel = new StackPanel {Orientation = Orientation.Horizontal};
        _stackPanel.Children.Add(_clef);
        // TODO dodaj skalę i metrum
        
        // dodaj do grida pięciolinię a nad nią stack panel z elementami
        Children.Add(lines);
        Children.Add(_stackPanel);
    }

    private static void SetTop(FrameworkElement element, int position)
    {
        var top =
            element.Margin.Top
            // -Glyph.BaseLine // przesun baseline znaku w gore do poczatku ukladu wspolrzednych
            + Glyph.HeadHeight * position / 2; // przesun baseline znaku w dol we wskazywaną pozycję na pięciolinii
        element.Margin = new Thickness(0, top, 0, 0);
    }

    private const int LineSecond = 6+2;
}