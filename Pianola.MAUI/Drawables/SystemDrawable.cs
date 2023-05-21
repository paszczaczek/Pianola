using Pianola.MAUI.Models;

namespace Pianola.MAUI.Drawables;

public class SystemDrawable : IDrawable
{
    private const float VerticalStaffMargin = 30; // TODO
    private const int LinesInStaff = 5;
    private const float SpaceHeight = 10; // TODO
    private const double StaffHeight = SpaceHeight * LinesInStaff;

    private const double TrebleStaffTop = VerticalStaffMargin;
    private const double BassStaffTop = TrebleStaffTop + StaffHeight + VerticalStaffMargin;

    public const double Height = BassStaffTop + StaffHeight + VerticalStaffMargin;

    public static double StaffPositionToY(Staff.Position staffPosition, Clef clef)
    {
        var staffPositionY = SpaceHeight * (double) staffPosition.Number / 2;
        var clefTop = clef switch
        {
            Clef.Treble => TrebleStaffTop,
            Clef.Bass => BassStaffTop,
            _ => throw new ArgumentOutOfRangeException(nameof(clef), clef, null)
        };
        var top = clefTop + staffPositionY;
        return top;
    }

    public void Draw(ICanvas canvas, RectF dirtyRect)
    {
        // narysuj pięciolinię wiolinową i basową
        DrawStaff(canvas, dirtyRect, (float) TrebleStaffTop);
        DrawStaff(canvas, dirtyRect, (float) BassStaffTop);

        var bounds = dirtyRect;
        bounds.Bottom = (float) Height;
        canvas.DrawBounds(bounds);
    }

    private static RectF DrawStaff(ICanvas canvas, RectF dirtyRect, float top)
    {
        canvas.StrokeColor = Colors.Black;

        var lineY = dirtyRect.Y = top;
        for (var i = 0; i < LinesInStaff; i++)
        {
            canvas.DrawLine(x1: dirtyRect.X, y1: lineY, x2: dirtyRect.Width, y2: lineY);
            lineY += SpaceHeight;
        }

        var bounds = dirtyRect;
        bounds.Top = top;
        bounds.Bottom = lineY;
        return bounds;
    }
}