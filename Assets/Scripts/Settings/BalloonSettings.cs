using UnityEngine;

[CreateAssetMenu(menuName = "Settings/BalloonSettings", fileName = "BalloonSettings", order = 0)]
public class BalloonSettings : ScriptableObject
{
    [SerializeField] private Color[] _colors;

    public Color GetRandomColor()
    {
        return _colors.GetRandom();
    }
}
