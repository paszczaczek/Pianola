using System;

namespace Pianola;

public class ClefSign : Sign
{
    public static ClefSign Create(Clef.Type type)
    {
        return type switch
        {
            Clef.Type.Treble => new TrebleClefSign(),
            Clef.Type.Bass => new BassClefSign(),
            _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
        };
    }

    protected ClefSign(string clefText)
    {
        Text = clefText;
    }
}

public class TrebleClefSign : ClefSign
{
    public TrebleClefSign() : base(TrebleClef)
    {
    }
}

public class BassClefSign : ClefSign
{
    public BassClefSign() : base(BassClef)
    {
    }
}