namespace Pianola.MAUI.Views;

public partial class SignView : ContentView
{
    public string FamilyName { get; } = "feta26";
    public double FontSize { get; } = 48;
    // TODO
    // W WPF wyznaczałem HeadHeight i BaseLine na przy pomocy FormattedText(). W MaUI ma tej funkcji.
    // Przepiszę wartość wyliczoną na WPF do czasu aż dowiem się jak to wyznaczyć z fontu.
    // Tera, jeśli zmienię FontSize, to HeadHeight i BaseLine będą niewłaściwe.
    public double HeadHeight { get; } = 15.056000000000005;
    public double BaseLine { get; private set; } = 60.951999999999998;

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
        
    }

    private static readonly BindableProperty TextProperty = BindableProperty.Create(
        nameof(Text), typeof(string), typeof(SignView), string.Empty);

    public string Text
    {
        get => (string) GetValue(TextProperty);
        set => SetValue(TextProperty, value);
    }
}