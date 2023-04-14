using System;
using System.Linq;
using System.Windows;
using System.Windows.Data;

namespace Pianola;

// public class TrebleClef : Clef
// {
//     public TrebleClef() : base(Types.Treble, Staff.TopOf(Staff.Line.Second))
//     {
//     }
// }
//
// public class BassClef : Clef
// {
//     public BassClef() : base(Types.Bass, Staff.TopOf(Staff.Line.Fourth))
//     {
//     }
// }

/// <remarks>
/// Struktura elementu:
/// <code>
///     Canvas
///         Sign
/// </code>
/// </remarks>
public class Clef : CustomizedCanvas
{
    public enum Types
    {
        Treble,
        Bass
    }

    private Sign Sign => Children.OfType<Sign>().First();
    
    // public static Clef Create(Types type)
    // {
    //     return type switch
    //     {
    //         Types.Treble => new TrebleClef(),
    //         Types.Bass => new BassClef(),
    //         _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
    //     };
    // }
    
    

    public Clef()
    {
        // // utwórz znak klucza
        // var clefSignText = type switch
        // {
        //     Types.Treble => Sign.TrebleClef,
        //     Types.Bass => Sign.BassClef,
        //     _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
        // };
        // var clefSign = new Sign
        // {
        //     Text = clefSignText,
        //     VerticalAlignment = VerticalAlignment.Top
        // };
        //
        // // dodaj go do płótna i ustaw w pionie tak aby wskaywał właściwą linię pięciolinii
        // Children.Add(clefSign);
        // SetTop(clefSign, -clefSign.BaseLine + top);
        //
        // // płótno ma dostosowywać swoją szerokość do szerokości klucza
        // SetBinding(WidthProperty, new Binding(nameof(ActualWidth)) {Source = clefSign});
        //
        // // a wysokość do wysokości pięciolinii
        // Height = Staff.LinesHeight;
        
        // dodaj do płótna znak klucza
        var clefSign = new Sign {Text = Sign.TrebleClef};
        Children.Add(clefSign);

        // przesuń znak klucz na pozycję piątej linii w pięciolinii
        // SetTop(clefSign, -clefSign.BaseLine);

        // płótno ma dostosowywać swoją szerokość do szerokości klucza
        SetBinding(WidthProperty, new Binding(nameof(Width)) {Source = clefSign});

        // a wysokość do wysokości pięciolinii
        Height = Staff.LinesHeight;
    }
    
    #region TypeProperty

    public static readonly DependencyProperty TypeProperty = DependencyProperty.Register(
        nameof(Type), typeof(Types), typeof(Clef),
        new FrameworkPropertyMetadata(
            default(Types),
            FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.AffectsMeasure,
            TypePropertyChangedCallback));

    private static void TypePropertyChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        // ustawiono typ klucza
        var clef = (Clef) d;
        var type = (Types) e.NewValue;

        // ustaw odpowiedni tekst klucza
        var clefSign = clef.Sign;
        clefSign.Text = type switch
        {
            Types.Treble => Sign.TrebleClef,
            Types.Bass => Sign.BassClef,
            _ => throw new ArgumentOutOfRangeException()
        };

        // przesuń znak klucza na pozycję piątej linii w pięciolinii
        SetTop(clefSign, -clefSign.BaseLine);
        
        // //
        //
        // // ustaw odpowiedni tekst znaku chromatycznego
        // var chromaticSign = clef.Sign;
        // chromaticSign.Text = type switch
        // {
        //     Types.Sharp => Sign.Sharp,
        //     Types.Flat => Sign.Flat,
        //     Types.Natural => Sign.Natural,
        //     _ => throw new ArgumentOutOfRangeException()
        // };
        //
        // // przesuń znak chromatyczny na pozycję piątej linii w pięciolinii
        // SetTop(chromaticSign, -chromaticSign.BaseLine);
    }

    public Types Type
    {
        get => (Types) GetValue(TypeProperty);
        set => SetValue(TypeProperty, value);
    }

    #endregion
}