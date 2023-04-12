using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Pianola;

/// <remarks>
/// Struktura elementu:
/// <code>
///     Canvas
///         TextBlock
/// </code>
/// </remarks>
public class Sign : Canvas
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
    
    #region TextProperty

    public static readonly DependencyProperty TextProperty = DependencyProperty.Register(
        nameof(Text), typeof(string), typeof(Sign),
        new FrameworkPropertyMetadata(
            default(string),
            FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.AffectsMeasure,
            (o, args) =>
            {
                // zmienił się tekst znaku
                var sign = (Sign) o;
                var text = (string) args.NewValue;

                // uaktualij tekst znaku
                sign.TextBlock.Text = text;

                // uaktualnij pozycję i wymiary znaku
                var m = sign.Measure();
                SetTop(sign.TextBlock, m.top);
                sign.Height = m.height;
                sign.Width = m.widht;
                sign.BaseLine = m.baseline;
            }));

    public string Text
    {
        get => (string) GetValue(TextProperty);
        set => SetValue(TextProperty, value);
    }

    #endregion

    #region IsHelperLinesVisibleProperty

    public static readonly DependencyProperty IsGuidLinesVisibleProperty = DependencyProperty.Register(
        nameof(IsGuidLinesVisible), typeof(bool), typeof(Sign),
        new FrameworkPropertyMetadata(
            default(bool),
            FrameworkPropertyMetadataOptions.AffectsRender,
            (d, e) =>
            {
                // zmieniła się prarametr określający czy wyświetlać liniie pomocnicze
                var sign = (Sign) d;
                var newIsVisible = (bool) e.NewValue;
                if (newIsVisible)
                {
                    // linie mają być wyświetlane
                    var m = sign.Measure();

                    // dodaj linię bazową dla znaku
                    var baseLine = new Line
                    {
                        X1 = 0,
                        Y1 = m.baseline,
                        X2 = sign.Width,
                        Y2 = m.baseline,
                        Stroke = Brushes.Violet,
                        StrokeDashArray = new DoubleCollection(new double[] {1}),
                        StrokeThickness = 1,
                        SnapsToDevicePixels = true
                    };
                    sign.Children.Add(baseLine);

                    // dodaj ramkę wokół znaku
                    var border = new Border
                    {
                        Width = sign.Width,
                        Height = sign.Height,
                        BorderBrush = Brushes.BlueViolet,
                        BorderThickness = new Thickness(1),
                        SnapsToDevicePixels = true
                    };
                    sign.Children.Add(border);
                }
                else
                {
                    // linie mają nie być wyświetlane - usuń je
                    var lines = sign.Children.OfType<Line>().ToList();
                    foreach (var line in lines) 
                        sign.Children.Remove(line);
                }
            }));


    public bool IsGuidLinesVisible
    {
        get => (bool) GetValue(IsGuidLinesVisibleProperty);
        set => SetValue(IsGuidLinesVisibleProperty, value);
    }

    #endregion

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
        // BaseLine = ft.Baseline;
    }

    private TextBlock TextBlock => (TextBlock) Children[0];

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