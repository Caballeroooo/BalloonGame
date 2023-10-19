using UnityEngine;

public static class Vector2Extension
{
    public static Vector2 WithX(this Vector2 vec, float x)
    {
        return new Vector2(x, vec.y);
    }

    public static Vector2 WithY(this Vector2 vec, float y)
    {
        return new Vector2(vec.x, y);
    }

    public static Vector3 WithZ(this Vector2 vec, float z)
    {
        return new Vector3(vec.x, vec.y, z);
    }
}
