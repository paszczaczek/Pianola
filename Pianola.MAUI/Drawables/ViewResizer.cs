namespace Pianola.MAUI.Drawables;

public abstract class ViewResizer<TView> where TView : VisualElement
{
    public TView View { get; set; }
    protected Rect Bounds { get; private set; }

    protected bool ViewResized(ICanvas canvas, RectF dirtyRect, Property property = Property.Both)
    {
        // czy po narysowaniu elementu zmieni się szerokość view?
        Bounds = CalculateBounds(canvas, dirtyRect);
        var widthChanged = Math.Abs(View.WidthRequest - Bounds.Width) >= 0.01;
        var heightChanged = Math.Abs(View.HeightRequest - Bounds.Height) >= 0.01;

        switch (property)
        {
            case Property.Width:
                if (!widthChanged) return false;
                View.WidthRequest = Bounds.Width;
                break;
            case Property.Height:
                if (!heightChanged) return false;
                View.HeightRequest = Bounds.Height;
                break;
            case Property.Both:
                if (!widthChanged && !heightChanged) return false;
                View.WidthRequest = Bounds.Width;
                View.HeightRequest = Bounds.Height;
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(property), property, null);
        }

        return true;
    }

    protected abstract Rect CalculateBounds(ICanvas canvas, RectF dirtyRect);

    protected enum Property
    {
        Width,
        Height,
        Both
    }
}