using System;
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
        var top = -Sign.BaseLine; // przesun baseline znaku w gore do poczatku ukladu wspolrzednych
        var glyph = new Sign {Text = glyphText};
        Children.Add(glyph);
        SetTop(glyph, top);
        // Background = Brushes.Aquamarine;
    }

    protected override void OnRender(DrawingContext dc)
    {
        // aktualizujemy wymiary znaku chromatycznego na podstawie wymiarów glypha 
        var glyph = (Sign)Children[0];
        Width = glyph.ActualWidth;
        Height = glyph.ActualHeight;
        base.OnRender(dc);
    }
}

public class Sharp : Chromatic
{
    public Sharp() : base(Sign.Sharp)
    {
    }
}

public class Flat : Chromatic
{
    public Flat() : base(Sign.Flat)
    {
    }
}

public class Natural : Chromatic
{
    public Natural() : base(Sign.Natural)
    {
    }
}