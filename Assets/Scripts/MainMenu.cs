using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Screens _screens;
    [SerializeField] private BalloonController _balloonController;
    [SerializeField] private int _nextSceneIndex;
    [SerializeField] private Range _balloonsSpeed;
    [SerializeField] private Range _balloonsSpawnTime;

    private LeaderBoardSettings _leaderBoardSettings;

    private bool IsFirstMeet => string.IsNullOrEmpty(PlayerPrefsProvider.GetPlayerGuid());

    private void OnEnable()
    {
        Subscribe();
    }

    private void Awake()
    {
        Application.targetFrameRate = 60;
        
        _leaderBoardSettings = SettingsProvider.Get<LeaderBoardSettings>();
        _balloonController.SetCurrentSpeedRange(_balloonsSpeed);
        _balloonController.SetCurrentSpawnTimeRange(_balloonsSpawnTime);
        _balloonController.StartSpawn();

        _screens.GetScreen<LeaderBoardScreen>().SetLeaderBoardSettings(_leaderBoardSettings);

        if (!IsFirstMeet)
        {
            RestorePlayerConfig();
            UpdateLeaderBoardScreen();
        }
    }

    private void Start()
    {
        ShowStartScreen();
    }

    private void OnDisable()
    {
        Unsubscribe();
    }

    private void Subscribe()
    {
        var menuScreen = _screens.GetScreen<MenuScreen>();
        menuScreen.StartGameButtonClicked += OnStartGameButtonClicked;
        menuScreen.LeaderBoardButtonClicked += OnLeaderBoardButtonClicked;

        _screens.GetScreen<LeaderBoardScreen>().BackButtonClicked += OnLeaderBoardBackButtonClicked;
    }

    private void Unsubscribe()
    {
        var menuScreen = _screens.GetScreen<MenuScreen>();
        menuScreen.StartGameButtonClicked -= OnStartGameButtonClicked;
        menuScreen.LeaderBoardButtonClicked -= OnLeaderBoardButtonClicked;

        _screens.GetScreen<LeaderBoardScreen>().BackButtonClicked -= OnLeaderBoardBackButtonClicked;

        if (IsFirstMeet)
        {
            _screens.GetScreen<FirstMeetScreen>().ApplyButtonClicked -= OnApplyButtonClicked;
        }
    }

    private void ShowStartScreen()
    {
        if (IsFirstMeet)
        {
            var firstMeetScreen = _screens.GetScreen<FirstMeetScreen>();
            firstMeetScreen.Show();
            firstMeetScreen.ApplyButtonClicked += OnApplyButtonClicked;
        }
        else
        {
            _screens.GetScreen<MenuScreen>().Show();
        }
    }

    private void OnStartGameButtonClicked()
    {
        AsyncSceneLoader.Instance.LoadScene(_nextSceneIndex);
    }

    private void OnLeaderBoardButtonClicked()
    {
        _screens.GetScreen<MenuScreen>().Hide();
        _screens.GetScreen<LeaderBoardScreen>().Show();
    }

    private void OnApplyButtonClicked(string nickname)
    {
        _screens.GetScreen<MenuScreen>().Show();
        var firstMeetScreen = _screens.GetScreen<FirstMeetScreen>();
        firstMeetScreen.Hide();
        firstMeetScreen.ApplyButtonClicked -= OnApplyButtonClicked;

        GeneratePlayerConfig(nickname);

        UpdateLeaderBoardScreen();
    }

    private void OnLeaderBoardBackButtonClicked()
    {
        _screens.GetScreen<LeaderBoardScreen>().Hide();
        _screens.GetScreen<MenuScreen>().Show();
    }

    private void GeneratePlayerConfig(string nickname)
    {
        var playerConfig = _leaderBoardSettings.AddNew(nickname);
        PlayerPrefsProvider.SetPlayerGuid(playerConfig.Guid);
        PlayerPrefsProvider.SetPlayerNickname(playerConfig.Nickname);
    }

    private void RestorePlayerConfig()
    {
        var guid = PlayerPrefsProvider.GetPlayerGuid();
        var nickname = PlayerPrefsProvider.GetPlayerNickname();
        var bestScore = PlayerPrefsProvider.GetPlayerBestScore();
        _leaderBoardSettings.Restore(guid, nickname, bestScore);
    }

    private void UpdateLeaderBoardScreen()
    {
        var leaderBoardScreen = _screens.GetScreen<LeaderBoardScreen>();
        leaderBoardScreen.UpdateLines();
        leaderBoardScreen.PrepareLinesPosition();
    }
}