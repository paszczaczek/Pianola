﻿using System;

namespace Pianola;

public class ChromaticSign : Sign
{
    public static ChromaticSign Create(Chromatic.Type type)
    {
        return type switch
        {
            Chromatic.Type.Sharp => new SharpSign(),
            Chromatic.Type.Flat => new FlatSign(),
            Chromatic.Type.Natural => new NaturalSing(),
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