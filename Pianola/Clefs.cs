using System;
using System.Windows;
using System.Windows.Controls;

namespace Pianola;

/// <remarks>
/// Struktura elementu:
/// <code>
///     Canvas
///         Glyph
/// </code>
/// </remarks>
public class Clef : Canvas
{
    public enum Type
    {
        Treble,
        Bass
    }

    #region IsHelperLinesVisibleProperty

    public static readonly DependencyProperty IsGuidLinesVisibleProperty = DependencyProperty.Register(
        nameof(IsGuidLinesVisible), typeof(bool), typeof(Clef),
        new FrameworkPropertyMetadata(
            default(bool),
            (d, e) =>
            {
                // zmieniła się prarametr określający czy wyświetlać linie pomocnicze
                var clef = (Clef) d;
                var glyph = (Glyph) clef.Children[0];
                var isVisible = (bool) e.NewValue;
                glyph.IsGuidLinesVisible = isVisible;
            }));


    public bool IsGuidLinesVisible
    {
        get => (bool) GetValue(IsGuidLinesVisibleProperty);
        set => SetValue(IsGuidLinesVisibleProperty, value);
    }

    #endregion

    public static Clef Create(Type type)
    {
        return type switch
        {
            Type.Treble => new TrebleClef(),
            Type.Bass => new BassClef(),
            _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
        };
    }

    protected Clef(string glyphText, Staff.Line staffLine)
    {
        // dodaj do canvas znak
        var glyph = new Glyph {Text = glyphText};
        Children.Add(glyph);

        // i ustaw go na wskazywaną pozycję na pięciolinii
        var top = Staff.TopOf(staffLine);
        SetTop(glyph, top);

        // ustaw jego wymiary
        Width = Glyph.ActualWidth;
        Height = Glyph.Height;
    }

    private Glyph Glyph => (Glyph) Children[0];

    protected override void OnRenderSizeChanged(SizeChangedInfo sizeInfo)
    {
        // po narysowaniu znaku zaktualizuj wymiary canvas
        Width = Glyph.ActualWidth;
        Height = Glyph.Height;
        base.OnRenderSizeChanged(sizeInfo);
    }
}

public class TrebleClef : Clef
{
    public TrebleClef() : base(Glyph.TrebleClef, Staff.Line.Second)
    {
    }
}

public class BassClef : Clef
{
    public BassClef() : base(Glyph.BassClef, Staff.Line.Fourth)
    {
    }
}