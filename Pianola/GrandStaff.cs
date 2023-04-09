using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Pianola.Clefs;

namespace Pianola;

public class GrandStaff : StackPanel
{
    private readonly Staff _trebleStaff = new Staff {ClefType = Clef.Type.Treble};
    private readonly Staff _bassStaff = new Staff {ClefType = Clef.Type.Bass};

    public GrandStaff()
    {
        _trebleStaff.Background = Brushes.Khaki;
        _bassStaff.Background = Brushes.Bisque;

        Children.Add(_trebleStaff);
        Children.Add(_bassStaff);

        Height = _trebleStaff.Height + _bassStaff.Height;
    }

    protected override void OnInitialized(EventArgs e)
    {
        // jak selektywnie dodawać wizualne informacje pomocnicze
        // if (Name == "System2") Score.SetShowVisualHelpers(this, true);
    }

    // protected override void OnRender(DrawingContext dc)
    // {
    //     // if (this.ShowVisualHelpers())  this.AddVisualHelpers(dc);
    //     this.AddVisualHelpers(dc);
    //     base.OnRender(dc);
    // }
}