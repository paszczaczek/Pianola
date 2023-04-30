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
}

public class FlatDrawable : SignDrawable
{
    public FlatDrawable() : base("\x003a", -43)
    {
    }
}