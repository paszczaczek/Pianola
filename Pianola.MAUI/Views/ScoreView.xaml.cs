namespace Pianola.MAUI.Views;

public partial class ScoreView //: ContentView
{
    public ScoreView()
    {
        InitializeComponent();
        FlexLayout.Add(new ScoreItemView {Drawable = new ScoreItemDrawable() {Item = "Measure 1"}});
        FlexLayout.Add(new ScoreItemView {Drawable = new ScoreItemDrawable() {Item = "Measure 2"}});
        FlexLayout.Add(new ScoreItemView {Drawable = new ScoreItemDrawable() {Item = "Measure 3"}});
        FlexLayout.Add(new ScoreItemView {Drawable = new ScoreItemDrawable() {Item = "Measure 4"}});
        FlexLayout.Add(new ScoreItemView {Drawable = new ScoreItemDrawable() {Item = "Measure 5"}});
        FlexLayout.Add(new ScoreItemView {Drawable = new ScoreItemDrawable() {Item = "Measure 6"}});
        FlexLayout.Add(new ScoreItemView {Drawable = new ScoreItemDrawable() {Item = "Measure 7"}});
        FlexLayout.Add(new ScoreItemView {Drawable = new ScoreItemDrawable() {Item = "Measure 8"}});
        FlexLayout.Add(new ScoreItemView {Drawable = new ScoreItemDrawable() {Item = "Measure 9"}});
        FlexLayout.Add(new ScoreItemView {Drawable = new ScoreItemDrawable() {Item = "Measure 10"}});
    }

    private void CompleteBeginningsOfStaffs()
    {
        var systemCount = FlexLayout.Children.Cast<View>().GroupBy(v => v.Y).Count();
        if (Clefs.Count == systemCount) return;
        if (Clefs.Count < systemCount)
            for (var i = Clefs.Count; i < systemCount; i++)
                Clefs.Add(new ScoreItemView {Drawable = new ScoreItemDrawable() {Item = "Clef"}});
        if (Clefs.Count == systemCount) return;
        for (var i = systemCount; i < Clefs.Count; i++)
            Clefs.RemoveAt(0);
    }
    
    private void FlexLayout_OnSizeChanged(object sender, EventArgs e)
    {
        CompleteBeginningsOfStaffs();
    }

    private void GraphicsView_OnEndInteraction(object sender, TouchEventArgs e)
    {
        
    }
}

public class ScoreDrawable : IDrawable
{
    public void Draw(ICanvas canvas, RectF dirtyRect)
    {
        // canvas.SaveState();

        canvas.FillColor = Colors.Violet;
        canvas.FillRectangle(dirtyRect);
        canvas.StrokeColor = Colors.BlueViolet;
        canvas.DrawRectangle(dirtyRect);

        canvas.FontColor = Colors.Black;
        canvas.DrawString(nameof(ScoreDrawable), 0, dirtyRect.Bottom - 10, HorizontalAlignment.Left);

        canvas.StrokeColor = Colors.Black;
        canvas.DrawLine(60, 100, 180, 100);

        // canvas.RestoreState();
        // canvas.ResetState();
    }
}