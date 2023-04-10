using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Pianola;

public class Chromatic : Canvas
{
    public enum Type
    {
        Sharp,
        Flat,
        Natural
    }

    public static Chromatic Create(Type type)
    {
        return type switch
        {
            Type.Sharp => new Sharp(),
            Type.Flat => new Flat(),
            Type.Natural => new Natural(),
            _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
        };
    }

    protected Chromatic(string glyphText)
    {
        var top = -Glyph.BaseLine; // przesun baseline znaku w gore do poczatku ukladu wspolrzednych
        var glyph = new Glyph {Text = glyphText};
        Children.Add(glyph);
        SetTop(glyph, top);
    }

    protected override void OnRender(DrawingContext dc)
    {
        var glyph = (Glyph)Children[0];
        Width = glyph.ActualWidth;
        Height = glyph.ActualHeight;
        base.OnRender(dc);
    }

    protected override void OnRenderSizeChanged(SizeChangedInfo sizeInfo)
    {
        // po narysowaniu glypg zaktualizuj szerokość canvas
        var glyph = (Glyph) Children[0];
        Width = glyph.ActualWidth;
        base.OnRenderSizeChanged(sizeInfo);
    }
}

public class Sharp : Chromatic
{
    public Sharp() : base(Glyph.Sharp)
    {
    }
}

public class Flat : Chromatic
{
    public Flat() : base(Glyph.Flat)
    {
    }
}

public class Natural : Chromatic
{
    public Natural() : base(Glyph.Natural)
    {
    }
}