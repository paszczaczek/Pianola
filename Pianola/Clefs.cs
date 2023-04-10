namespace Pianola;

public class Clef : Glyph
{
    public enum Type
    {
        Treble,
        Bass
    }
}

public class TrebleClef : Clef
{
    public TrebleClef()
    {
        Text = TrebleClef;
    }
}

public class BassClef : Clef
{
    public BassClef()
    {
        Text = BassClef;
    }
}