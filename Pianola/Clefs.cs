using System;
using System.Windows;
using System.Windows.Controls;

namespace Pianola;

/// <remarks>
/// Struktura elementu:
/// <code>
///     Canvas
///         Sign
/// </code>
/// </remarks>
public class Clef : Canvas
{
    public enum Type
    {
        Treble,
        Bass
    }


    #region IsHelperLinesVisibleProperty

    public static readonly DependencyProperty IsGuidLinesVisibleProperty = DependencyProperty.Register(
        nameof(IsGuidLinesVisible), typeof(bool), typeof(Clef),
        new FrameworkPropertyMetadata(
            default(bool),
            (d, e) =>
            {
                // zmieniła się prarametr określający czy wyświetlać linie pomocnicze
                var clef = (Clef) d;
                var isVisible = (bool) e.NewValue;
                // clef.Sign.IsGuidLinesVisible = isVisible;
            }));


    public bool IsGuidLinesVisible
    {
        get => (bool) GetValue(IsGuidLinesVisibleProperty);
        set => SetValue(IsGuidLinesVisibleProperty, value);
    }

    #endregion

    public static Clef Create(Type type)
    {
        return type switch
        {
            Type.Treble => new TrebleClef(),
            Type.Bass => new BassClef(),
            _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
        };
    }

    protected Clef(string clefText, double clefTop)
    {
        // dodaj do canvas znak
        var clef = new Sign {Text = clefText};
        Children.Add(clef);

        // i ustaw go na wskazywaną pozycję na pięciolinii
        var top = 0
                  - clef.BaseLine // przesun baseline znaku w gore do poczatku ukladu wspolrzednych
                  + clefTop // przesun baseline znaku w dol na wskazywaną pozycję na pięciolinii
            // + Staff.TopOf(Staff.Line.Fifth) // przesun baseline znaku w dol na wskazywaną pozycję na pięciolinii
            ;
        SetTop(clef, top);

        // ustaw jego wymiary
        Width = clef.ActualWidth;
        Height = clef.Height;
    }

    private Sign Sign => (Sign) Children[0];

    protected override void OnRenderSizeChanged(SizeChangedInfo sizeInfo)
    {
        // po narysowaniu znaku zaktualizuj wymiary canvas
        Width = Sign.ActualWidth;
        Height = Sign.Height;
        base.OnRenderSizeChanged(sizeInfo);
    }
}

public class TrebleClef : Clef
{
    public TrebleClef() : base(Sign.TrebleClef, Staff.TopOf(Staff.Line.Second))
    {
    }
}

public class BassClef : Clef
{
    public BassClef() : base(Sign.BassClef, Staff.TopOf(Staff.Line.Fourth))
    {
    }
}