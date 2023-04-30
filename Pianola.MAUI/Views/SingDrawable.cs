using Font = Microsoft.Maui.Graphics.Font;

namespace Pianola.MAUI.Views;

public class SignDrawable : IDrawable
{
    private readonly Font _font = new("feta26");
    private const float FontSize = 48;

    private readonly float _yShift;
    private readonly string _sign;
    
    public float BaseLine = 67.5f;

    // public const string TrebleClef = "\x00c9";
    // public const string BassClef = "\x00c7";
    // public const string Sharp = "\x002e";
    // public const string Flat = "\x003a";
    // public const string Natural = "\x0036";
    // private const string BlackNoteHead = "\x0056";
    // private const string WhiteNoteHead = "\x0055";

    public GraphicsView GraphicsView { get; set; }

    protected SignDrawable(string sign, float yShift)
    {
        _sign = sign;
        _yShift = yShift;
        BaseLine += _yShift;
    }

    public void Draw(ICanvas canvas, RectF dirtyRect)
    {
        // boundary
        canvas.StrokeColor = Colors.Violet;
        var signSize = canvas.GetStringSize(_sign, _font, FontSize);
        canvas.DrawRectangle(0, 0, signSize.Width, signSize.Height);
        
        // draw baseline
        canvas.StrokeColor = Colors.Violet;
        canvas.DrawLine(0, BaseLine, dirtyRect.Width, BaseLine);

        // draw sign
        canvas.FontColor = Colors.Black;
        canvas.Font = _font;
        canvas.FontSize = FontSize;
        canvas.DrawString(
            value: _sign,
            x: dirtyRect.X,
            y: dirtyRect.Y + _yShift,
            width: dirtyRect.Width,
            height: dirtyRect.Height,
            horizontalAlignment: HorizontalAlignment.Left,
            verticalAlignment: VerticalAlignment.Top,
            textFlow: TextFlow.OverflowBounds,
            lineSpacingAdjustment: 0);

        // resize GraphicsView
        GraphicsView.WidthRequest = signSize.Width + 0;
        GraphicsView.HeightRequest = signSize.Height + 2;
    }
}