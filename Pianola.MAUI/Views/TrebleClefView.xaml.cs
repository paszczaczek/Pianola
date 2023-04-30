namespace Pianola.MAUI.Views;

public partial class TrebleClefView : ContentView
{
    public TrebleClefView()
    {
        InitializeComponent();
    }
}

public class TrebleClefDrawable : SignDrawable
{
    public TrebleClefDrawable() : base("\x00c9", -7)
    {
    }
}