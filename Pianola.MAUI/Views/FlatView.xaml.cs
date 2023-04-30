using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pianola.MAUI.Views;

public partial class FlatView : ContentView
{
    public FlatView()
    {
        InitializeComponent();
    }

    private void OnStartInteraction(object sender, TouchEventArgs e)
    {
        var graphicsView = (GraphicsView)sender;
        graphicsView.Invalidate();
    }
}

public class FlatDrawable : SignDrawable
{
    public FlatDrawable() : base("\x003a", -45)
    {
    }
}