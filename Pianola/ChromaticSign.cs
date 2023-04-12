using System;

namespace Pianola;

public class ChromaticSign : Sign
{
    public enum Type
    {
        Sharp,
        Flat,
        Natural
    }

    public static ChromaticSign Create(Type type)
    {
        return type switch
        {
            Type.Sharp => new SharpSign(),
            Type.Flat => new FlatSign(),
            Type.Natural => new NaturalSing(),
            _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
        };
    }

    protected ChromaticSign(string chromaticText)
    {
        Text = chromaticText;
    }
}

public class SharpSign : ChromaticSign
{
    public SharpSign() : base(Sharp)
    {
    }
}

public class FlatSign : ChromaticSign
{
    public FlatSign() : base(Flat)
    {
    }
}

public class NaturalSing : ChromaticSign
{
    public NaturalSing() : base(Natural)
    {
    }
}