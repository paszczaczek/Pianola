using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Pianola;

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

public class Scale : CustomizedCanvas
{
    public enum Type
    {
        Ces,
        Cis
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

    // TODO zamienić OnRener na OnVisualChildrenChanged
    protected override void OnRender(DrawingContext dc)
    {
        // rozmieszczamy znaki chromatyczne w poziome, tak by były jeden za drugim
        var width = .0;
        foreach (FrameworkElement chromatic in Children)
        {
            if (chromatic is Border) continue; // TODO
            SetLeft(chromatic, width);
            width += chromatic.ActualWidth;
        }

        Width = width;
    }
}