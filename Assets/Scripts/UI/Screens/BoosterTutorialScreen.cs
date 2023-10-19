using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class BoosterTutorialScreen : ScreenBase, IPointerDownHandler
{
    [SerializeField] private Animator _animator;
    
    public event Action Clicked;

    private void Start()
    {
        _animator.enabled = false;
    }

    public override void Show()
    {
        base.Show();
        _animator.enabled = true;
    }

    public override void Hide()
    {
        base.Hide();
        _animator.enabled = false;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Clicked?.Invoke();
    }
}
