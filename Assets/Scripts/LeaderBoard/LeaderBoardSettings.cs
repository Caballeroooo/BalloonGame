using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(menuName = "Settings/LeaderBoardSettings", fileName = "LeaderBoardSettings", order = 0)]
public class LeaderBoardSettings : ScriptableObject
{
    private const string PathToNicknames = "Assets/JSONData/Nicknames.json";

    [SerializeField] private List<LeaderBoardConfig> _configs;
    [SerializeField] private Color _playerLineViewColor;
    [SerializeField] private Color _defaultLineViewColor;
#if UNITY_EDITOR
    [SerializeField] private RangeInt _bestScoreRange;
#endif

    public IEnumerable<LeaderBoardConfig> Configs => _configs;
    public Color PlayerLineViewColor => _playerLineViewColor;
    public Color DefaultLineViewColor => _defaultLineViewColor;
    
    public LeaderBoardConfig AddNew(string nickname)
    {
        var newConfig = new LeaderBoardConfig(nickname, 0);
        _configs.Add(newConfig);
        return newConfig;
    }

    public void Restore(string guid, string nickname, int bestScore)
    {
        Remove(guid);
        var newConfig = new LeaderBoardConfig(guid, nickname, bestScore);
        _configs.Add(newConfig);
    }

    public void UpdatePlayerConfig(string guid, int bestScore)
    {
        var playerConfig = _configs.First(config => config.Guid == guid);
        var nickname = playerConfig.Nickname;
        Remove(guid);
        var newConfig = new LeaderBoardConfig(guid, nickname, bestScore);
        _configs.Add(newConfig);
    }

    public void Remove(string guid)
    {
        var config = _configs.FirstOrDefault(config => config.Guid == guid);
        _configs.Remove(config);
    }
    
#if UNITY_EDITOR
    [ContextMenu("Init")]
    private void Generate()
    {
        var jsonString = File.ReadAllText(PathToNicknames);
        var nicknames = JsonHelper.FromJson<Nickname>(jsonString);

        _configs = new List<LeaderBoardConfig>();
        for (int i = 0; i < nicknames.Length; i++)
        {
            var newConfig = new LeaderBoardConfig(nicknames[i].Name, _bestScoreRange.GetRandom());
            _configs.Add(newConfig);
        }
    }
#endif
}