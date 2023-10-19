using System;
using UnityEngine;
using Random = UnityEngine.Random;

[Serializable]
public struct RangeInt
{
    [SerializeField] private int _min;
    [SerializeField] private int _max;

    public RangeInt(int min, int max)
    {
        _min = min;
        _max = max;
    }

    public int Min => _min;
    public int Max => _max;

    public int GetRandom()
    {
        return Random.Range(_min, _max + 1);
    }
}
