using System.Linq;
using UnityEngine;

[CreateAssetMenu(menuName = "Settings/SoundSettings", fileName = "SoundSettings", order = 0)]
public class SoundSettings : ScriptableObject
{
    [SerializeField] private SoundConfig[] _configs;

    public AudioClip GetClip(TrackName name)
    {
        return _configs.First(config => config.Name == name).Clip;
    }
}