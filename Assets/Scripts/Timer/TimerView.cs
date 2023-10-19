using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class TimerView : MonoBehaviour
{
    [SerializeField] private Timer _timer;
    [SerializeField] private Image _image;
    [SerializeField] private float _showDuration = 0.5f;
    [SerializeField] private float _hideDuration = 0.5f;
    [SerializeField] private float _blinkFadeValue = 0.4f;
    [SerializeField] private float _blinkStepDuration = 0.1f;

    private Sequence _blinkSequence;

    private void OnEnable()
    {
        _timer.Changed += OnTimerChanged;
        _timer.CriticalValueReached += OnCriticalValueReached;
    }

    private void Start()
    {
        _blinkSequence = DOTween.Sequence();
        _blinkSequence.SetAutoKill(false);
        _blinkSequence.Pause();
    }

    private void OnDisable()
    {
        _timer.Changed -= OnTimerChanged;
        _timer.CriticalValueReached -= OnCriticalValueReached;
    }

    private void OnDestroy()
    {
        _image.DOKill();
        _blinkSequence.Kill();
    }

    public void Show()
    {
        _image.DOFade(1f, _showDuration).SetEase(Ease.InCubic);
    }

    public void Hide()
    {
        PauseBlinking();
        _image.DOFade(0f, _hideDuration).SetEase(Ease.OutCubic).OnComplete(Reset);
    }

    public void FastHide()
    {
        _image.color = _image.color.WithA(0f);
    }

    private void Reset()
    {
        _image.fillAmount = 1f;
    }

    private void OnTimerChanged(float normalizedValue)
    {
        _image.fillAmount = normalizedValue;
    }

    private void OnCriticalValueReached()
    {
        _timer.CriticalValueReached -= OnCriticalValueReached;
        StartBlinking();
    }

    private void StartBlinking()
    {
        if (_blinkSequence.playedOnce)
        {
            _blinkSequence.Restart();
        }
        else
        {
            _blinkSequence.Append(_image.DOFade(_blinkFadeValue, _blinkStepDuration).SetEase(Ease.Linear));
            _blinkSequence.Append(_image.DOFade(1f, _blinkStepDuration).SetEase(Ease.Linear));
            _blinkSequence.SetLoops(-1);
            _blinkSequence.Play();
        }
    }

    private void PauseBlinking()
    {
        _blinkSequence.Pause();
    }
}
