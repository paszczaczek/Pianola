using System.Windows;
using System.Windows.Controls;

namespace Pianola;

public class Staff : Grid
{
    #region ClefTypeProperty

    public static readonly DependencyProperty ClefTypeProperty = DependencyProperty.Register(
        nameof(ClefType), typeof(Clef.Type), typeof(Staff),
        new FrameworkPropertyMetadata(
            Clef.Type.Treble,
            FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsRender,
            propertyChangedCallback: (d, e) =>
            {
                // ustawiono typ pięciolinii (wiolinowa/basowa) - dodaj do pięciolinii właściwy klucz
                var staff = (Staff) d;
                var newClefType = (Clef.Type) e.NewValue;
                staff._stackPanel.Children.Remove(staff._clef);
                staff._clef = Clef.Create(newClefType);
                staff._stackPanel.Children.Add(staff._clef);
            }));

    public Clef.Type ClefType
    {
        get => (Clef.Type) GetValue(ClefTypeProperty);
        set => SetValue(ClefTypeProperty, value);
    }

    #endregion


    private readonly StaffLines _staffLines;
    private Clef _clef;
    private readonly StackPanel _stackPanel;

    public Staff()
    {
        /*
         * Grid
         *      StaffLines
         *      StackPanel
         *          Clef
         *          Scale
         *          Metrum TODO
         */
        
        // utworz pięciolinię
        _staffLines = new StaffLines
        {
            VerticalAlignment = VerticalAlignment.Top
        };
        
        // utwórz klucz
        _clef = new TrebleClef();

        // utwórz stack panel i odaj do niego niego klucz, skalę i metrum a później takty
        _stackPanel = new StackPanel {Orientation = Orientation.Horizontal};
        _stackPanel.Children.Add(_clef);
        _stackPanel.Children.Add(new CesScale());
        _stackPanel.Children.Add(new CisScale());
        // TODO dodaj metrum

        // dodaj do grida pięciolinię a nad nią stack panel z elementami
        Children.Add(_staffLines);
        Children.Add(_stackPanel);
    }

    protected override void OnRenderSizeChanged(SizeChangedInfo sizeInfo)
    {
        base.OnRenderSizeChanged(sizeInfo);
        if (!sizeInfo.WidthChanged) return;
        _staffLines.Width = sizeInfo.NewSize.Width;
    }

    private static double SpaceHeight => Sign.HeadHeight;
    public static double TopOf(Line staffLine) => (int) staffLine * SpaceHeight;
    public static double TopOf(Space staffSpace) => (int) staffSpace * SpaceHeight + SpaceHeight / 2;

    public enum Line
    {
        Fifth = 0,
        Fourth = 1,
        Third = 2,
        Second = 3,
        First = 4
    }

    public enum Space
    {
        FirstAbove = -1,
        
        Fourth = 0,
        Third = 1,
        Second = 2,
        First = 3
    }
}