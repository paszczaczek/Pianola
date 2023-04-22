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
        var chromaticSign = new Sign {Text = Sign.Sharp};
        Children.Add(chromaticSign);

        // przesuń znak chromatyczny na pozycję piątej linii w pięciolinii
        SetTop(chromaticSign, -chromaticSign.BaseLine);

        // płótno ma dostosowywać swoją szerokość do szerokości znaku chromatycznego
        SetBinding(WidthProperty, new Binding(nameof(Width)) {Source = chromaticSign});

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
        var chromaticSign = chromatic.Sign;
        chromaticSign.Text = type switch
        {
            Types.Sharp => Sign.Sharp,
            Types.Flat => Sign.Flat,
            Types.Natural => Sign.Natural,
            _ => throw new ArgumentOutOfRangeException()
        };

        // przesuń znak chromatyczny na pozycję piątej linii w pięciolinii
        SetTop(chromaticSign, -chromaticSign.BaseLine);
    }

    public Types Type
    {
        get => (Types) GetValue(TypeProperty);
        set => SetValue(TypeProperty, value);
    }

    #endregion
}
