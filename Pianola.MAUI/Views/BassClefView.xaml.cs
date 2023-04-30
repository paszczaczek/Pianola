namespace Pianola.MAUI.Views;

public partial class BassClefView : ContentView
{
    public BassClefView()
    {
        InitializeComponent();
    }
}

public class BassClefDrawable : SignDrawable
{
    public BassClefDrawable() : base("\x00c7", -55)
    {
    }
}