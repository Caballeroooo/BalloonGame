using UnityEngine;

public static class ColorExtension
{
    public static Color WithA(this Color clr, float a)
    {
        return new Color(clr.r, clr.g, clr.b, a);
    }
}