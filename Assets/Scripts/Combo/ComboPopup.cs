using System;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class ComboPopup : MonoBehaviour, IPoolable
{
    [SerializeField] private TMP_Text _text;
    [SerializeField] private float _scalingTime = 0.2f;
    [SerializeField] private float _moveUpTime = 0.6f;
    [SerializeField] private float _fadeTime = 0.2f;

    public event Action<ComboPopup> Showed;

    public void Initialize()
    {
        ResetValues();
    }

    public void SetParent(Transform parent)
    {
        transform.SetParent(parent);
    }

    public void Show(int value, Vector3 position)
    {
        gameObject.SetActive(true);
        _text.text = $"x{value}";
        transform.position = position;
        PlayAnimation();
    }

    private void PlayAnimation()
    {
        var sequence = DOTween.Sequence();
        sequence.Append(transform.DOScale(Vector3.one, _scalingTime).SetEase(Ease.OutBack));
        sequence.Join(transform.DOLocalMove(Vector3.up, _moveUpTime).SetRelative(true));
        sequence.Join(_text.DOFade(0f, _fadeTime).SetDelay(_moveUpTime - _fadeTime));
        sequence.OnComplete(OnShowComplete);
    }

    private void OnShowComplete()
    {
        Showed?.Invoke(this);
        ResetValues();
    }

    private void ResetValues()
    {
        transform.localScale = Vector3.zero;
        _text.color = _text.color.WithA(1f);
        gameObject.SetActive(false);
    }
}