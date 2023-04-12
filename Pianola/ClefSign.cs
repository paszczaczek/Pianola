using System;

namespace Pianola;

public class ClefSign : Sign
{
    public enum Type
    {
        Treble,
        Bass
    }

    public static ClefSign Create(Type type)
    {
        return type switch
        {
            Type.Treble => new TrebleClefSign(),
            Type.Bass => new BassClefSign(),
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