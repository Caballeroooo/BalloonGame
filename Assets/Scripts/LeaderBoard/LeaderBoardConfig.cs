using System;
using UnityEngine;

[Serializable]
public struct LeaderBoardConfig
{
    [SerializeField] private string _guid;
    [SerializeField] private string _nickname;
    [SerializeField] private int _bestScore;

    public string Guid => _guid;
    public string Nickname => _nickname;
    public int BestScore => _bestScore;

    public LeaderBoardConfig(string nickname, int bestScore) : this(GetNewGuid(), nickname, bestScore)
    {
    }

    public LeaderBoardConfig(string guid, string nickname, int bestScore)
    {
        _guid = guid;
        _nickname = nickname;
        _bestScore = bestScore;
    }

    private static string GetNewGuid()
    {
        return System.Guid.NewGuid().ToString();
    }
}