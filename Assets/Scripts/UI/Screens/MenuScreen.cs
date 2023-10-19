using System;
using UnityEngine;

public class MenuScreen : ScreenBase
{
    [SerializeField] private AnimatedButton _startGameButton;
    [SerializeField] private AnimatedButton _leaderBoardButton;

    public event Action StartGameButtonClicked;
    public event Action LeaderBoardButtonClicked;

    private void OnEnable()
    {
        _startGameButton.Clicked += OnStartGameButtonClicked;
        _leaderBoardButton.Clicked += OnLeaderBoardButtonClicked;
    }

    private void OnDisable()
    {
        _startGameButton.Clicked -= OnStartGameButtonClicked;
        _leaderBoardButton.Clicked -= OnLeaderBoardButtonClicked;
    }

    private void OnStartGameButtonClicked()
    {
        StartGameButtonClicked?.Invoke();
    }

    private void OnLeaderBoardButtonClicked()
    {
        LeaderBoardButtonClicked?.Invoke();
    }
}