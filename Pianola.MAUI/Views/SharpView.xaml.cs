namespace Pianola.MAUI.Views;

public partial class SharpView : ContentView
{
    public SharpView()
    {
        InitializeComponent();
    }

    private void OnStartInteraction(object sender, TouchEventArgs e)
    {
        var graphicsView = (GraphicsView) sender;
        graphicsView.Invalidate();
    }
}

public class SharpDrawable : SignDrawable
{
    public SharpDrawable() : base("\x002e", -49)
    {
    }
}