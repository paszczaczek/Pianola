using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Pianola;

public class Staff : Grid
{
    private readonly StaffLines _staffLines;

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

        // utworz pięciolinię
        _staffLines = new StaffLines {VerticalAlignment = VerticalAlignment.Top};

        // utwórz klucz
        // var clef = new TrebleClef();

        // var top = 0
        // -_clef.BaseLine // przesun baseline znaku w gore do poczatku ukladu wspolrzednych
        // + TopOf(Line.Second) // przesun baseline znaku w dol na wskazywaną pozycję na pięciolinii
        // ;
        // SetTop(_clef, top);

        // utwórz stack panel i odaj do niego niego klucz, skalę i metrum a później takty
        _stackPanel = new StackPanel {Orientation = Orientation.Horizontal};
        // _stackPanel.Children.Add(clef);
        // _stackPanel.Children.Add(new CesScale());
        // _stackPanel.Children.Add(new CisScale());
        // TODO dodaj metrum

        // dodaj do grida pięciolinię a nad nią stack panel z elementami
        Children.Add(_staffLines);
        Children.Add(_stackPanel);
    }

    // #region ClefTypeProperty
    //
    // public static readonly DependencyProperty ClefTypeProperty = DependencyProperty.Register(
    //     nameof(ClefType), typeof(Clef.Type), typeof(Staff),
    //     new FrameworkPropertyMetadata(
    //         Clef.Type.Treble,
    //         FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsRender,
    //         propertyChangedCallback: (d, e) =>
    //         {
    //             // ustawiono typ pięciolinii (wiolinowa/basowa) - dodaj do pięciolinii właściwy klucz
    //             var staff = (Staff) d;
    //             var newClefType = (Clef.Type) e.NewValue;
    //             staff._stackPanel.Children.Remove(staff._clef);
    //             // staff._clef = Clef.Create(newClefType);
    //             staff._stackPanel.Children.Add(staff._clef);
    //         }));
    //
    // public Clef.Type ClefType
    // {
    //     get => (Clef.Type) GetValue(ClefTypeProperty);
    //     set => SetValue(ClefTypeProperty, value);
    // }
    //
    // #endregion

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
        // var staff = (Staff) d;
        // var clefType = (Clef.Types) e.NewValue;

        // var clef = staff.Children.OfType<Clef>().First();
        // staff.Children.Remove(clef);
        // new Clef(clefType);
        // staff.Children.Add()
        
        // ustaw odpowiedni tekst znaku chromatycznego
        // var chromaticSign = chromatic.Sign;
        // chromaticSign.Text = type switch
        // {
            // Chromatic.Types.Sharp => Sign.Sharp,
            // Chromatic.Types.Flat => Sign.Flat,
            // Chromatic.Types.Natural => Sign.Natural,
            // _ => throw new ArgumentOutOfRangeException()
        // };

        // przesuń znak chromatyczny na pozycję piątej linii w pięciolinii
        // SetTop(chromaticSign, -chromaticSign.BaseLine);
    }

    public Clef.Types ClefType
    {
        get => (Clef.Types) GetValue(ClefTypeProperty);
        set => SetValue(ClefTypeProperty, value);
    }

    #endregion

    // private Clef _clef;

    private readonly StackPanel _stackPanel;

    protected override void OnRenderSizeChanged(SizeChangedInfo sizeInfo)
    {
        base.OnRenderSizeChanged(sizeInfo);
        if (!sizeInfo.WidthChanged) return;
        _staffLines.Width = sizeInfo.NewSize.Width;
    }

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