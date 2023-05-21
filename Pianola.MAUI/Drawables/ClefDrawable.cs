using Pianola.MAUI.Models;
using Pianola.MAUI.Views;

namespace Pianola.MAUI.Drawables;

public class ClefDrawable : ViewResizer<ClefView>, IDrawable
{
    // definicja kluczy
    public record ClefDefinition(Pitch Pitch, Octave Octave, Staff.Position StaffPosition);

    public static readonly IReadOnlyDictionary<Clef, ClefDefinition> ClefDefinitions =
        new Dictionary<Clef, ClefDefinition>
        {
            {Clef.Treble, new(Pitch.G, Octave.O4, Staff.Position.Line.Second)},
            {Clef.Bass, new(Pitch.F, Octave.O3, Staff.Position.Line.Fourth)}
        };

    public void Draw(ICanvas canvas, RectF dirtyRect)
    {
        if (ResizeView(canvas, dirtyRect)) return;
        DrawClefs(canvas);
    }

    protected override Rect CalculateBounds(ICanvas canvas, RectF dirtyRect)
    {
        return DrawClefs(canvas, calculateBoundsOnly: true);
    }

    private static Rect DrawClefs(ICanvas canvas, bool calculateBoundsOnly = false)
    {
        var bounds = new Rect();

        // trzeba narysować dwa klucze
        foreach (var clef in new[] {Clef.Treble, Clef.Bass})
        {
            // na której linii narysować klucz?
            var (_, _, clefStaffPosition) = ClefDefinitions[clef];
            
            // jaka to będzie współrzędna y?
            var clefBaseLineY = StaffDrawable.StaffPositionToY(clefStaffPosition, clef);
            var clefLocation = new Point(0, clefBaseLineY);
            
            // jakiego znaku trzeba użyć do narysowania klucza?
            var clefSign = clef switch
            {
                Clef.Treble => Sign.TrebleClef,
                Clef.Bass => Sign.BassClef,
                _ => throw new ArgumentOutOfRangeException(nameof(clef), clef, "")
            };
            
            // narysuj klucz
            var clefBounds = SignDrawable.Draw(canvas, clefLocation, clefSign, calculateBoundsOnly);
            
            // wyznacz granice obu kluczy
            bounds = bounds.Union(clefBounds);
        }

        return bounds;
    }
}