namespace Pianola.MAUI.Views;

public partial class SharpView : ContentView
{
    public SharpView()
    {
        InitializeComponent();
    }
}

public class SharpDrawable : SignDrawable
{
    public SharpDrawable() : base("\x002e", -44.5f)
    {
    }
}