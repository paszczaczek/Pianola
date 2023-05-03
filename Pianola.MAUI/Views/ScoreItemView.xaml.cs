using System.ComponentModel;

namespace Pianola.MAUI.Views;

public partial class ScoreItemView //: GraphicsView
{
    public ScoreItemView()
    {
        InitializeComponent();
    }

    private void ScoreItemView_OnEndInteraction(object sender, TouchEventArgs e)
    {
        Invalidate();
    }
}

public class ScoreItemDrawable : IDrawable
{
    private const float SpaceHeight = 10; // TODO
    private float VerticalStaffMargin = 30; // TODO

    public string Item { get; set; }


    public void Draw(ICanvas canvas, RectF dirtyRect)
    {
        canvas.StrokeColor = Colors.Black;
        canvas.DrawRectangle(dirtyRect);

        canvas.FontColor = Colors.Black;
        // canvas.DrawString(nameof(ScoreItemDrawable), 0, 10, HorizontalAlignment.Left);
        canvas.DrawString(Item, 0, 10, HorizontalAlignment.Left);

        DrawStaffs(canvas, dirtyRect);
    }

    private void DrawStaffs(ICanvas canvas, RectF dirtyRect)
    {
        canvas.StrokeColor = Colors.Black;

        // treble staff
        var y = dirtyRect.Y + VerticalStaffMargin;
        for (var i = 0; i < 5; i++)
        {
            canvas.DrawLine(dirtyRect.X, y, dirtyRect.Width, y);
            y += SpaceHeight;
        }

        // bass staff
        y += VerticalStaffMargin * 2;
        for (var i = 0; i < 5; i++)
        {
            canvas.DrawLine(dirtyRect.X, y, dirtyRect.Width, y);
            y += SpaceHeight;
        }
    }
}