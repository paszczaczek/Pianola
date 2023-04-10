using System;
using System.Windows;
using System.Windows.Controls;

namespace Pianola;

public class Clef : Canvas
{
    public enum Type
    {
        Treble,
        Bass
    }

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
        var top =
            -Glyph.BaseLine // przesun baseline znaku w gore do poczatku ukladu wspolrzednych
            + Staff.TopOf(staffLine); // przesun baseline znaku w dol na wskazywaną pozycję na pięciolinii
        var glyph = new Glyph {Text = glyphText};
        Children.Add(glyph);
        SetTop(glyph, top);
    }

    protected override void OnRenderSizeChanged(SizeChangedInfo sizeInfo)
    {
        // po narysowaniu glypg zaktualizuj szerokość canvas
        var glyph = (Glyph) Children[0];
        Width = glyph.ActualWidth;
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