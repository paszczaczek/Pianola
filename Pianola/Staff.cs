using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Pianola;

public class Staff : Canvas
{
    #region TypeProperty

    public static readonly DependencyProperty TypeProperty = DependencyProperty.Register(
        nameof(Type), typeof(Types), typeof(Staff),
        new FrameworkPropertyMetadata(
            Types.Treble,
            FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsRender,
            propertyChangedCallback: (d, e) =>
            {
                // zmieniono typ pięciolinii (wiolinowa/basowa) - pokaz właściwy symbol klucza
                var staff = (Staff) d;
                staff._clefSymbol.Text = e.NewValue switch
                {
                    Types.Treble => Symbol.TrebleClef,
                    Types.Bass => Symbol.BassClef,
                    _ => throw new ArgumentOutOfRangeException()
                };
            }));

    public Types Type
    {
        get => (Types) GetValue(TypeProperty);
        set => SetValue(TypeProperty, value);
    }

    public enum Types
    {
        Treble,
        Bass
    }

    #endregion

    private readonly Symbol _clefSymbol = new() {Text = Symbol.TrebleClef};
    private readonly Line[] _lines = new Line[5];

    public Staff()
    {
        // odstęp pomiędzy liniami pięciolinii
        var lineSpacing = Symbol.HeadHeight;
        
        // dodaj linie pięciolinii
        for (var i = 0; i < _lines.Length; i++)
        {
            _lines[i] = new Line
            {
                X1 = 0,
                Y1 = i * lineSpacing,
                Y2 = i * lineSpacing,
                Stroke = Brushes.Gray,
                StrokeThickness = 1,
                SnapsToDevicePixels = true
            };
            Children.Add(_lines[i]);
        }

        // dodaj klucz 
        var secondLineY = lineSpacing * 3;
        var clefTop = secondLineY - Symbol.BaseLine;
        SetTop(_clefSymbol, clefTop);
        Children.Add(_clefSymbol);

        // ustaw wysokość pięciolinii
        Height = (_lines.Length - 1) * lineSpacing;
    }

    protected override void OnRenderSizeChanged(SizeChangedInfo sizeInfo)
    {
        // ustaw długości linii w pieciolinii
        if (sizeInfo.WidthChanged)
            foreach (var line in _lines)
                line.X2 = sizeInfo.NewSize.Width;

        base.OnRenderSizeChanged(sizeInfo);
    }
}