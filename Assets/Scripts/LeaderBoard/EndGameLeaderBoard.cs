using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EndGameLeaderBoard : MonoBehaviour
{
    private const int MaxLinesCount = 5;
    
    [SerializeField] private List<LeaderBoardLineView> _lines;
    [SerializeField] private LeaderBoardShowAnimation _animation;
    
    private LeaderBoardSettings _settings;
    private int _currentLinesCount;

    private void Start()
    {
        _settings = SettingsProvider.Get<LeaderBoardSettings>();
        HideAllLines();
    }

    public void Initialize()
    {
        var allConfigs = new List<LeaderBoardConfig>();
        allConfigs.AddRange(_settings.Configs);
        allConfigs = allConfigs.OrderByDescending(config => config.BestScore).ToList();

        var playerConfig =
            allConfigs.First(config => config.Guid == PlayerPrefsProvider.GetPlayerGuid());

        var playerConfigIndex = allConfigs.IndexOf(playerConfig);

        var configsToShow = GetNeighboringConfigs(allConfigs, playerConfigIndex);
        _currentLinesCount = configsToShow.Count;

        var firstBoardPlace = allConfigs.IndexOf(configsToShow[0]) + 1;
        for (int i = 0; i < _currentLinesCount; i++)
        {
            _lines[i].SetNickname(configsToShow[i].Nickname);
            _lines[i].SetBestScore(configsToShow[i].BestScore);
            _lines[i].SetPlace(firstBoardPlace + i);
            _lines[i].SetBackgroundColor(configsToShow[i].Guid == PlayerPrefsProvider.GetPlayerGuid()
                ? _settings.PlayerLineViewColor
                : _settings.DefaultLineViewColor);
        }
    }

    public void Show()
    {
        _animation.Show(_lines, _currentLinesCount);
    }

    private List<LeaderBoardConfig> GetNeighboringConfigs(List<LeaderBoardConfig> allConfigs, int playerConfigIndex)
    {
        var result = new List<LeaderBoardConfig>();
        result.Add(allConfigs[playerConfigIndex]);

        var neighbourIteration = 1;
        var linesCount = allConfigs.Count > MaxLinesCount ? MaxLinesCount : allConfigs.Count;
        while (result.Count < linesCount)
        {
            if (playerConfigIndex + neighbourIteration < allConfigs.Count)
            {
                result.Add(allConfigs[playerConfigIndex + neighbourIteration]);
            }

            if (playerConfigIndex - neighbourIteration >= 0)
            {
                result.Insert(0, allConfigs[playerConfigIndex - neighbourIteration]);
            }

            neighbourIteration++;
        }

        return result;
    }

    private void HideAllLines()
    {
        foreach (var line in _lines)
        {
            line.Hide();
        }

        _animation.PrepareLinesPosition(_lines);
    }
}