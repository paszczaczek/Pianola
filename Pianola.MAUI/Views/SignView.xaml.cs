using System.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Font = Microsoft.Maui.Graphics.Font;

namespace Pianola.MAUI.Views;

public partial class SignView : ContentView
{
    public SignView()
    {
        InitializeComponent();
    }

    private void OnMoveHoverInteraction(object sender, TouchEventArgs e)
    {
        var graphicsView = (GraphicsView) sender;
        var drawable = (SignViewDrawable) graphicsView.Drawable;
    }
}


public class SignViewDrawable : IDrawable
{
    private readonly Font _font = new("feta26");
    private const float FontSize = 48;

    // private const float BaseLine = 60.951999999999998f;
    private const float BaseLine = 65.663333333333341f;

    private const string TrebleClef = "\x00c9";
    public const string BassClef = "\x00c7";
    public const string Sharp = "\x002e";
    public const string Flat = "\x003a";
    public const string Natural = "\x0036";
    private const string BlackNoteHead = "\x0056";
    private const string WhiteNoteHead = "\x0055";

    public GraphicsView GraphicsView { get; set; }

    public void Draw(ICanvas canvas, RectF dirtyRect)
    {
        // const string sign = SignViewLabel.BlackNoteHead;
        const string sign = TrebleClef + BlackNoteHead + WhiteNoteHead;

        canvas.FontColor = Colors.Black;
        canvas.Font = _font;
        canvas.FontSize = FontSize; // - 1; // NOTE: reduce to prevent clipping
        canvas.DrawString(
            value: sign,
            x: dirtyRect.X,
            y: dirtyRect.Y, // - BaseLine,
            width: dirtyRect.Width,
            height: dirtyRect.Height,
            horizontalAlignment: HorizontalAlignment.Left,
            verticalAlignment: VerticalAlignment.Top,
            textFlow: TextFlow.OverflowBounds,
            lineSpacingAdjustment: 0);

        canvas.StrokeColor = Colors.Violet;
        canvas.DrawLine(0, BaseLine, dirtyRect.Width, BaseLine);

        canvas.StrokeColor = Colors.Black;
        var stringSize = canvas.GetStringSize(sign, _font, FontSize);
        canvas.DrawRectangle(0, 0, stringSize.Width, stringSize.Height);
       
        GraphicsView.WidthRequest = stringSize.Width;
        GraphicsView.HeightRequest = stringSize.Height;
    }
}