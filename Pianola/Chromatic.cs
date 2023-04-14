using System;
using System.Linq;
using System.Windows;
using System.Windows.Data;

namespace Pianola;

public class Chromatic : CustomizedCanvas
{
    public enum Types
    {
        Sharp,
        Flat,
        Natural
    }

    private Sign Sign => Children.OfType<Sign>().First();

    public Chromatic()
    {
        // dodaj do płótna znak chromatyczny
        var sign = new Sign {Text = Sign.Sharp};
        Children.Add(sign);

        // przesuń znak chromatyczny na pozycję piątej linii w pięciolinii
        SetTop(sign, -sign.BaseLine);

        // płótno ma dostosowywać swoją szerokość do szerokości znaku chromatycznego
        SetBinding(WidthProperty, new Binding(nameof(Width)) {Source = sign});

        // a wysokość do wysokości pięciolinii
        Height = Staff.LinesHeight;
    }

    #region TypeProperty

    public static readonly DependencyProperty TypeProperty = DependencyProperty.Register(
        nameof(Type), typeof(Types), typeof(Chromatic),
        new FrameworkPropertyMetadata(
            default(Types),
            FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.AffectsMeasure,
            TypePropertyChangedCallback));

    private static void TypePropertyChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        // ustawiono typ znaku chromatycznego
        var chromatic = (Chromatic) d;
        var type = (Types) e.NewValue;

        // ustaw odpowiedni tekst znaku chromatycznego
        var sign = chromatic.Sign;
        sign.Text = type switch
        {
            Types.Sharp => Sign.Sharp,
            Types.Flat => Sign.Flat,
            Types.Natural => Sign.Natural,
            _ => throw new ArgumentOutOfRangeException()
        };

        // przesuń znak chromatyczny na pozycję piątej linii w pięciolinii
        SetTop(sign, -sign.BaseLine);
    }

    public Types Type
    {
        get => (Types) GetValue(TypeProperty);
        set => SetValue(TypeProperty, value);
    }

    #endregion

    //
    // #region PositionProperty
    //
    // public static readonly DependencyProperty PositionProperty = DependencyProperty.Register(
    //     nameof(Position), typeof(int), typeof(Chromatic),
    //     new FrameworkPropertyMetadata(
    //         default(int),
    //         FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.AffectsMeasure,
    //         PositionPropertyChangedCallback));
    //
    // private static void PositionPropertyChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
    // {
    //     // ustawiono pozycję znaku chromatycznego na pięciolinii
    //     var chromatic = (Chromatic) d;
    //     var position = (int) e.NewValue;
    //
    //     Staff.TopOf()
    //     var sign = chromatic.Sign;
    //     SetTop(sign, -sign.BaseLine);
    // }
    //
    // public int Position
    // {
    //     get => (int) GetValue(PositionProperty);
    //     set => SetValue(PositionProperty, value);
    // }
    //
    // #endregion
}
