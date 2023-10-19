using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class DefaultButton : MonoBehaviour
{
    [SerializeField] private Button _button;

    public event Action Clicked;

    private void OnEnable()
    {
        _button.onClick.AddListener(OnClicked);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(OnClicked);
    }

    public virtual void Enable()
    {
        _button.interactable = true;
    }

    public virtual void Disable()
    {
        _button.interactable = false;
    }

    private void OnClicked()
    {
        Clicked?.Invoke();
    }
}