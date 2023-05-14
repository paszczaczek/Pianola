using Pianola.MAUI.Models;

namespace Pianola.MAUI.Views;

public partial class SystemBeginningView //: HorizontalStackLayout
{
    public KeySignature KeySignature
    {
        get => (KeySignature) GetValue(KeySignatureProperty);
        set => SetValue(KeySignatureProperty, value);
    }

    public static readonly BindableProperty KeySignatureProperty = BindableProperty.Create(
        nameof(KeySignature), typeof(KeySignature), typeof(SystemBeginningView),
        default, BindingMode.Default, null, (bindable, _, _) =>
        {
            // var sbv = (SystemBeginningView) bindable;
            // sbv.OnPropertyChanged(nameof(KeySignature));
        });

    public SystemBeginningView()
    {
        InitializeComponent();
        
        Children.Add(new ClefView());
        
        var ksv = new KeySignatureView();
        ksv.SetBinding(KeySignatureView.SignatureProperty,
            new Binding(nameof(KeySignature)) {Source = this});
        Children.Add(ksv);
    }

    private void OnSizeChanged(object sender, EventArgs e)
    {
    }
}