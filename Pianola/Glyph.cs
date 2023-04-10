using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Pianola;

public class Glyph : TextBlock
{
    private const string FamilyName = "feta26";
    private new const double FontSize = 48;

    private static readonly double BaseLine;
    public static readonly double HeadHeight;

    protected const string TrebleClef = "\x00c9";
    protected const string BassClef = "\x00c7";
    private const string BlackNoteHead = "\x0056";
    // private const string WhiteNoteHead = "\x0055";

    static Glyph()
    {
        // wyznacz baseline i wysokość główki nuty (przy starcie aplikacji)
        var ft = new FormattedText(
            BlackNoteHead,
            CultureInfo.InvariantCulture,
            FlowDirection.LeftToRight,
            new Typeface(FamilyName),
            FontSize,
            Brushes.Black, 1);
        BaseLine = ft.Baseline;
        HeadHeight = ft.Extent;
    }

    protected Glyph()
    {
        FontFamily = new FontFamily(FamilyName);
        base.FontSize = FontSize;
        
        // przesuń baseline znaku w górę do współrzędnej zerowej
        Margin = new Thickness(0, -BaseLine, 0, 0);
    }
}