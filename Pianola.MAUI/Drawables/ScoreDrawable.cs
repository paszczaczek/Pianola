namespace Pianola.MAUI.Drawables;

public class ScoreDrawable : IDrawable
{
    public void Draw(ICanvas canvas, RectF dirtyRect)
    {
        SystemDrawable.CalculateStaffSpaceHeight(canvas);
    }
}