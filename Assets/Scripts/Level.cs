using UnityEngine;

public class Level : MonoBehaviour
{
    [SerializeField] private BalloonController _balloonController;
    [SerializeField] private ComboController _comboController;
    [SerializeField] private ScoreCounter _scoreCounter;
    [SerializeField] private MissCounter _missCounter;
    [SerializeField] private Clicker _clicker;
    [SerializeField] private Screens _screens;
    [SerializeField] private int _previousSceneIndex;
    [SerializeField] private Boosters _boosters;
    [SerializeField] private Tutorials _tutorials;
    [SerializeField] private CameraEffects _cameraEffects;
    [SerializeField] private float _timeScalingDuration = 0.6f;

    private LeaderBoardSettings _leaderBoardSettings;
    private GameplaySettings _gameplaySettings;
    private int _currentGameplayConfigIndex;
    
    private bool AnyBoosterEnable => _boosters.AnyBoosterEnable;
    private bool IsFirstGame => PlayerPrefsProvider.GetPlayerBestScore() == 0;

    private void OnEnable()
    {
        Subscribe();
    }

    private void Start()
    {
        _leaderBoardSettings = SettingsProvider.Get<LeaderBoardSettings>();
        _gameplaySettings = SettingsProvider.Get<GameplaySettings>();

        var gameplayScreen = _screens.GetScreen<GameplayScreen>();
        gameplayScreen.Show();
        gameplayScreen.FastHideTimer();

        _clicker.Enable();

        if (!IsFirstGame)
        {
            StartSpawnBalloons();
        }
        else
        {
            _balloonController.SpawnTutorial(_gameplaySettings.TutorialBalloonSpeed);
        }
    }

    private void OnDisable()
    {
        Unsubscribe();
    }

    private void Subscribe()
    {
        _balloonController.BalloonKilled += OnBalloonKilled;
        _balloonController.BalloonMissed += OnBalloonMissed;
        _balloonController.TutorialMoveCompleted += OnTapTutorialTriggered;
        _comboController.ComboReached += OnComboReached;
        _screens.GetScreen<ChooseBoosterScreen>().Chose += OnBoosterChoose;
        _screens.GetScreen<GameplayScreen>().MenuButtonClicked += OnMenuButtonClicked;
        _screens.GetScreen<GameOverScreen>().MenuButtonClicked += OnMenuButtonClicked;
        _missCounter.Reached += OnMissCounterReached;
        _boosters.TimerEnded += OnBoosterTimerEnded;
        _boosters.ForceCompleted += OnBoosterForceCompleted;
        _scoreCounter.ScoreBoundReached += OnScoreBoundReached;

        if (IsFirstGame)
        {
            _balloonController.BalloonKilled += HideTapTutorial;
        }
    }

    private void Unsubscribe()
    {
        UnsubscribeCore();
        _screens.GetScreen<ChooseBoosterScreen>().Chose -= OnBoosterChoose;
        _screens.GetScreen<GameplayScreen>().MenuButtonClicked -= OnMenuButtonClicked;
        _screens.GetScreen<GameOverScreen>().MenuButtonClicked -= OnMenuButtonClicked;
    }

    private void UnsubscribeCore()
    {
        _balloonController.BalloonKilled -= OnBalloonKilled;
        _balloonController.BalloonMissed -= OnBalloonMissed;
        _balloonController.TutorialMoveCompleted -= OnTapTutorialTriggered;
        _comboController.ComboReached -= OnComboReached;
        _missCounter.Reached -= OnMissCounterReached;
        _boosters.TimerEnded -= OnBoosterTimerEnded;
        _boosters.ForceCompleted -= OnBoosterForceCompleted;
        _scoreCounter.ScoreBoundReached -= OnScoreBoundReached;

        if (IsFirstGame)
        {
            _balloonController.BalloonKilled -= HideTapTutorial;
        }
    }

    private void OnBalloonKilled(Vector2 balloonPosition)
    {
        _cameraEffects.Shake();
        _scoreCounter.Increase();
        if (!AnyBoosterEnable)
        {
            _comboController.Increase(balloonPosition);
        }
    }

    private void OnBalloonMissed()
    {
        _missCounter.Increase();
        if (!AnyBoosterEnable)
        {
            _comboController.Reset();
        }
    }

    private void OnComboReached()
    {
        TimeScaler.Instance.StartChange(0, _timeScalingDuration, CurveType.InQuad);
        _clicker.Disable();
        _screens.GetScreen<ChooseBoosterScreen>().Show();
        _screens.GetScreen<GameplayScreen>().Hide();
    }

