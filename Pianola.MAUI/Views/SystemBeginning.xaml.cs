namespace Pianola.MAUI.Views;

public partial class SystemBeginning : HorizontalStackLayout
{
    public SystemBeginning()
    {
        InitializeComponent();
        Children.Add(new ClefView());
    }

    private void OnSizeChanged(object sender, EventArgs e)
    {
        
    }
}