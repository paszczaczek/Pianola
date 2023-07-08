using Pianola.MAUI.Models;
using Pianola.MAUI.Views;

namespace Pianola.MAUI.Drawables;

public class MeasureDrawable : ViewResizer<MeasureView>, IDrawable
{
    public void Draw(ICanvas canvas, RectF dirtyRect)
    {
        if (ViewResized(canvas, dirtyRect)) return;
        DrawMeasure(canvas, dirtyRect);
    }

    protected override Rect CalculateBounds(ICanvas canvas, RectF dirtyRect)
    {
        return DrawMeasure(canvas, dirtyRect, calculateBoundsOnly: true);
    }

    private Rect DrawMeasure(ICanvas canvas, RectF dirtyRect, bool calculateBoundsOnly = false)
    {
        var bounds = new Rect(0, 0, 100, SystemDrawable.Height); // TODO
        if (calculateBoundsOnly) return bounds;

        var model = (MeasureModel) View.BindingContext;
        canvas.DrawBounds(dirtyRect);
        canvas.FontColor = Colors.Black;
        canvas.DrawString(model.DebugText, 0, 10, HorizontalAlignment.Left);
        
        return bounds;
    }
}