namespace Pianola.MAUI.Drawables;

public class SignInfo
{
    public readonly string Code;
    public readonly double AscenderRatio;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="code"></param>
    /// <param name="ascender">Odczytany z FontForge, wyrażony w jego wewnętrznych współrzędnych.</param>
    /// <param name="descender">Odczytany z FontForge, wyrażony w jego wewnętrznych współrzędnych.</param>
    public SignInfo(string code, double ascender, double descender)
    {
        Code = code;
        AscenderRatio = ascender / (ascender + descender);
    }
}