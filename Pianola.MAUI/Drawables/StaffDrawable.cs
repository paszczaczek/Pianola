﻿namespace Pianola.MAUI.Drawables;

public class StaffDrawable : IDrawable
{
    private const float VerticalStaffMargin = 30; // TODO

    private const float SpaceHeight = 10; // TODO
    private const double StaffHeight = SpaceHeight * 5;

    private const double TrebleStaffTop = VerticalStaffMargin;
    private const double BassStaffTop = TrebleStaffTop + StaffHeight + VerticalStaffMargin;
    
    public const double Height = BassStaffTop + StaffHeight + VerticalStaffMargin;

    public static double TrebleStaffLineTop(int line) => TrebleStaffTop + SpaceHeight * (5 - line);
    public static double BassStaffLineTop(int line) => BassStaffTop + SpaceHeight * (5 - line);
    
    public void Draw(ICanvas canvas, RectF dirtyRect)
    {
        var trebleStaffBounds = DrawStaff(canvas, dirtyRect, VerticalStaffMargin);
        DrawStaff(canvas, dirtyRect, trebleStaffBounds.Bottom + VerticalStaffMargin);

        var bounds = dirtyRect;
        bounds.Bottom = (float) Height;
        canvas.DrawBounds(bounds);
    }

    private static RectF DrawStaff(ICanvas canvas, RectF dirtyRect, float top)
    {
        canvas.StrokeColor = Colors.Black;
        // canvas.StrokeSize = 1;

        var lineY = dirtyRect.Y = top;
        const int linesInStaff = 5;
        for (var i = 0; i < linesInStaff; i++)
        {
            canvas.DrawLine(x1: dirtyRect.X, y1: lineY, x2: dirtyRect.Width, y2: lineY);
            lineY += SpaceHeight;
        }

        var bounds = dirtyRect;
        bounds.Bottom = lineY;
        return bounds;
    }
}