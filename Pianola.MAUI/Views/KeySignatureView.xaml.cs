using Pianola.MAUI.Models;

namespace Pianola.MAUI.Views;

/// <remarks>
/// https://simple.wikipedia.org/wiki/Key_signature
/// https://en.wikipedia.org/wiki/Key_signature
/// </remarks>
public partial class KeySignatureView //: GraphicsView
{
    public static readonly BindableProperty SignatureProperty = BindableProperty.Create(
        nameof(Signature), typeof(KeySignature), typeof(KeySignatureView), 
        default, BindingMode.Default, null, (bindable, value, newValue) =>
        {
            var thisView = (KeySignatureView) bindable;
            thisView.Invalidate();
        });

    public KeySignature Signature
    {
        get => (KeySignature)GetValue(SignatureProperty);
        set => SetValue(SignatureProperty, value);
    }

    public KeySignatureView()
    {
        InitializeComponent();
    }

    private void OnStartInteraction(object sender, TouchEventArgs e)
    {
        Invalidate();
    }
}