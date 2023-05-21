using Pianola.MAUI.Models;
using Pianola.MAUI.Views;

namespace Pianola.MAUI.Drawables;

public class KeySignatureStaffDrawable : ViewResizer<KeySignatureView>, IDrawable
{
    // ułożenie znaków chromatycznych w ...
    private static readonly IReadOnlyDictionary<ChromaticSign, (Pitch, Octave)[]> Definitions =
        new Dictionary<ChromaticSign, (Pitch, Octave)[]>
        {
            {
                // w syngaturze 7 krzyżyków
                ChromaticSign.Sharp, new[]
                {
                    (Pitch.F, Octave.O5),
                    (Pitch.C, Octave.O5),
                    (Pitch.G, Octave.O5),
                    (Pitch.D, Octave.O5),
                    (Pitch.A, Octave.O4),
                    (Pitch.E, Octave.O5),
                    (Pitch.B, Octave.O4),
                }
            },
            {
                // w syngaturze 7 bemoli
                ChromaticSign.Flat, new[]
                {
                    (Pitch.B, Octave.O4),
                    (Pitch.E, Octave.O5),
                    (Pitch.A, Octave.O4),
                    (Pitch.D, Octave.O5),
                    (Pitch.G, Octave.O4),
                    (Pitch.C, Octave.O5),
                    (Pitch.F, Octave.O4),
                }
            }
        };

    public void Draw(ICanvas canvas, RectF dirtyRect)
    {
        if (ResizeView(canvas, dirtyRect)) return;
        DrawKeySignatures(canvas);
    }

    protected override Rect CalculateBounds(ICanvas canvas, RectF dirtyRect)
    {
        return DrawKeySignatures(canvas, calculateBoundsOnly: true);
    }

    private Rect DrawKeySignatures(ICanvas canvas, bool calculateBoundsOnly = false)
    {
        // jakich znaków trzeba użyć, krzyżyków czy bemoli?
        var sign = View.Signature.ChromaticSign switch
        {
            ChromaticSign.Sharp => Sign.Sharp,
            ChromaticSign.Flat => Sign.Flat,
            _ => throw new ArgumentOutOfRangeException(
                nameof(View.Signature.ChromaticSign),
                View.Signature.ChromaticSign, "Key signature can only contain sharp or flats.")
        };

        var bounds = new Rect();

        // sygnaturę trzeba narysować na obu pięcioliniach
        foreach (var clef in new[] {Clef.Treble, Clef.Bass})
        {
            var signLocation = new Point(0, 0);
            for (var i = 0; i < View.Signature.NumberOfChromaticSigns; i++)
            {
                // na której linii/polu narysować znak chromatyczny?
                var (pitch, octave) = Definitions[View.Signature.ChromaticSign][i];
                if (clef == Clef.Bass) octave -= 2;
                var staffPosition = Staff.Position.For(pitch, octave, clef);

                // jaka to będzie jego współrzędna y?
                var signTop = StaffDrawable.StaffPositionToY(staffPosition, clef);
                signLocation.Y = signTop;

                // narysuj znak chromatyczny lub tylko wyznacz jego granice
                var signBounds = SignDrawable.Draw(canvas, signLocation, sign, calculateBoundsOnly);
                
                // wyznacz miejsce rysowania następnego znaku                
                signLocation.X += signBounds.Width;
                
                // wyznacz granice całej sygnatury
                bounds = bounds.Union(signBounds);
            }
        }

        return bounds;
    }
}