using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Pianola;

public class Chromatic : Canvas
{
    public enum Type
    {
        Sharp,
        Flat,
        Natural
    }
    
    #region IsHelperLinesVisibleProperty

    public static readonly DependencyProperty IsGuidLinesVisibleProperty = DependencyProperty.Register(
        nameof(IsGuidLinesVisible), typeof(bool), typeof(Chromatic),
        new FrameworkPropertyMetadata(
            default(bool),
            (d, e) =>
            {
                // zmieniła się prarametr określający czy wyświetlać linie pomocnicze
                var chromatic = (Chromatic) d;
                var isVisible = (bool) e.NewValue;
                chromatic.ChromaticSign.IsGuidLinesVisible = isVisible;
            }));


    public bool IsGuidLinesVisible
    {
        get => (bool) GetValue(IsGuidLinesVisibleProperty);
        set => SetValue(IsGuidLinesVisibleProperty, value);
    }

    #endregion

    public static Chromatic Create(Type type, double position)
    {
        return type switch
        {
            Type.Sharp => new Sharp(position),
            Type.Flat => new Flat(position),
            Type.Natural => new Natural(position),
            _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
        };
    }

    protected Chromatic(Type chromaticType, double chromaticTop)
    {
        {
            // dodaj do canvas znak
            var chromatic = ChromaticSign.Create(chromaticType);
            Children.Add(chromatic);
            
            // i ustaw go na wskazywaną pozycję na pięciolinii
            var top = 0
                       -chromatic.BaseLine // przesun baseline znaku w gore do poczatku ukladu wspolrzednych
                      + chromaticTop // przesun baseline znaku w dol na wskazywaną pozycję na pięciolinii
                ;
            SetTop(chromatic, top);
            
            Width = 100;
        }
    }
    
    private ChromaticSign ChromaticSign => (ChromaticSign) Children[0];


    protected override void OnRender(DrawingContext dc)
    {
        // rozmieszczamy znaki chromatyczne w poziome, tak by były jeden za drugim
        var width = .0;
        foreach (FrameworkElement chromatic in Children)
        {
            SetLeft(chromatic, width);
            width += chromatic.ActualWidth;
        }
        
        Width = width;
    }
}

public class Sharp : Chromatic
{
    public Sharp(double top) : base(Type.Sharp, top)
    {
    }
}

public class Flat : Chromatic
{
    public Flat(double top) : base(Type.Flat, top)
    {
    }
}

public class Natural : Chromatic
{
    public Natural(double top) : base(Type.Natural, top)
    {
    }
}