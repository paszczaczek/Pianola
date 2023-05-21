namespace Pianola.MAUI.Drawables;

public abstract class ViewResizer<TView> where TView : VisualElement
{
    public TView View { get; set; }

    protected abstract Rect CalculateBounds(ICanvas canvas, RectF dirtyRect);

    protected bool ResizeView(ICanvas canvas, RectF dirtyRect)
    {
        // czy po narysowaniu elementu zmieni się szerokość view?
        var bounds = CalculateBounds(canvas, dirtyRect);
        var widthChanged = Math.Abs(View.WidthRequest - bounds.Width) >= 0.01;
        
        // nie
        if (!widthChanged) return false;

        // tak, dostosuj szerokość view do szerokości elementu
        View.WidthRequest = bounds.Width;
        return true;
    }
}