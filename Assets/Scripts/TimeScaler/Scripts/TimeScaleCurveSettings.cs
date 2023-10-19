using UnityEngine;

[CreateAssetMenu(fileName = "TimeScaleCurveSettings", menuName = "Settings/TimeScaleCurveSettings", order = 0)]
public class TimeScaleCurveSettings : ScriptableObject
{
    [SerializeField] private AnimationCurve[] _animationCurves;

    public AnimationCurve GetCurve(CurveType type)
    {
        var index = (int)type;
        return _animationCurves[index];
    }
}