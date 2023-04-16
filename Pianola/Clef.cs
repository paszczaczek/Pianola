using System;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using System.Windows.Diagnostics;

namespace Pianola;

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


    public Clef()
    {
        // dodaj do płótna znak klucza
        var clefSign = new Sign {Text = Sign.TrebleClef};
        Children.Add(clefSign);
        
        // przesuń klucz na właściwą linię pięciolinii
        SetClefTop(clefSign, Types.Treble);

        
        // płótno ma dostosowywać swoją szerokość do szerokości klucza
        SetBinding(WidthProperty, new Binding(nameof(Width)) {Source = clefSign});

        // a wysokość do wysokości pięciolinii
        Height = Staff.LinesHeight;
    }

    private static void SetClefTop(Sign sign, Types types)
    {
        var top = types switch
        {
            Types.Treble => Staff.TopOf(Staff.Position.SecondLine),
            Types.Bass => Staff.TopOf(Staff.Position.FourthLine),
            _ => throw new ArgumentOutOfRangeException()
        };

        // przesuń znak klucza na pozycję piątej linii w pięciolinii
        SetTop(sign, -sign.BaseLine + top);
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
        
        SetClefTop(clefSign, type);
    }

    public Types Type
    {
        get => (Types) GetValue(TypeProperty);
        set => SetValue(TypeProperty, value);
    }

    #endregion
}