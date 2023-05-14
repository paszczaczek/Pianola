namespace Pianola.MAUI.Models;

public class KeySignature
{
    public readonly int NumberOfChromaticSigns;
    public readonly ChromaticSign ChromaticSign;

    public KeySignature(int numberOfChromaticSigns, ChromaticSign chromaticSign)
    {
        if (numberOfChromaticSigns is < 0 or > 7)
            throw new ArgumentException("Number of chromatic sings must be between 0 and 7.",
                nameof(numberOfChromaticSigns));

        if (chromaticSign is not ChromaticSign.Sharp && chromaticSign is not ChromaticSign.Flat)
            throw new ArgumentException("Chromatic sign for key signature must be Sharp or Flat.",
                nameof(chromaticSign));

        NumberOfChromaticSigns = numberOfChromaticSigns;
        ChromaticSign = chromaticSign;
    }
}