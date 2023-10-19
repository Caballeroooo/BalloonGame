using System;
using TMPro;
using UnityEngine;

public class GameOverScreen : ScreenBase
{
    [SerializeField] private EndGameLeaderBoard _endGameLeaderBoard;
    [SerializeField] private AnimatedButton _menuButton;
    [SerializeField] private TMP_Text _currentScore;

    public event Action MenuButtonClicked;

    private void OnEnable()
    {
        _menuButton.Clicked += OnMenuButtonClicked;
    }

    private void OnDisable()
    {
        _menuButton.Clicked -= OnMenuButtonClicked;
    }
    
    public void SetCurrentScore(int currentScore)
    {
        _currentScore.text = currentScore.ToString();
    }
    
    public void InitializeLeaderBoard()
    {
        _endGameLeaderBoard.Initialize();
    }

    public void ShowLeaderBoard()
    {
        _endGameLeaderBoard.Show();
    }

    private void OnMenuButtonClicked()
    {
        MenuButtonClicked?.Invoke();
    }
}
