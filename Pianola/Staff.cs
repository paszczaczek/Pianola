using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using Pianola.Clefs;

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
                // zmieniono typ pięciolinii (wiolinowa/basowa) - pokaż właściwy symbol klucza
                var staff = (Staff) d;
                // staff._clef.Text = e.NewValue switch
                // {
                //     Types.Treble => Symbol.TrebleClef,
                //     Types.Bass => Symbol.BassClef,
                //     _ => throw new ArgumentOutOfRangeException()
                // };
                staff._clef = e.NewValue switch
                {
                    Types.Treble => new TrebleClef(),
                    Types.Bass => new BassClef(),
                    _ => throw new ArgumentOutOfRangeException()
                };
                staff.UpdateLayout();
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

    private Clef _clef = new TrebleClef();
    private readonly Line[] _lines = new Line[5];

    public Staff()
    {
        // odstęp pomiędzy liniami pięciolinii
        var lineSpacing = Glyph.HeadHeight;
        
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
        var clefTop = secondLineY - Glyph.BaseLine;
        SetTop(_clef, clefTop);
        Children.Add(_clef);

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