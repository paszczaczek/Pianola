using Pianola.MAUI.Models;

namespace Pianola.MAUI.Views;

public partial class ScoreView //: ContentView
{
    public ScoreView()
    {
        InitializeComponent();
        for (var i = 0; i < 10; i++)
            Measures.Add(new MeasureView {BindingContext = new MeasureModel {DebugText = $"Measure {i}"}});
    }

    private void AccomodateScoreToItems()
    {
        AccomodateScoreLayout<Staffs>(Staffs);
        AccomodateScoreLayout<SystemBeginning>(SystemsBeginnings);

        void AccomodateScoreLayout<TView>(Layout layout) where TView : View, new()
        {
            var systemCount = Measures.Children.Cast<View>().GroupBy(v => v.Y).Count();
            if (layout.Count == systemCount) return;
            if (layout.Count < systemCount)
                for (var i = layout.Count; i < systemCount; i++)
                    layout.Add(new TView());
            if (layout.Count == systemCount) return;
            for (var i = systemCount; i < layout.Count; i++)
                layout.RemoveAt(0);
        }
    }

    private void Measures_OnSizeChanged(object sender, EventArgs e)
    {
        AccomodateScoreToItems();
    }
}