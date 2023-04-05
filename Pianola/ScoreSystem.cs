using System;
using System.Windows;
using System.Windows.Media;

namespace Pianola;

public class ScoreSystem : FrameworkElement
{
    protected override void OnInitialized(EventArgs e)
    {
        // jak selektywnie dodawać wizualne informacje pomocnicze
        // if (Name == "System2") Score.SetShowVisualHelpers(this, true);
    }

    protected override void OnRender(DrawingContext drawingContext)
    {
        if (this.ShowVisualHelpers())  this.AddVisualHelpers(drawingContext);
    }
}