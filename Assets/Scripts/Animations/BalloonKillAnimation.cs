using System;
using UnityEngine;
using DG.Tweening;

public class BalloonKillAnimation : MonoBehaviour
{
    [SerializeField] private Vector3 _endScale;
    [SerializeField] private float _scalingDurations;

    public event Action Ended;

    public void Play()
    {
        transform.DOScale(_endScale, _scalingDurations).SetEase(Ease.InCubic).OnComplete(() => Ended?.Invoke());
    }

    private void OnDestroy()
    {
        transform.DOKill();
    }
}
