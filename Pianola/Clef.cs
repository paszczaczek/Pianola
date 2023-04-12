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
                clef.ClefSign.IsGuidLinesVisible = isVisible;
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

    protected Clef(Type clefType, double clefTop)
    {
        // dodaj do canvas znak
        var clefSign = ClefSign.Create(clefType);
        Children.Add(clefSign);

        // i ustaw go na wskazywaną pozycję na pięciolinii
        var top =
            -clefSign.BaseLine // przesun baseline znaku w gore do poczatku ukladu wspolrzednych
            + clefTop; // przesun baseline znaku w dol na wskazywaną pozycję na pięciolinii
        SetTop(clefSign, top);

        // ustaw jego wymiary
        Width = clefSign.ActualWidth;
        Height = clefSign.Height;
    }

    private ClefSign ClefSign => (ClefSign) Children[0];

    protected override void OnRenderSizeChanged(SizeChangedInfo sizeInfo)
    {
        // po narysowaniu znaku zaktualizuj wymiary canvas
        Width = ClefSign.ActualWidth;
        Height = ClefSign.Height;
        base.OnRenderSizeChanged(sizeInfo);
    }
}

public class TrebleClef : Clef
{
    public TrebleClef() : base(Type.Treble, Staff.TopOf(Staff.Line.Second))
    {
    }
}

public class BassClef : Clef
{
    public BassClef() : base(Type.Bass, Staff.TopOf(Staff.Line.Fourth))
    {
    }
}