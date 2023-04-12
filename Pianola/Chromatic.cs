using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Pianola;

public class Chromatic : Sign
{
    public enum Type
    {
        Sharp,
        Flat,
        Natural
    }

    // #region IsHelperLinesVisibleProperty
    //
    // public static readonly DependencyProperty IsGuidLinesVisibleProperty = DependencyProperty.Register(
    //     nameof(IsGuidLinesVisible), typeof(bool), typeof(Chromatic),
    //     new FrameworkPropertyMetadata(
    //         default(bool),
    //         (d, e) =>
    //         {
    //             // zmieniła się prarametr określający czy wyświetlać linie pomocnicze
    //             var chromatic = (Chromatic) d;
    //             var isVisible = (bool) e.NewValue;
    //             chromatic.Sign.IsGuidLinesVisible = isVisible;
    //         }));
    //
    //
    // public bool IsGuidLinesVisible
    // {
    //     get => (bool) GetValue(IsGuidLinesVisibleProperty);
    //     set => SetValue(IsGuidLinesVisibleProperty, value);
    // }
    //
    // #endregion
    
    public static Chromatic Create(Type type)
    {
        return type switch
        {
            Type.Sharp => new Sharp(),
            Type.Flat => new Flat(),
            Type.Natural => new Natural(),
            _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
        };
    }

    protected Chromatic(string chromaticText)
    {
        Text = chromaticText;

        // dodaj do canvas znak
        // var sign = new Sign {Text = chromaticText};
        // Children.Add(sign);
        
        // i ustaw go na wskazywaną pozycję na pięciolinii
        // var top = 0
                // -sign.BaseLine // przesun baseline znaku w gore do poczatku ukladu wspolrzednych
            // + Staff.TopOf(staffLine); // przesun baseline znaku w dol na wskazywaną pozycję na pięciolinii
            ;
        // SetTop(sign, top);
        
        // i ustaw jego wymiary
        // Width = Sign.ActualWidth;
        // Height = Sign.Height;
        
        // var top = -Sign.BaseLine; // przesun baseline znaku w gore do poczatku ukladu wspolrzednych
        // var glyph = new Sign {Text = chromaticText};
        // Children.Add(glyph);
        // SetTop(glyph, top);
    }

    // private Sign Sign => (Sign) Children[0];

    // public double BaseLine => Sign.BaseLine;
    
    // protected override void OnRender(DrawingContext dc)
    // {
        // po narysowaniu znaku zaktualizuj wymiary canvas
        // Width = Sign.ActualWidth;
        // Height = Sign.ActualHeight;
        // base.OnRender(dc);
    // }
}

public class Sharp : Chromatic
{
    public Sharp() : base(Sign.Sharp)
    {
    }
}

public class Flat : Chromatic
{
    public Flat() : base(Sign.Flat)
    {
    }
}

public class Natural : Chromatic
{
    public Natural() : base(Sign.Natural)
    {
    }
}