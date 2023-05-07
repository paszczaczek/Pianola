namespace Pianola.MAUI.Drawables;

public class SignInfo
{
    public readonly string Code;
    public readonly double AscenderRatio;

    public SignInfo(string code, double ascender, double descender)
    {
        Code = code;
        AscenderRatio = ascender / (ascender + descender);
    }
}