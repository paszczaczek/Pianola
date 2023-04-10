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
            // przesun baseline znaku w gore do poczatku ukladu wspolrzednych
            // chromaticTops[i] -= -Glyph.BaseLine;
            var chromatic = Chromatic.Create(chromaticType);
            Children.Add(chromatic);
            SetTop(chromatic, chromaticTops[i]);    
            SetLeft(chromatic, i* 20);
            Width = 100;
        }
    }

    protected override void OnRender(DrawingContext dc)
    {
        // Width = Children.Cast<FrameworkElement>().Sum(child => child.ActualWidth);
        var width = .0;
        foreach (FrameworkElement chromatic in Children)
        {
            SetLeft(chromatic, width);
            width += chromatic.ActualWidth;
        }
        
        Width = width;
    }

    protected override void OnRenderSizeChanged(SizeChangedInfo sizeInfo)
    {
        // po narysowaniu znaków chromatycznych zaktualizuj szerokość canvas
        // Width = Children.Cast<FrameworkElement>().Sum(child => child.ActualWidth);
        // var width = .0;
        // foreach (FrameworkElement chromatic in Children)
        // {
        //     SetLeft(chromatic, width);
        //     width += chromatic.ActualWidth;
        // }
        //
        // Width = width;
        base.OnRenderSizeChanged(sizeInfo);
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