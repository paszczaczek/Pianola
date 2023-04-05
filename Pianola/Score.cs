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

        public static readonly DependencyProperty ShowVisualHelperProperty =
            DependencyProperty.RegisterAttached("ShowVisualHelper", typeof(bool), typeof(Score));

        public static bool GetShowVisualHelper(UIElement target) =>
            (bool) target.GetValue(ShowVisualHelperProperty);

        public static void SetShowVisualHelper(UIElement target, bool value) =>
            target.SetValue(ShowVisualHelperProperty, value);

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
            if (GetShowVisualHelper(this)) this.AddVisualHelpers(drawingContext);
        }
    }
}