using UnityEngine;

public class SoundManager : Singleton<SoundManager>
{
    [SerializeField] private AudioSource _audioSource;

    private SoundSettings _settings;

    protected override void Awake()
    {
        base.Awake();
        _settings = SettingsProvider.Get<SoundSettings>();
    }

    public void PlayOneShot(TrackName name)
    {
        var clip = _settings.GetClip(name);
        _audioSource.PlayOneShot(clip);
    }
}
