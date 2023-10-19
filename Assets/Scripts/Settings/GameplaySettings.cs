using UnityEngine;

[CreateAssetMenu(menuName = "Settings/GameplaySettings", fileName = "GameplaySettings", order = 0)]
public class GameplaySettings : ScriptableObject
{
    [Min(0f)]
    [SerializeField] private float _tutorialBalloonSpeed;
    [Min(0)]
    [SerializeField] private int _maxComboForBonus = 10;
    [Range(3, 6)]
    [SerializeField] private int _missForGameOver = 5;
    [Range(0f, 1f)]
    [SerializeField] private float _criticalTimerNormalizedValue = 0.2f;
    [SerializeField] private GameplayConfig[] _configs;

    public float TutorialBalloonSpeed => _tutorialBalloonSpeed;
    public int MaxComboForBonus => _maxComboForBonus;
    public int MissForGameOver => _missForGameOver;
    public float CriticalTimerNormalizedValue => _criticalTimerNormalizedValue;

    public GameplayConfig GetConfigByIndex(int index)
    {
        if (index >= 0 && index < _configs.Length)
        {
            return _configs[index];
        }
        else
        {
            Debug.LogWarning($"The index = {index} is not within the bounds of the array. Will be returned first element.");
            return _configs[0];
        }
    }
}
