using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Pianola
{
    // [ContentProperty("Systems")]
    public class Score : ContentControl
    {
        // public static readonly DependencyProperty VisualHelperProperty =
        //     DependencyProperty.Register(nameof(VisualHelper), typeof(bool), typeof(Score));
        //
        // public bool VisualHelper
        // {
        //     get => (bool) GetValue(VisualHelperProperty);
        //     set => SetValue(VisualHelperProperty, value);
        // }

        #region ShowVisualHelperProperty

        public static readonly DependencyProperty ShowVisualHelpersProperty =
            DependencyProperty.RegisterAttached("ShowVisualHelpers", typeof(bool?), typeof(Score));

        public static bool? GetShowVisualHelpers(UIElement target) =>
            (bool?) target.GetValue(ShowVisualHelpersProperty);

        public static void SetShowVisualHelpers(UIElement target, bool? value) =>
            target.SetValue(ShowVisualHelpersProperty, value);

        #endregion

        private static readonly Thickness SystemMargin = new(15);
        private readonly ICollection<ScoreSystem> _systems = new Collection<ScoreSystem>();

        public Score()
        {
            _systems.Add(new() {Name = "System1", Height = 90});
            _systems.Add(new() {Name = "System2", Height = 90});

            var stackPanel = new StackPanel()
            {
                Margin = SystemMargin
            };
            foreach (var system in _systems) stackPanel.Children.Add(system);

            Content = stackPanel;
        }

        protected override void OnRender(DrawingContext drawingContext)
        {
            if (GetShowVisualHelpers(this) is true) this.AddVisualHelpers(drawingContext);
        }
    }
}