using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class MissCounterItemView : MonoBehaviour
{
    [SerializeField] private Image _fill;
    [SerializeField] private Color _fillColor;
    [SerializeField] private float _endScale = 1.1f;
    [SerializeField] private float _scaleInDuration = 0.4f;
    [SerializeField] private float _scaleOutDuration = 0.4f;

    private RectTransform _rectTransform;

    public event Action FillCompleted;

    private void Start()
    {
        _rectTransform = transform as RectTransform;
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public void Fill()
    {
        var sequence = DOTween.Sequence();
        sequence.Append(_rectTransform.DOScale(_endScale, _scaleInDuration).SetEase(Ease.OutCubic));
        sequence.Join(_fill.DOColor(_fillColor, _scaleInDuration).SetEase(Ease.OutCubic));
        sequence.Append(_rectTransform.DOScale(1f, _scaleOutDuration).SetEase(Ease.InCubic));
        sequence.OnComplete(() => FillCompleted?.Invoke());
    }
}
