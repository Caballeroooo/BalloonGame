using UnityEngine;

[CreateAssetMenu(menuName = "Settings/LeaderBoardAnimationSettings", fileName = "LeaderBoardAnimationSettings", order = 0)]
public class LeaderBoardAnimationSettings : ScriptableObject
{
    [SerializeField] private float _lineShowDuration;
    [SerializeField] private float _lineShowDelay;
    [SerializeField] private float _lineXPositionOffset;

    public float LineShowDuration => _lineShowDuration;
    public float LineShowDelay => _lineShowDelay;
    public float LineXPositionOffset => _lineXPositionOffset;
}
