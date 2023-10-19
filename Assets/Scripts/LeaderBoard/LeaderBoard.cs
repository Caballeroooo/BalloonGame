using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LeaderBoard : MonoBehaviour
{
    [SerializeField] private LeaderBoardLinePool _linesPool;
    [SerializeField] private RectTransform _contentRoot;
    [SerializeField] private LeaderBoardShowAnimation _animation;

    private List<LeaderBoardLineView> _lines = new List<LeaderBoardLineView>();
    private LeaderBoardSettings _settings;

    public void SetLeaderBoardSettings(LeaderBoardSettings settings)
    {
        _settings = settings;
    }

    public void Show()
    {
        _animation.Show(_lines);
    }

    public void Hide()
    {
        foreach (var line in _lines)
        {
            line.Hide();
        }
        
        _contentRoot.anchoredPosition = _contentRoot.anchoredPosition.WithY(0f);
    }

    public void PrepareLinesPosition()
    {
        _animation.PrepareLinesPosition(_lines);
    }

    public void UpdateLines()
    {
        _lines.Clear();

        var allConfigs = new List<LeaderBoardConfig>();
        allConfigs.AddRange(_settings.Configs);
        allConfigs = allConfigs.OrderByDescending(config => config.BestScore).ToList();

        for (int i = 0; i < allConfigs.Count; i++)
        {
            var line = _linesPool.Get();
            line.SetBackgroundColor(allConfigs[i].Guid == PlayerPrefsProvider.GetPlayerGuid()
                ? _settings.PlayerLineViewColor
                : _settings.DefaultLineViewColor);

            line.SetParent(_contentRoot);
            line.SetNickname(allConfigs[i].Nickname);
            line.SetBestScore(allConfigs[i].BestScore);
            line.SetPlace(i + 1);
            _lines.Add(line);
        }
    }
}