using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class BoosterButton : MonoBehaviour
{
    [SerializeField] private BoosterType _type;
    [SerializeField] private Button _button;

    public event Action<BoosterType> Clicked;

    private void OnEnable()
    {
        _button.onClick.AddListener(OnClicked);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(OnClicked);
    }

    private void OnClicked()
    {
        Clicked?.Invoke(_type);
    }
}
