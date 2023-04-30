﻿// using Pianola.MAUI.Views;
// using Font = Microsoft.Maui.Graphics.Font;
//
// namespace Pianola.MAUI;
//
// // https://learn.microsoft.com/en-us/dotnet/maui/user-interface/controls/graphicsview?view=net-maui-7.0
// // https://learn.microsoft.com/en-us/dotnet/maui/user-interface/graphics/draw?view=net-maui-7.0
// public class GraphicsDrawable : IDrawable
// {
//     public void Draw(ICanvas canvas, RectF dirtyRect)
//     {
//         // define and measure a string
//         PointF stringLocation = new(0, 0);
//         const string stringText = SignViewLabel.TrebleClef;
//         Font font = new("feta26");
//         const float fontSize = 48;
//         var stringSize = canvas.GetStringSize(stringText, font, fontSize);
//         var stringRect = new RectF(stringLocation, stringSize);
//
//         // draw the string and its outline
//         canvas.StrokeColor = Colors.Black;
//         canvas.DrawRectangle(stringRect);
//         canvas.FontColor = Colors.Violet;
//         canvas.Font = font;
//         canvas.FontSize = fontSize - 1; // NOTE: reduce to prevent clipping
//         // canvas.FontSize = fontSize ; // NOTE: reduce to prevent clipping
//         canvas.DrawString(
//             value: stringText,
//             x: stringLocation.X,
//             y: stringLocation.Y,
//             width: stringSize.Width,
//             height: stringSize.Height,
//             horizontalAlignment: HorizontalAlignment.Left,
//             verticalAlignment: VerticalAlignment.Top,
//             textFlow: TextFlow.OverflowBounds,
//             lineSpacingAdjustment: 0);
//         
//         canvas.DrawRectangle(0, 0, stringSize.Width, stringSize.Height);
//     }      
// }