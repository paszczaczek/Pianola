using Pianola.MAUI.Models;
using Font = Microsoft.Maui.Graphics.Font;

namespace Pianola.MAUI.Drawables;

public static class SignDrawable
{
    private static readonly Font Font = new("feta26");
    private const float FontBaseline = 67.5f;
    private const float FontSize = 48;

    private static readonly IReadOnlyDictionary<SignModel, SignInfo> FontSignInfos =
        new Dictionary<SignModel, SignInfo>
        {
            {SignModel.TrebleClef, new(code: "\x00c9", ascender: 1249, descender: 654)},
            {SignModel.BassClef, new(code: "\x00c7", ascender: 261, descender: 511)},
            {SignModel.Sharp, new(code: "\x002e", ascender: 375, descender: 375)},
            {SignModel.Flat, new(code: "\x003a", ascender: 470, descender: 157)},
            {SignModel.Natural, new(code: "\x0036", ascender: 381, descender: 381)},
        };

    public static void Draw(ICanvas canvas, double top, SignModel signModel)
    {
        var location = new Point(0, top);
        Draw(canvas, location, signModel);
    }

    private static void Draw(ICanvas canvas, Point location, SignModel signModel)
    {
        var signInfo = FontSignInfos[signModel];
        var bounds = CalculateBounds(canvas, location, signInfo);
        canvas.DrawBounds(bounds);
        DrawBaseline(canvas, bounds, location);
        DrawSign(canvas, location, signInfo);
    }

    public static Rect CalculateBounds(ICanvas canvas, double top, SignModel signModel)
    {
        var signInfo = FontSignInfos[signModel];
        var location = new Point(0, top);
        return CalculateBounds(canvas, location, signInfo);
    }

    private static Rect CalculateBounds(ICanvas canvas, Point location, SignInfo signInfo)
    {
        var blackBoxSize = canvas.GetStringSize(signInfo.Code, Font, FontSize);
        var bounds = new Rect(
            x: location.X,
            y: location.Y - blackBoxSize.Height * signInfo.AscenderRatio,
            width: blackBoxSize.Width,
            height: blackBoxSize.Height
        );

        return bounds;
    }

    private static void DrawBaseline(ICanvas canvas, RectF bounds, PointF location)
    {
        canvas.StrokeColor = Colors.Violet;
        canvas.DrawLine(0, location.Y, bounds.Width, location.Y);
    }

    private static void DrawSign(ICanvas canvas, PointF location, SignInfo signInfo)
    {
        canvas.FontColor = Colors.Black;
        canvas.Font = Font;
        canvas.FontSize = FontSize;
        canvas.DrawString(
            value: signInfo.Code,
            x: location.X,
            y: location.Y - FontBaseline,
            width: 200,
            height: 200,
            horizontalAlignment: HorizontalAlignment.Left,
            verticalAlignment: VerticalAlignment.Top,
            textFlow: TextFlow.OverflowBounds,
            lineSpacingAdjustment: 0);
    }
}