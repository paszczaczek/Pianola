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
                var sign = (Sign) clef.Children[0];
                var isVisible = (bool) e.NewValue;
                sign.IsGuidLinesVisible = isVisible;
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

    protected Clef(string clefText, Staff.Line staffLine)
    {
        // dodaj do canvas znak
        var sign = new Sign {Text = clefText};
        Children.Add(sign);

        // i ustaw go na wskazywaną pozycję na pięciolinii
        var top = Staff.TopOf(staffLine);
        SetTop(sign, top);

        // ustaw jego wymiary
        Width = Sign.ActualWidth;
        Height = Sign.Height;
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
    public TrebleClef() : base(Sign.TrebleClef, Staff.Line.Second)
    {
    }
}

public class BassClef : Clef
{
    public BassClef() : base(Sign.BassClef, Staff.Line.Fourth)
    {
    }
}