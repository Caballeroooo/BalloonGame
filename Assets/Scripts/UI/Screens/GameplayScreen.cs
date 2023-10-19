using System;
using UnityEngine;

public class GameplayScreen : ScreenBase
{
    [SerializeField] private TimerView _timerView;
    [SerializeField] private AnimatedButton _menuButton;

    public event Action MenuButtonClicked;

    private void OnEnable()
    {
        _menuButton.Clicked += OnMenuButtonClicked;
    }

    private void OnDisable()
    {
        _menuButton.Clicked -= OnMenuButtonClicked;
    }

    public void ShowTimer()
    {
        _timerView.Show();
    }

    public void HideTimer()
    {
        _timerView.Hide();
    }

    public void FastHideTimer()
    {
        _timerView.FastHide();
    }

    private void OnMenuButtonClicked()
    {
        MenuButtonClicked?.Invoke();
    }
}