using Pianola.MAUI.Models;

namespace Pianola.MAUI.Views;

public partial class KeySignatureView //: GraphicsView
{
    public KeySignature Signature { get; set; }

    public static readonly BindableProperty SignatureProperty = BindableProperty.Create(
        nameof(Signature), typeof(KeySignature), typeof(KeySignatureView), 
        default, BindingMode.Default, null, (bindable, value, newValue) =>
        {
            throw new Exception("TU SKONCZYLEM!");
        });

    public KeySignatureView()
    {
        InitializeComponent();
    }

    private void OnStartInteraction(object sender, TouchEventArgs e)
    {
        Invalidate();
    }
}