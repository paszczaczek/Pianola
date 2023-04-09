using System;

namespace Pianola.Clefs;

public class Clef : Glyph
{
    public enum Type
    {
        Treble,
        Bass
    }

    internal static Clef Create(Type type)
    {
        return type switch
        {
            Type.Treble => new TrebleClef(),
            Type.Bass => new BassClef(),
            _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
        };
    }
}