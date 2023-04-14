using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace Pianola;

public class CesScale : Scale
{
    public CesScale() : base(Chromatic.Types.Flat, new[]
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
    public CisScale() : base(Chromatic.Types.Sharp, new[]
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
    public enum Types
    {
        Ces,
        Cis
    }

    protected Scale(Chromatic.Types type, IEnumerable<double> tops)
    {
        // dodaj do płótna stackpanel, będą do niego dodawane znaki chromatyczne
        var stackPanel = new StackPanel {Orientation = Orientation.Horizontal};
        Children.Add(stackPanel);

        // dodaj do stackpanelu wszystkie chromatyczne
        foreach (var top in tops)
        {
            // utwórz znak chromatyczny
            var chromatic = new Chromatic
            {
                Type = type,
                VerticalAlignment = VerticalAlignment.Top
            };
            
            // dodaj go do własnego płótna, tak by można było ustalić jego położenie w pionie 
            var canvas = new Canvas();
            canvas.SetBinding(WidthProperty, new Binding(nameof(Width)) {Source = chromatic});
            canvas.Children.Add(chromatic);
            SetTop(chromatic, top);

            // a płótno dodaj do stackpanelu
            stackPanel.Children.Add(canvas);
        }

        // płótno ma dostosowywać swoją szerokość do sumy szerokości znaków chromatycznych
        SetBinding(WidthProperty, new Binding(nameof(ActualWidth)) {Source = stackPanel});
        
        // a wysokość do wysokości pięciolinii
        Height = Staff.LinesHeight;
    }
}