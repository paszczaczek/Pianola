using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

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
    public const string Flat = "\x003a";
    public const string Natural = "\x0036";
    public const string BlackNoteHead = "\x0056";
    public const string WhiteNoteHead = "\x0055";

    private static readonly SolidColorBrush GuidLinesBrush = new(Brushes.Violet.Color) { Opacity = 0.3 };

    #region IsHelperLinesVisibleProperty

    public static readonly DependencyProperty IsGuidLinesVisibleProperty = DependencyProperty.Register(
        nameof(IsGuidLinesVisible), typeof(bool), typeof(Glyph),
        new FrameworkPropertyMetadata(false,
            (d, e) =>
            {
                var glyph = (Glyph)d;
                var isHelperLinesVisible = (bool)e.NewValue;
                glyph.Background = isHelperLinesVisible
                    ? GuidLinesBrush
                    : Brushes.Transparent;
            }));

    public bool IsGuidLinesVisible
    {
        get => (bool)GetValue(IsGuidLinesVisibleProperty);
        set => SetValue(IsGuidLinesVisibleProperty, value);
    }

    #endregion

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

        var line = new Line
        {
            X1 = 0, Y1 = 10, X2 = 10, Y2 = 10 ,
            Stroke = Brushes.Black, 
            StrokeThickness = 10
        };
        Inlines.Add(line);
    }
}