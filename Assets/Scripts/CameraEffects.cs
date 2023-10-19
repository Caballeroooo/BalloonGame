using DG.Tweening;
using UnityEngine;

public class CameraEffects : Singleton<CameraEffects>
{
    [SerializeField] private Camera _camera;
    [SerializeField] private Range _orthoSizeShakeOffset;
    [SerializeField] private Range _orthoSizeShakeStepTime;
    [SerializeField] private float _focusSizeOffset;
    [SerializeField] private float _focusSizeTime;

    private Sequence _shakeSequence;
    private float _startOrthoSize;

    protected override void Awake()
    {
        base.Awake();
        _startOrthoSize = _camera.orthographicSize;
        _shakeSequence = DOTween.Sequence();
        _shakeSequence.SetAutoKill(false);
        _shakeSequence.Pause();
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        _shakeSequence.Kill();
        _camera.DOKill();
    }

    public void Shake()
    {
        if (_shakeSequence.playedOnce)
        {
            _shakeSequence.Restart();
        }
        else
        {
            _shakeSequence = DOTween.Sequence()
                .Append(_camera.DOOrthoSize(_startOrthoSize + _orthoSizeShakeOffset.GetRandom(),
                    _orthoSizeShakeStepTime.GetRandom()))
                .Append(_camera.DOOrthoSize(_startOrthoSize - _orthoSizeShakeOffset.GetRandom(),
                    _orthoSizeShakeStepTime.GetRandom()))
                .Append(_camera.DOOrthoSize(_startOrthoSize, _orthoSizeShakeStepTime.GetRandom()))
                .SetUpdate(true)
                .OnComplete(() =>
                {
                    _camera.orthographicSize = _startOrthoSize;
                });
            _shakeSequence.Play();
        }
    }

    public void Focus()
    {
        _camera.DOOrthoSize(_startOrthoSize - _focusSizeOffset, _focusSizeTime);
    }
}
