﻿namespace Pianola.MAUI.Views;

public partial class SignView : ContentView
{
    public string FamilyName { get; } = "feta26";

    public double FontSize { get; } = 48;

    // TODO
    // W WPF wyznaczałem HeadHeight na przy pomocy FormattedText().
    // W MaUI nie wiem jak to wyznaczyć. Przepiszę wartość wyliczoną na WP;
    // Jeśli zmienię FontSize, to HeadHeight będzie niewłaściwa.
    // Nie wiadomo tez jak to zachowa się na androidzie.
    public double HeadHeight { get; } = 15.056000000000005;

    public const string TrebleClef = "\x00c9";
    public const string BassClef = "\x00c7";
    public const string Sharp = "\x002e";
    public const string Flat = "\x003a";
    public const string Natural = "\x0036";
    public const string BlackNoteHead = "\x0056";
    public const string WhiteNoteHead = "\x0055";

    public SignView()
    {
        InitializeComponent();
        // https://swharden.com/blog/2021-10-16-maui-graphics-measurestring/
        // SizeF f;
        // (Content as Label).FontFamily.Get
    }

    private static readonly BindableProperty TextProperty = BindableProperty.Create(
        nameof(Glyph), typeof(string), typeof(SignView), string.Empty);

    public string Glyph
    {
        get => (string) GetValue(TextProperty);
        set => SetValue(TextProperty, value);
    }
}