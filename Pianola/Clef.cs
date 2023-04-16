using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Data;

namespace Pianola;

/// <remarks>
/// Struktura elementu:
/// <code>
///     Canvas
///         Sign
/// </code>
/// </remarks>
public class Clef : CustomizedCanvas
{
    // typy kluczy
    public enum Types
    {
        Treble,
        Bass
    }

    // opis typów kluczy
    private static readonly IReadOnlyDictionary<Types, Description> Descriptions = new Dictionary<Types, Description>
    {
        {Types.Treble, new(Sign.TrebleClef, Staff.Position.SecondLine, Pitch.Names.G, Octave.Names.OneLined)},
        {Types.Bass, new(Sign.BassClef, Staff.Position.FourthLine, Pitch.Names.F, Octave.Names.Small)},
    };

    private record struct Description(
        string SignText,
        Staff.Position StaffPosition,
        Pitch.Names PitchName,
        Octave.Names OctaveName);

    private Sign Sign => Children.OfType<Sign>().First();

    public Clef()
    {
        // dodaj do płótna znak klucza
        var clefSign = new Sign();
        Children.Add(clefSign);

        // ustaw typ klucza na domyślny (wiolinowy)
        SetType(Types.Treble);

        // płótno ma dostosowywać swoją szerokość do szerokości klucza
        SetBinding(WidthProperty, new Binding(nameof(Width)) {Source = clefSign});

        // a wysokość do wysokości pięciolinii
        Height = Staff.LinesHeight;
    }

    private void SetType(Types type)
    {
        var desc = Descriptions[type];

        // ustaw właściwy znak klucza
        var sign = Sign;
        sign.Text = desc.SignText;

        // przesuń znak klucza na pozycję odpowiedniej linii w pięciolinii
        var top = Staff.TopOf(desc.StaffPosition);
        SetTop(sign, -sign.BaseLine + top);
    }

    #region TypeProperty

    public static readonly DependencyProperty TypeProperty = DependencyProperty.Register(
        nameof(Type), typeof(Types), typeof(Clef),
        new FrameworkPropertyMetadata(
            default(Types),
            FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.AffectsMeasure,
            TypePropertyChangedCallback));

    private static void TypePropertyChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        // ustawiono typ klucza
        var clef = (Clef) d;
        var clefType = (Types) e.NewValue;
        clef.SetType(clefType);
    }

    public Types Type
    {
        get => (Types) GetValue(TypeProperty);
        set => SetValue(TypeProperty, value);
    }

    #endregion

    public static Staff.Position StaffPosition(Pitch.Names pitchName, Octave.Names octaveName, Types type)
    {
        var clefTypeDesc = Descriptions[type];
        var intervals =
            (int) clefTypeDesc.PitchName + (int) clefTypeDesc.OctaveName * 7
            - ((int) pitchName + (int) octaveName * 7);
        // if (intervals > 0)
            // intervals += 7;

        var staffPosition = clefTypeDesc.StaffPosition + intervals;

        return staffPosition;
    }
}