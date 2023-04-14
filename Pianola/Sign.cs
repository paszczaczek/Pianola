using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Pianola;

/// <remarks>
/// Struktura elementu:
/// <code>
///     Canvas
///         TextBlock
/// </code>
/// </remarks>
public class Sign : CustomizedCanvas
{
    private const string FamilyName = "feta26"; // ok
    private const double FontSize = 48; // ok

    public double BaseLine { get; private set; }
    public static readonly double HeadHeight;

    public const string TrebleClef = "\x00c9";
    public const string BassClef = "\x00c7";
    public const string Sharp = "\x002e";
    public const string Flat = "\x003a";
    public const string Natural = "\x0036";
    public const string BlackNoteHead = "\x0056";
    public const string WhiteNoteHead = "\x0055";

    public Sign()
    {
        // dodaj do canvas znak
        var textBlock = new TextBlock
        {
            FontFamily = new FontFamily(FamilyName),
            FontSize = FontSize
        };
        Children.Add(textBlock);
    }

    static Sign()
    {
        // wysokość główki nuty (przy starcie aplikacji)
        var ft = new FormattedText(
            BlackNoteHead,
            CultureInfo.InvariantCulture,
            FlowDirection.LeftToRight,
            new Typeface(FamilyName),
            FontSize,
            Brushes.Black, 1);
        HeadHeight = ft.Extent;
    }

    private TextBlock TextBlock => (TextBlock) Children[0];

    #region TextProperty

    public static readonly DependencyProperty TextProperty = DependencyProperty.Register(
        nameof(Text), typeof(string), typeof(Sign),
        new FrameworkPropertyMetadata(
            default(string),
            FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.AffectsMeasure,
            TextPropertyChangedCallback));

    private static void TextPropertyChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        // zmienił się tekst znaku
        var sign = (Sign) d;
        var text = (string) e.NewValue;

        // uaktualij tekst znaku
        sign.TextBlock.Text = text;

        // uaktualnij pozycję i wymiary znaku
        var m = sign.Measure();
        SetTop(sign.TextBlock, m.top);
        sign.Height = m.height;
        sign.Width = m.widht;
        sign.BaseLine = m.baseline;
    }

    public string Text
    {
        get => (string) GetValue(TextProperty);
        set => SetValue(TextProperty, value);
    }

    #endregion

    private (double top, double widht, double height, double baseline) Measure()
    {
        var ft = new FormattedText(
            TextBlock.Text,
            CultureInfo.InvariantCulture,
            FlowDirection.LeftToRight,
            new Typeface(FamilyName),
            FontSize,
            Brushes.Black,
            VisualTreeHelper.GetDpi(this).PixelsPerDip);

        // odległość pomiędzy górną krawędzią textblock'u a nawyższym czarnym pikselem
        // na ogół ma wartość ujemną bo znak na ogół nie wykracza poza granice textblock'u
        var overhangBefore =
            ft.Height // wysokość tekstblock'u
            + ft.OverhangAfter // odległość pomiędzy dolną krawędzią textblocku a najniższym czarnym pikselem, na ogół ma wartość ujemną bo znak na ogół nie wykracza poza granice textblock'u
            - ft.Extent; // odległość pomiędzy najwyższym czarnym pikselem a najniższym czarnym pikselem

        // o ile trzeba przesunąć tekstblock żeby najwyższy czarny piksel znalazł się w punkcie Y = 0
        var top = -overhangBefore;

        // szerokość tekstblock'u
        var width = ft.Width;

        // wysokość tekstblock'u
        var height = ft.Extent;

        // linia bazowa
        var baseline = ft.Baseline - overhangBefore;

        return (top, width, height, baseline);
    }
}