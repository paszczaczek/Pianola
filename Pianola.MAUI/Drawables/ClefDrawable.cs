using Pianola.MAUI.Models;

namespace Pianola.MAUI.Drawables;

public class ClefDrawable : IDrawable
{
    private const int TrebleClefLocatesG4OnLine = 2;
    private const int BassClefLocatesF3OnLine = 4;

    private static readonly double TrebleClefBaseline = StaffDrawable.TrebleStaffLineTop(TrebleClefLocatesG4OnLine);
    private static readonly double BassClefBaseline = StaffDrawable.BassStaffLineTop(BassClefLocatesF3OnLine);

    private Rect? _trebleClefBounds; 
    private Rect? _bassClefBounds; 
    
    public GraphicsView GraphicsView { get; set; }

    public void Draw(ICanvas canvas, RectF dirtyRect)
    {
        if (Resized(canvas)) return;

        SignDrawable.Draw(canvas, TrebleClefBaseline, SignModel.TrebleClef);
        SignDrawable.Draw(canvas, BassClefBaseline, SignModel.BassClef);
    }

    private bool Resized(ICanvas canvas)
    {
        _trebleClefBounds ??= SignDrawable.CalculateBounds(canvas, TrebleClefBaseline, SignModel.TrebleClef);
        _bassClefBounds ??= SignDrawable.CalculateBounds(canvas, BassClefBaseline, SignModel.BassClef);

        var width = Math.Max(_trebleClefBounds.Value.Width, _bassClefBounds.Value.Width);
        var resized = Math.Abs(GraphicsView.WidthRequest - width) >= 0.01;
        if (resized) GraphicsView.WidthRequest = width + 0;

        return resized;
    }
}