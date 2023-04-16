using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace Pianola;

public class Scale : CustomizedCanvas
{
    public enum Types
    {
        Ces,
        Cis
    }

    private static readonly IReadOnlyDictionary<Types, Description> Descriptions =
        new Dictionary<Types, Description>
        {
            {
                Types.Ces,
                new Description
                {
                    ChromaticType = Chromatic.Types.Flat,
                    StaffPositions = new[]
                    {
                        Staff.Position.ThirdLine,
                        Staff.Position.FourthSpace,
                        Staff.Position.SecondSpace,
                        Staff.Position.FourthLine,
                        Staff.Position.SecondLine,
                        Staff.Position.ThirdSpace,
                        Staff.Position.FirstSpace
                    }
                }
            },
            {
                Types.Cis,
                new Description
                {
                    ChromaticType = Chromatic.Types.Sharp,
                    StaffPositions = new[]
                    {
                        Staff.Position.FifthLine,
                        Staff.Position.ThirdSpace,
                        Staff.Position.FirstSpaceAbove,
                        Staff.Position.FourthLine,
                        Staff.Position.SecondSpace,
                        Staff.Position.FourthSpace,
                        Staff.Position.SecondLine
                    }
                }
            }
        };

    private class Description
    {
        public Chromatic.Types ChromaticType;
        public Staff.Position[] StaffPositions = null!;
    }

    public Scale()
    {
        // dodaj do płótna stackpanel, będą do niego dodawane znaki chromatyczne
        var stackPanel = new StackPanel {Orientation = Orientation.Horizontal};
        Children.Add(stackPanel);

        // płótno ma dostosowywać swoją szerokość do sumy szerokości znaków chromatycznych
        SetBinding(WidthProperty, new Binding(nameof(ActualWidth)) {Source = stackPanel});

        // a wysokość do wysokości pięciolinii
        Height = Staff.LinesHeight;
    }

    private Types _type;

    public Types Type
    {
        get => _type;
        set => OnTypeChanged(value);
    }

    private void OnTypeChanged(Types type)
    {
        _type = type;

        // usuń ze stackanelu wszystkie znaki chromatyczne (jeśli jakieś były)
        var stackPanel = Children.OfType<StackPanel>().First();
        stackPanel.Children.Clear();

        // pobierz definicję skali
        var description = Descriptions[type];

        // dodaj jej wszystki znaki chromatyczne do stackpanelu
        foreach (var staffPosition in description.StaffPositions)
        {
            // utwórz znak chromatyczny
            var chromatic = new Chromatic
            {
                Type = description.ChromaticType,
                VerticalAlignment = VerticalAlignment.Top
            };

            // dodaj go do własnego płótna, tak by można było ustalić jego położenie w pionie 
            var canvas = new Canvas();
            canvas.SetBinding(WidthProperty, new Binding(nameof(Width)) {Source = chromatic});
            canvas.Children.Add(chromatic);
            var top = Staff.TopOf(staffPosition);
            SetTop(chromatic, top);

            // a płótno dodaj do stackpanelu
            stackPanel.Children.Add(canvas);
        }
    }
}