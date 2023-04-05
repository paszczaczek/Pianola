using System;
using System.Windows;
using System.Windows.Media;

namespace Pianola;

public class ScoreSystem : FrameworkElement
{
    protected override void OnInitialized(EventArgs e)
    {
        // jak selektywnie dodawać wizualne informacje pomocnicze
        if (Name == "System2") Score.SetShowVisualHelper(this, true);
    }

    protected override void OnRender(DrawingContext drawingContext)
    {
        if (!Score.GetShowVisualHelper(this)) return;
        
        var score = this.FindVisualParent<Score>();
        if (!Score.GetShowVisualHelper(score)) return;
        
        this.AddVisualHelpers(drawingContext);
    }
}