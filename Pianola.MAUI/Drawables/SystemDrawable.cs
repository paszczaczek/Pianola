using Pianola.MAUI.Models;
using Pianola.MAUI.Views;

namespace Pianola.MAUI.Drawables;

public class SystemDrawable : ViewResizer<SystemView>, IDrawable
{
    private const float VerticalStaffMargin = 30;
    private const int LinesInStaff = 5;
    private static float _staffSpaceHeight;
    private static double StaffHeight => _staffSpaceHeight * (LinesInStaff - 1);
    private const double TrebleStaffTop = VerticalStaffMargin;
    private static double BassStaffTop => TrebleStaffTop + StaffHeight + VerticalStaffMargin;
    public static double Height => BassStaffTop + StaffHeight + VerticalStaffMargin;

    public void Draw(ICanvas canvas, RectF dirtyRect)
    {
        if (ViewResized(canvas, dirtyRect, Property.Height)) return;

        DrawStaff(TrebleStaffTop);
        DrawStaff(BassStaffTop);

        canvas.DrawBounds(Bounds);
        return;

        void DrawStaff(double top)
        {
            canvas.StrokeColor = Colors.Black;
            var lineY = (float) top;
            for (var i = 0; i < LinesInStaff; i++)
            {
                canvas.DrawLine(x1: dirtyRect.X, y1: lineY, x2: dirtyRect.Width, y2: lineY);
                lineY += _staffSpaceHeight;
            }
        }
    }

    protected override Rect CalculateBounds(ICanvas canvas, RectF dirtyRect)
    {
        var bounds = dirtyRect;
        bounds.Bottom = (float) Height;
        return bounds;
    }

    public static double StaffPositionToY(Staff.Position staffPosition, Clef clef)
    {
        var staffPositionY = _staffSpaceHeight * (double) staffPosition.Number / 2;
        var clefTop = clef switch
        {
            Clef.Treble => TrebleStaffTop,
            Clef.Bass => BassStaffTop,
            _ => throw new ArgumentOutOfRangeException(nameof(clef), clef, null)
        };
        var top = clefTop + staffPositionY;
        return top;
    }

    public static void CalculateStaffSpaceHeight(ICanvas canvas)
    {
        if (_staffSpaceHeight != 0) return;
        var bounds = SignDrawable.Draw(canvas, Point.Zero, Sign.BlackNoteHead, calculateBoundsOnly: true);
        _staffSpaceHeight = (float) bounds.Height;
    }
}