using Pianola.MAUI.Models;
using Font = Microsoft.Maui.Graphics.Font;

namespace Pianola.MAUI.Drawables;

public static class SignDrawable
{
#if ANDROID
    private static readonly Font Font = new("feta.ttf");
#else
    private static readonly Font Font = new("feta26");
#endif
    private const float FontBaseline = 67.5f;
    private const float FontSize = 48;

    private static readonly IReadOnlyDictionary<Sign, SignInfo> FontSignInfos =
        new Dictionary<Sign, SignInfo>
        {
            {Sign.TrebleClef, new(code: "\x00c9", ascender: 1249, descender: 654)},
            {Sign.BassClef, new(code: "\x00c7", ascender: 261, descender: 511)},
            {Sign.Sharp, new(code: "\x002e", ascender: 375, descender: 375)},
            {Sign.Flat, new(code: "\x003a", ascender: 470, descender: 157)},
            {Sign.Natural, new(code: "\x0036", ascender: 381, descender: 381)},
        };

    public static Rect Draw(ICanvas canvas, Point baselineLocation, Sign sign, bool calculateBoundsOnly = false)
    {
        var signInfo = FontSignInfos[sign];
        var bounds = CalculateBounds(canvas, baselineLocation, signInfo);
        if (calculateBoundsOnly) return bounds;

        canvas.DrawBounds(bounds);
        DrawBaseline(canvas, bounds, baselineLocation);
        DrawSign(canvas, baselineLocation, signInfo);
        return bounds;
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
        canvas.StrokeColor = Colors.BlueViolet;
        canvas.DrawLine(bounds.X, location.Y, bounds.X + bounds.Width, location.Y);
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

    private class SignInfo
    {
        public readonly string Code;
        public readonly double AscenderRatio;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="code"></param>
        /// <param name="ascender">Odczytany z FontForge, wyrażony w jego wewnętrznych współrzędnych.</param>
        /// <param name="descender">Odczytany z FontForge, wyrażony w jego wewnętrznych współrzędnych.</param>
        public SignInfo(string code, double ascender, double descender)
        {
            Code = code;
            AscenderRatio = ascender / (ascender + descender);
        }
    }
}