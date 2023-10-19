using System;
using UnityEngine;

public class ChooseBoosterScreen : ScreenBase
{
    [SerializeField] private BoosterButton[] _boosterButtons;

    public event Action<BoosterType> Chose;

    private void OnEnable()
    {
        foreach (var boosterButton in _boosterButtons)
        {
            boosterButton.Clicked += OnBoosterButtonClicked;
        }
    }

    private void OnDisable()
    {
        foreach (var boosterButton in _boosterButtons)
        {
            boosterButton.Clicked -= OnBoosterButtonClicked;
        }
    }

    private void OnBoosterButtonClicked(BoosterType type)
    {
        Chose?.Invoke(type);
    }
}