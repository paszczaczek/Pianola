using CommunityToolkit.Mvvm.ComponentModel;
using Pianola.MAUI.Models;

namespace Pianola.MAUI.Views;

[ObservableObject]
public partial class ScoreView //: ContentView
{
    [ObservableProperty] private KeySignature _keySignature = new KeySignature(7, ChromaticSign.Flat);

    public ScoreView()
    {
        InitializeComponent();
        for (var i = 0; i < 10; i++)
            Measures.Add(new MeasureView {BindingContext = new MeasureModel {DebugText = $"Measure {i}"}});
    }

    private void AccomodateScoreToItems()
    {
        AccomodateScoreLayout(Systems, () => new SystemView());
        AccomodateScoreLayout(SystemsBeginnings, viewConstructor: () =>
        {
            var sbv = new SystemBeginningView();
            sbv.SetBinding(
                SystemBeginningView.KeySignatureProperty,
                new Binding(nameof(Models.KeySignature)) {Source = this});
            return sbv;
        });

        void AccomodateScoreLayout<TView>(Layout layout, Func<TView> viewConstructor) where TView : View, new()
        {
            var systemCount = Measures.Children.Cast<View>().GroupBy(v => v.Y).Count();
            if (layout.Count == systemCount) return;
            if (layout.Count < systemCount)
                for (var i = layout.Count; i < systemCount; i++)
                    layout.Add(viewConstructor());
            if (layout.Count == systemCount) return;
            for (var i = systemCount; i < layout.Count; i++)
                layout.RemoveAt(0);
        }
    }

    private void Measures_OnSizeChanged(object sender, EventArgs e)
    {
        // Przy maksymalizacji okna ten event jest inaczej wołany niż przy zmianie rozmiaru okna:
        // - przy zmianie rozmiaru okna event wołany jest _po_ wyliczeniu nowych położeń elementów, 
        // - przy maksymalizacji okna _przed_ wyliczeniem nowych położeń elementów.
        // Powoduje to, że przy maksymalizacji okna początki systemów (klucze, sygnatury przykluczowe, itd)
        // nie są uaktualniane/
        AccomodateScoreToItems();
    }

    private void OnTapped(object sender, TappedEventArgs e)
    {
    }
}