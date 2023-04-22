using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace Pianola;

public class KeySignature : CustomizedCanvas
{
    // nazwy sygnatur kluczy
    public enum Keys
    {
        FSharp,
        CSharp,
        G,
        D,
        A,
        E,
        B,

        BFlat,
        EFlat,
        AFlat,
        DFlat,
        GFlat,
        CFlat,
        F
    }

    #region informacje potrzebne do wygenerowania sygnatur kluczy https: //dictionary.onmusic.org/terms/3660-treble_clef

    private readonly Description[] _descriptions =
    {
        new(Keys.FSharp, Pitch.Names.F, Octave.Names.TwoLined, Chromatic.Types.Sharp, 6),
        new(Keys.CSharp, Pitch.Names.C, Octave.Names.TwoLined, Chromatic.Types.Sharp, 7),
        new(Keys.G, Pitch.Names.G, Octave.Names.TwoLined, Chromatic.Types.Sharp, 1),
        new(Keys.D, Pitch.Names.D, Octave.Names.TwoLined, Chromatic.Types.Sharp, 2),
        new(Keys.A, Pitch.Names.A, Octave.Names.OneLined, Chromatic.Types.Sharp, 3),
        new(Keys.E, Pitch.Names.E, Octave.Names.OneLined, Chromatic.Types.Sharp, 4),
        new(Keys.B, Pitch.Names.B, Octave.Names.OneLined, Chromatic.Types.Sharp, 5),

        new(Keys.BFlat, Pitch.Names.B, Octave.Names.OneLined, Chromatic.Types.Flat, 2),
        new(Keys.EFlat, Pitch.Names.E, Octave.Names.OneLined, Chromatic.Types.Flat, 3),
        new(Keys.AFlat, Pitch.Names.A, Octave.Names.OneLined, Chromatic.Types.Flat, 4),
        new(Keys.DFlat, Pitch.Names.D, Octave.Names.OneLined, Chromatic.Types.Flat, 5),
        new(Keys.GFlat, Pitch.Names.G, Octave.Names.OneLined, Chromatic.Types.Flat, 6),
        new(Keys.CFlat, Pitch.Names.C, Octave.Names.OneLined, Chromatic.Types.Flat, 7),
        new(Keys.F, Pitch.Names.F, Octave.Names.OneLined, Chromatic.Types.Flat, 1),
    };

    private record struct Description(
        Keys Key,
        Pitch.Names PitchName,
        Octave.Names OctaveName,
        Chromatic.Types ChromaticType,
        int ChromaticCount);

    #endregion

    private void SetType()
    {
        // https://www.google.com/search?client=firefox-b-d&q=music+key+signature#fpstate=ive&vld=cid:de4c87d3,vid:xY9Q0R0G2jM


        // usuń ze stackanelu wszystkie znaki chromatyczne (jeśli jakieś były)
        var stackPanel = Children.OfType<StackPanel>().First();
        stackPanel.Children.Clear();

        var description = _descriptions.Single(description => description.Key == Key);

        var chromaticStaffPositions = _descriptions
            .Take(description.ChromaticCount)
            .Select(desc =>
            {
                var octaveName = ClefType == Clef.Types.Treble
                    ? desc.OctaveName
                    : desc.OctaveName - 2;
                var staffPosition = Clef.StaffPosition(desc.PitchName, octaveName, ClefType);
                staffPosition = (Staff.Position)((int) staffPosition % 7);
                return staffPosition;
            })
            .ToArray();

        // dodaj jej wszystki znaki chromatyczne do stackpanelu
        foreach (var staffPosition in chromaticStaffPositions)
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


    public KeySignature()
    {
        // dodaj do płótna stackpanel, będą do niego dodawane znaki chromatyczne
        var stackPanel = new StackPanel {Orientation = Orientation.Horizontal};
        Children.Add(stackPanel);

        // płótno ma dostosowywać swoją szerokość do sumy szerokości znaków chromatycznych
        SetBinding(WidthProperty, new Binding(nameof(ActualWidth)) {Source = stackPanel});

        // a wysokość do wysokości pięciolinii
        Height = Staff.LinesHeight;
    }

    private Keys _key;

    public Keys Key
    {
        get => _key;
        set
        {
            _key = value;
            SetType();
        }
    }

    private Clef.Types _clefType;

    public Clef.Types ClefType
    {
        get => _clefType;
        set
        {
            _clefType = value;
            SetType();
        }
    }
}

public static class Pitch
{
    public enum Names
    {
        A,
        B,
        C,
        D,
        E,
        F,
        G
    }
}

public static class Octave
{
    public enum Names
    {
        SubContra,
        Contra,
        Great,
        Small,
        OneLined,
        TwoLined,
        ThreeLined,
        FourLined,
        FiveLined
    }
}