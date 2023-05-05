namespace Pianola.MAUI.Drawables;

public static class CanvasExtensions
{
    public static void DrawBounds(this ICanvas canvas, RectF dirtyRect)
    {
        canvas.StrokeColor = Colors.BlueViolet;
        canvas.DrawRectangle(dirtyRect);
    }
}