    private void OnBoosterChoose(BoosterType type)
    {
        _screens.GetScreen<ChooseBoosterScreen>().Hide();
        _screens.GetScreen<GameplayScreen>().Show();
        if (!TryShowBoosterTutorial(type))
        {
            TimeScaler.Instance.StartChange(1, _timeScalingDuration, CurveType.OutQuad);
        }

        _boosters.SetCurrent(type);
        EnableBooster();
    }

    private bool TryShowBoosterTutorial(BoosterType type)
    {
        if (_tutorials.NeedShowBoosterTutorial(type))
        {
            switch (type)
            {
                case BoosterType.Slasher:
                {
                    var slashTutorialScreen = _screens.GetScreen<SlashTutorialScreen>();
                    slashTutorialScreen.Show();
                    slashTutorialScreen.Clicked += HideSlashTutorial;
                    break;
                }
                case BoosterType.MegaTap:
                {
                    var megaTapTutorialScreen = _screens.GetScreen<MegaTapTutorialScreen>();
                    megaTapTutorialScreen.Show();
                    megaTapTutorialScreen.Clicked += HideMegaTapTutorial;
                    break;
                }
            }

            return true;
        }

        return false;
    }

    private void OnBoosterTimerEnded()
    {
        DisableBooster();
    }

    private void OnBoosterForceCompleted()
    {
        DisableBooster();
    }

    private void OnMissCounterReached()
    {
        StopGame();
    }

    private void StopGame()
    {
        UpdatePlayerConfig();
        UnsubscribeCore();
        DisableBooster();
        _balloonController.FinishGame();
        _balloonController.FinishAnimationCompleted += OnFinishAnimationCompleted;
    }

    private void OnFinishAnimationCompleted()
    {
        _balloonController.FinishAnimationCompleted -= OnFinishAnimationCompleted;
        _screens.GetScreen<GameplayScreen>().Hide();
        var gameOverScreen = _screens.GetScreen<GameOverScreen>();
        gameOverScreen.InitializeLeaderBoard();
        gameOverScreen.SetCurrentScore(_scoreCounter.Current);
        gameOverScreen.Show();
        gameOverScreen.ShowLeaderBoard();
    }

    private void EnableBooster()
    {
        _boosters.EnableCurrent();
        _screens.GetScreen<GameplayScreen>().ShowTimer();
    }

    private void DisableBooster()
    {
        _boosters.DisableCurrent();
        _clicker.Enable();
        _comboController.Reset();
        var gameplayScreen = _screens.GetScreen<GameplayScreen>();
        gameplayScreen.HideTimer();
    }

    private void OnMenuButtonClicked()
    {
        AsyncSceneLoader.Instance.LoadScene(_previousSceneIndex);
    }

    private void UpdatePlayerConfig()
    {
        var guid = PlayerPrefsProvider.GetPlayerGuid();
        var bestScore = PlayerPrefsProvider.GetPlayerBestScore();
        _leaderBoardSettings.UpdatePlayerConfig(guid, bestScore);
    }

    private void OnTapTutorialTriggered()
    {
        _screens.GetScreen<TapTutorialScreen>().Show();
    }

    private void HideTapTutorial(Vector2 vector2)
    {
        _balloonController.BalloonKilled -= HideTapTutorial;
        _tutorials.ResetTimeScale();
        _screens.GetScreen<TapTutorialScreen>().Hide();
        StartSpawnBalloons();
    }

    private void StartSpawnBalloons()
    {
        _balloonController.SetCurrentSpeedRange(_gameplaySettings.GetConfigByIndex(_currentGameplayConfigIndex)
            .BalloonsSpeed);
        _balloonController.SetCurrentSpawnTimeRange(_gameplaySettings.GetConfigByIndex(_currentGameplayConfigIndex)
            .BalloonsSpawnTime);
        _balloonController.StartSpawn();
    }

    private void HideSlashTutorial()
    {
        var slashTutorialScreen = _screens.GetScreen<SlashTutorialScreen>();
        slashTutorialScreen.Hide();
        slashTutorialScreen.Clicked -= HideSlashTutorial;

        _tutorials.ResetTimeScale();
    }

    private void HideMegaTapTutorial()
    {
        var megaTapTutorialScreen = _screens.GetScreen<MegaTapTutorialScreen>();
        megaTapTutorialScreen.Hide();
        megaTapTutorialScreen.Clicked += HideMegaTapTutorial;

        _tutorials.ResetTimeScale();
    }

    private void OnScoreBoundReached()
    {
        _currentGameplayConfigIndex++;
        _balloonController.SetCurrentSpeedRange(_gameplaySettings.GetConfigByIndex(_currentGameplayConfigIndex)
            .BalloonsSpeed);
        _balloonController.SetCurrentSpawnTimeRange(_gameplaySettings.GetConfigByIndex(_currentGameplayConfigIndex)
            .BalloonsSpawnTime);
    }
}