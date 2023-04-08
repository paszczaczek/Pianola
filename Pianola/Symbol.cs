using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Pianola;

public class Symbol : TextBlock
{
    private new const string FontFamily = "feta26";
    private new const double FontSize = 48;
    public static readonly double BaseLine;
    public static readonly double HeadHeight;

    static Symbol()
    {
        // wyznacz baseline i wysokość główki nuty
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

    public Symbol()
    {
        base.FontFamily = new FontFamily(FontFamily);
        base.FontSize = FontSize;
        // Background = Brushes.Bisque;
    }

    public const string TrebleClef = "\x00c9";
    public const string BassClef = "\x00c7";
    private const string BlackNoteHead = "\x0056";
    public const string WhiteNoteHead = "\x0055";
}