using Pianola.MAUI.Models;

namespace Pianola.MAUI.Drawables;

public class KeySignatureDrawable : IDrawable
{
    private Rect? _trebleBounds; // wyodrębnić do interface?
    private Rect? _bassBounds; // --||--

    public GraphicsView GraphicsView { get; set; }

    public void Draw(ICanvas canvas, RectF dirtyRect)
    {
        if (Resized(canvas)) return;

        SignDrawable.Draw(canvas, 1, SignModel.Sharp);
        // SignDrawable.Draw(canvas, TrebleClefBaseline, SignModel.TrebleClef);
        // SignDrawable.Draw(canvas, BassClefBaseline, SignModel.BassClef);
    }

    private bool Resized(ICanvas canvas)
    {
        // _trebleBounds ??= SignDrawable.CalculateBounds(canvas, TrebleClefBaseline, SignModel.TrebleClef);
        // _bassClefBounds ??= SignDrawable.CalculateBounds(canvas, BassClefBaseline, SignModel.BassClef);

        // var width = Math.Max(_trebleClefBounds.Value.Width, _bassClefBounds.Value.Width);
        // var resized = Math.Abs(GraphicsView.WidthRequest - width) >= 0.01;
        // if (resized) GraphicsView.WidthRequest = width + 0;

        // return resized;
        return false;
    }
}