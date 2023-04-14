using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Pianola;

public class Staff : Grid
{
    public Staff()
    {
        /*
         * Grid
         *      StaffLines
         *      StackPanel
         *          Clef
         *          Scale
         *          Metrum TODO
         */

        // dodaj do płótna pięciolinię
        var staffLines = new StaffLines
        {
            VerticalAlignment = VerticalAlignment.Top
        };
        Children.Add(staffLines);

        // dodaj do płótna stackpanel, będą do niego dodawane klucz, skala itd.
        var stackPanel = new StackPanel
        {
            Orientation = Orientation.Horizontal,
            VerticalAlignment = VerticalAlignment.Top
        };
        Children.Add(stackPanel);

        // var rect = new Rectangle
        // {
        //     Width = 100,
        //     Height = 34,
        //     Fill = Brushes.Chartreuse,
        //     VerticalAlignment = VerticalAlignment.Top
        // };
        // Children.Add(rect);
        // return;

        // dodaj do stackpanelu klucz
        var clef = new Clef
        {
            VerticalAlignment = VerticalAlignment.Top,
            Height = 35
        };
        stackPanel.Children.Add(clef);
        
        // dodaj do stackpanelu skalę
        var scale = new CesScale();
        stackPanel.Children.Add(scale);

    }

    #region ClefTypeProperty

    public static readonly DependencyProperty ClefTypeProperty = DependencyProperty.Register(
        nameof(ClefType), typeof(Clef.Types), typeof(Staff),
        new FrameworkPropertyMetadata(
            default(Clef.Types),
            FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.AffectsMeasure,
            ClefTypePropertyChangedCallback));

    private static void ClefTypePropertyChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        return;
        // ustawiono typ znaku chromatycznego
        var staff = (Staff) d;
        var clefType = (Clef.Types) e.NewValue;

        // usuń stary klucz i dodaj nowy właściwego typu
        var stackPanel = staff.Children.OfType<StackPanel>().First();
        var clef = stackPanel.Children.OfType<Clef>().First();
        stackPanel.Children.Remove(clef);
        clef = new Clef
        {
            Type = clefType,
            VerticalAlignment = VerticalAlignment.Top
        };
        staff.Children.Add(clef);

        // przesuń znak chromatyczny na pozycję piątej linii w pięciolinii
        // clef.SetTop(chromaticSign, -chromaticSign.BaseLine);
    }

    public Clef.Types ClefType
    {
        get => (Clef.Types) GetValue(ClefTypeProperty);
        set => SetValue(ClefTypeProperty, value);
    }

    #endregion

    public static double SpaceHeight => Sign.HeadHeight;

    public static double LinesHeight => SpaceHeight * 4;

    // public new static double Height => SpaceHeight * 8;
    public static double TopOf(Line staffLine) => (int) staffLine * SpaceHeight;
    public static double TopOf(Space staffSpace) => (int) staffSpace * SpaceHeight + SpaceHeight / 2;

    public enum Line
    {
        Fifth = 0,
        Fourth = 1,
        Third = 2,
        Second = 3,
        First = 4
    }

    public enum Space
    {
        FirstAbove = -1,

        Fourth = 0,
        Third = 1,
        Second = 2,
        First = 3
    }
}