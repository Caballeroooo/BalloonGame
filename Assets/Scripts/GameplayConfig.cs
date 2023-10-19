using System;
using UnityEngine;

[Serializable]
public struct GameplayConfig
{
    [SerializeField] private int _upperScoreBound;
    [SerializeField] private Range _balloonsSpeed;
    [SerializeField] private Range _balloonsSpawnTime;

    public int UpperScoreBound => _upperScoreBound;
    public Range BalloonsSpeed => _balloonsSpeed;
    public Range BalloonsSpawnTime => _balloonsSpawnTime;
}
