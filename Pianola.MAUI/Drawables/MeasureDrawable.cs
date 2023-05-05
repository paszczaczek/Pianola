using Pianola.MAUI.Models;

namespace Pianola.MAUI.Drawables;

public class MeasureDrawable : BindableObject, IDrawable
{
    public GraphicsView GraphicsView { get; set; }

    public void Draw(ICanvas canvas, RectF dirtyRect)
    {
        var model = (MeasureModel) GraphicsView.BindingContext;
        
        canvas.DrawBounds(dirtyRect);

        canvas.FontColor = Colors.Black;
        canvas.DrawString(model.DebugText, 0, 10, HorizontalAlignment.Left);

        GraphicsView.WidthRequest = GraphicsView.MaximumWidthRequest = 100; // TODO
    }
}