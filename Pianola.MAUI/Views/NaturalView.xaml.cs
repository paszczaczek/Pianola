namespace Pianola.MAUI.Views;

public partial class NaturalView : ContentView
{
    public NaturalView()
    {
        InitializeComponent();
    }
    private void OnStartInteraction(object sender, TouchEventArgs e)
    {
        var graphicsView = (GraphicsView)sender;
        graphicsView.Invalidate();
    }
}

public class NaturalDrawable : SignDrawable
{
    public NaturalDrawable() : base("\x0036", -49)
    {
    }
}