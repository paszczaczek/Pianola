using System.Linq;
using System.Windows;
using System.Windows.Controls;

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

        // dodaj do stackpanelu klucz
        var clef = new Clef {VerticalAlignment = VerticalAlignment.Top};
        stackPanel.Children.Add(clef);

        // dodaj do stackpanelu skalę
        // var scale = new CesScale {VerticalAlignment = VerticalAlignment.Top};
        var scale = new KeySignature {Key = KeySignature.Keys.CFlat, VerticalAlignment = VerticalAlignment.Top};
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
        stackPanel.Children.Insert(0, clef);
    }

    public Clef.Types ClefType
    {
        get => (Clef.Types) GetValue(ClefTypeProperty);
        set => SetValue(ClefTypeProperty, value);
    }

    #endregion

    #region ScaleTypeProperty

    public static readonly DependencyProperty ScaleKeyProperty = DependencyProperty.Register(
        nameof(ScaleKey), typeof(KeySignature.Keys), typeof(Staff),
        new FrameworkPropertyMetadata(
            // default(Scale.Types),
            KeySignature.Keys.CFlat,
            FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.AffectsMeasure,
            ScaleTypePropertyChangedCallback));

    private static void ScaleTypePropertyChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        // ustawiono typ skali
        var staff = (Staff) d;
        var scaleType = (KeySignature.Keys) e.NewValue;

        // usuń starą skalę i dodaj nowa właściwego typu
        var stackPanel = staff.Children.OfType<StackPanel>().First();
        var scale = stackPanel.Children.OfType<KeySignature>().First();
        stackPanel.Children.Remove(scale);
        // scale = Scale.Create(scaleType);
        scale = new KeySignature
        {
            Key = scaleType,
            VerticalAlignment = VerticalAlignment.Top
        };
        stackPanel.Children.Insert(1, scale);
    }

    public KeySignature.Keys ScaleKey
    {
        get => (KeySignature.Keys) GetValue(ScaleKeyProperty);
        set => SetValue(ScaleKeyProperty, value);
    }

    #endregion

    private static double SpaceHeight => Sign.HeadHeight;

    public static double LinesHeight => SpaceHeight * 4;

    public enum Space
    {
        FirstAbove = -1,

        Fourth = 0,
        Third = 1,
        Second = 2,
        First = 3
    }

    public enum Position
    {
        FirstLineAbove = -2,
        FirstSpaceAbove = -1,

        FifthLine = 0,

        FourthSpace,
        FourthLine,

        ThirdSpace,
        ThirdLine,

        SecondSpace,
        SecondLine,

        FirstSpace,
        FirstLine,

        FirsSpaceBelow,
        FirstLineBelow
    }

    public static double TopOf(Position position) => (double) position / 2 * SpaceHeight;
}