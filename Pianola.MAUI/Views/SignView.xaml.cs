using Font = Microsoft.Maui.Graphics.Font;

namespace Pianola.MAUI.Views;

public partial class SignView //: ContentView
{
    public SignView()
    {
        BindingContext = this;
        InitializeComponent();
    }

    public Sign Sign
    {
        get => (Sign) GetValue(SignProperty);
        set => SetValue(SignProperty, value);
    }

    public static readonly BindableProperty SignProperty = BindableProperty.Create(
        nameof(Sign), typeof(Sign), typeof(SignView), null, BindingMode.Default);

    private void OnStartInteraction(object sender, TouchEventArgs e)
    {
        // var graphicsView = (GraphicsView) sender;
        // graphicsView.Invalidate();
    }
}

public enum Sign
{
    TrebleClef,
    BassClef,
    Sharp,
    Flat,
    Natural,
    BlackNoteHead,
    WhiteNoteHead
}

public class SignDrawable : BindableObject, IDrawable
{
    private readonly Font _font = new("feta26");
    private const float FontSize = 48;
    private const double FontBaseLine = 67.5;
    public double BaseLine => FontBaseLine - Glyph.moveUp;

    public GraphicsView GraphicsView { get; set; }

    public Sign Sign
    {
        get => (Sign) GetValue(SignProperty);
        set => SetValue(SignProperty, value);
    }

    public static readonly BindableProperty SignProperty = BindableProperty.Create(
        nameof(Sign), typeof(Sign), typeof(SignDrawable), defaultValue: null, BindingMode.Default,
        validateValue: null,
        propertyChanged: (bindable, _, _) => ((SignDrawable) bindable).GraphicsView.Invalidate());

    private (string text, double moveUp) Glyph =>
        Sign switch
        {
            Sign.TrebleClef => ("\x00c9", 7),
            Sign.BassClef => ("\x00c7", 55),
            Sign.Sharp => ("\x002e", 49),
            Sign.Flat => ("\x003a", 45),
            Sign.Natural => ("\x0036", 49),
            Sign.BlackNoteHead => ("\x0056", 0 /*TODO*/),
            Sign.WhiteNoteHead => ("\x0055", 0 /*TODO*/),
            _ => throw new ArgumentOutOfRangeException()
        };

    public void Draw(ICanvas canvas, RectF dirtyRect)
    {
        // calculate boundary
        var glyph = Glyph;
        var signSize = canvas.GetStringSize(glyph.text, _font, FontSize);

        // resize boundary
        var originalWidth = GraphicsView.WidthRequest;
        var originalHeight = GraphicsView.HeightRequest;
        GraphicsView.WidthRequest = signSize.Width + 0;
        GraphicsView.HeightRequest = signSize.Height + 2;

        // if boundry resized stop drawing - resizing will fire yet another Draw()
        var resized = Math.Abs(GraphicsView.WidthRequest - originalWidth) > 0.01 ||
                      Math.Abs(GraphicsView.HeightRequest - originalHeight) > 0.01;
        if (resized) return;

        // draw boundary
        canvas.StrokeColor = Colors.Violet;
        canvas.DrawRectangle(0, 0, signSize.Width, signSize.Height);

        // draw baseline
        canvas.StrokeColor = Colors.Violet;
        canvas.DrawLine(0, (float) BaseLine, dirtyRect.Width, (float) BaseLine);

        // draw glyph
        canvas.FontColor = Colors.Black;
        canvas.Font = _font;
        canvas.FontSize = FontSize;
        canvas.DrawString(
            value: glyph.text,
            x: dirtyRect.X,
            y: dirtyRect.Y - (float) glyph.moveUp,
            width: dirtyRect.Width,
            height: dirtyRect.Height,
            horizontalAlignment: HorizontalAlignment.Left,
            verticalAlignment: VerticalAlignment.Top,
            textFlow: TextFlow.OverflowBounds,
            lineSpacingAdjustment: 0);
    }
}