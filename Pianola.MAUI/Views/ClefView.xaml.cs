using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pianola.MAUI.Views;

public partial class ClefView //: GraphicsView
{
    public ClefView()
    {
        InitializeComponent();
    }

    private void OnStartInteraction(object sender, TouchEventArgs e)
    {
        Invalidate();
    }
}