using UnityEngine;

public class BalloonKillVFX : MonoBehaviour, IPoolable
{
    [SerializeField] private ParticleSystem[] _killVFX;

    public void Initialize()
    {
        foreach (var vfx in _killVFX)
        {
            vfx.Stop();
        }
    }

    public void Play()
    {
        foreach (var vfx in _killVFX)
        {
            vfx.Play();
        }
    }

    public void SetColor(Color color)
    {
        foreach (var vfx in _killVFX)
        {
            var mainModule = vfx.main;
            mainModule.startColor = color;
        }
    }

    public void SetParent(Transform parent)
    {
        transform.SetParent(parent);
    }
}