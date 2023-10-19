using System;
using UnityEngine;

public class LeaderBoardScreen : ScreenBase
{
    [SerializeField] private LeaderBoard _leaderBoard;
    [SerializeField] private DefaultButton _backButton;

    public event Action BackButtonClicked;

    private void OnEnable()
    {
        _backButton.Clicked += OnBackButtonClicked;
    }

    private void OnDisable()
    {
        _backButton.Clicked -= OnBackButtonClicked;
    }

    public override void Show()
    {
        base.Show();
        _leaderBoard.Show();
    }

    public override void Hide()
    {
        base.Hide();
        _leaderBoard.Hide();
    }

    public void SetLeaderBoardSettings(LeaderBoardSettings settings)
    {
        _leaderBoard.SetLeaderBoardSettings(settings);
    }

    public void UpdateLines()
    {
        _leaderBoard.UpdateLines();
    }

    public void PrepareLinesPosition()
    {
        _leaderBoard.PrepareLinesPosition();
    }

    private void OnBackButtonClicked()
    {
        BackButtonClicked?.Invoke();
    }
}