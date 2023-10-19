using System;

public class ScoreCounter : BaseCounter
{
    private GameplaySettings _gameplaySettings;
    private GameplayConfig _currentGameplayConfig;
    private int _best;
    private int _currentGameplayConfigIndex;

    public event Action ScoreBoundReached;

    private void Start()
    {
        _gameplaySettings = SettingsProvider.Get<GameplaySettings>();
        _currentGameplayConfig = _gameplaySettings.GetConfigByIndex(_currentGameplayConfigIndex);
        _best = PlayerPrefsProvider.GetPlayerBestScore();
    }

    public override void Increase()
    {
        base.Increase();
        if (Current >= _best)
        {
            PlayerPrefsProvider.SetPlayerBestScore(Current);
        }

        if (Current >= _currentGameplayConfig.UpperScoreBound)
        {
            _currentGameplayConfig = _gameplaySettings.GetConfigByIndex(++_currentGameplayConfigIndex);
            ScoreBoundReached?.Invoke();
        }
    }
}