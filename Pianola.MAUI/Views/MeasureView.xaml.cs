using System.ComponentModel;
using Pianola.MAUI.Drawables;

namespace Pianola.MAUI.Views;

public partial class MeasureView //: GraphicsView
{
    public MeasureView()
    {
        InitializeComponent();
    }

    private void OnEndInteraction(object sender, TouchEventArgs e)
    {
        Invalidate();
    }
}