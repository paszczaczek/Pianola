namespace Pianola.MAUI.Drawables;

public class StaffDrawable : IDrawable
{
    private const float SpaceHeight = 20; // TODO
    
    public void Draw(ICanvas canvas, RectF dirtyRect)
    {
        for (var i = 0; i < 5; i++)
        {
            var y = dirtyRect.Y + SpaceHeight * i;
            canvas.StrokeColor = Colors.Black;
            canvas.DrawLine(
                dirtyRect.X, y,
                dirtyRect.Width, y);
        }
    }
}