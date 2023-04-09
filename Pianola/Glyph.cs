using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Pianola;

public class Glyph : TextBlock
{
    private new const string FontFamily = "feta26";
    private new const double FontSize = 48;

    public static readonly double BaseLine;
    public static readonly double HeadHeight;

    protected const string TrebleClef = "\x00c9";
    protected const string BassClef = "\x00c7";
    private const string BlackNoteHead = "\x0056";
    public const string WhiteNoteHead = "\x0055";

    static Glyph()
    {
        // wyznacz baseline i wysokość główki nuty (przy starcie aplikacji)
        var ft = new FormattedText(
            BlackNoteHead,
            CultureInfo.InvariantCulture,
            FlowDirection.LeftToRight,
            new Typeface(FontFamily),
            FontSize,
            Brushes.Black, 1);
        BaseLine = ft.Baseline;
        HeadHeight = ft.Extent;
    }

    protected Glyph()
    {
        base.FontFamily = new FontFamily(FontFamily);
        base.FontSize = FontSize;
        BaselineOffset = 2400;
    }
}