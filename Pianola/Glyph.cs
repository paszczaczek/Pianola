using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Pianola;

public class Glyph : TextBlock
{
    private const string FamilyName = "feta26";
    private new const double FontSize = 48;

    public static readonly double BaseLine;
    public static readonly double HeadHeight;

    public const string TrebleClef = "\x00c9";
    public const string BassClef = "\x00c7";
    public const string Sharp = "\x002e";
    public const string Flat = "\x002a";
    public const string Natural = "\x0036";
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

    public Glyph()
    {
        FontFamily = new FontFamily(FamilyName);
        base.FontSize = FontSize;
    }
}