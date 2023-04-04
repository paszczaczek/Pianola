using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace Pianola
{
    // [ContentProperty("Systems")]
    public class Score : ContentControl
    {
        private readonly ICollection<ScoreSystem> _systems = new Collection<ScoreSystem>();
        public static readonly Thickness SystemMargin = new(5);

        public Score()
        {
            _systems.Add(new() {Name = "System1", Height = 100});
            _systems.Add(new() {Name = "System2", Height = 100});

            var stackPanel = new StackPanel()
            {
                Margin = SystemMargin
            };
            foreach (var system in _systems) stackPanel.Children.Add(system);

            Content = stackPanel;
        }
    }
}