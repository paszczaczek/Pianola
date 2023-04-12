using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Pianola;

public class Scale : Canvas
{
    public enum Type
    {
        Ces,
        Cis
    }

    #region IsHelperLinesVisibleProperty

    public static readonly DependencyProperty IsGuidLinesVisibleProperty = DependencyProperty.Register(
        nameof(IsGuidLinesVisible), typeof(bool), typeof(Scale),
        new FrameworkPropertyMetadata(
            default(bool),
            (d, e) =>
            {
                // zmieniła się prarametr określający czy wyświetlać linie pomocnicze
                var scale = (Scale) d;
                var isVisible = (bool) e.NewValue;
                //scale.Chromatics.IsGuidLinesVisible = isVisible;
            }));


    public bool IsGuidLinesVisible
    {
        get => (bool) GetValue(IsGuidLinesVisibleProperty);
        set => SetValue(IsGuidLinesVisibleProperty, value);
    }

    #endregion

    public static Scale Create(Type type)
    {
        return type switch
        {
            Type.Ces => new CesScale(),
            Type.Cis => new CisScale(),
            _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
        };
    }

    protected Scale(Chromatic.Type chromaticType, IList<double> chromaticTops)
    {
        for (var i = 0; i < chromaticTops.Count; i++)
        {
            // dodaj do canvas znak chromatyczny
            var chromatic = Chromatic.Create(chromaticType, chromaticTops[i]);
            Children.Add(chromatic);

            SetLeft(chromatic, i * 20); // OnRender ustawi prawdziwe wartości
            Width = 100;
        }
    }
    
    private IEnumerable<Chromatic> Chromatics => Children.OfType<Chromatic>();


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

public class CesScale : Scale
{
    public CesScale() : base(Chromatic.Type.Flat, new double[]
    {
        Staff.TopOf(Staff.Line.Third),
        Staff.TopOf(Staff.Space.Fourth),
        Staff.TopOf(Staff.Space.Second),
        Staff.TopOf(Staff.Line.Fourth),
        Staff.TopOf(Staff.Line.Second),
        Staff.TopOf(Staff.Space.Third),
        Staff.TopOf(Staff.Space.First)
    })
    {
    }
}

public class CisScale : Scale
{
    public CisScale() : base(Chromatic.Type.Sharp, new double[]
    {
        Staff.TopOf(Staff.Line.Fifth),
        Staff.TopOf(Staff.Space.Third),
        Staff.TopOf(Staff.Space.FirstAbove),
        Staff.TopOf(Staff.Line.Fourth),
        Staff.TopOf(Staff.Space.Second),
        Staff.TopOf(Staff.Space.Fourth),
        Staff.TopOf(Staff.Line.Second)
    })
    {
    }
}