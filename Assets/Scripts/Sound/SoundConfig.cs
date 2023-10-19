using System;
using UnityEngine;

[Serializable]
public struct SoundConfig
{
    [SerializeField] private TrackName _name;
    [SerializeField] private AudioClip _clip;

    public TrackName Name => _name;
    public AudioClip Clip => _clip;
}
