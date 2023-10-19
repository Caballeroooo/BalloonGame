using System;
using UnityEngine;

public enum OrientationRatio
{
    Portrait = 0,
    Landscape = 1
}

public class OrientationSwitcher : MonoBehaviour
{
    private OrientationRatio _currentOrientationRatio;

    public static event Action<OrientationRatio> OrientationSwitched;

    private void Awake()
    {
        _currentOrientationRatio = Screen.width / Screen.height < 1 ? OrientationRatio.Portrait : OrientationRatio.Landscape;
        OrientationSwitched?.Invoke(_currentOrientationRatio);
    }

    private void Update()
    {
        if (Screen.width / Screen.height < 1)
        {
            if (_currentOrientationRatio == OrientationRatio.Portrait)
                return;

            _currentOrientationRatio = OrientationRatio.Portrait;
            OrientationSwitched?.Invoke(_currentOrientationRatio);
        }
        else
        {
            if (_currentOrientationRatio == OrientationRatio.Landscape)
                return;

            _currentOrientationRatio = OrientationRatio.Landscape;
            OrientationSwitched?.Invoke(_currentOrientationRatio);
        }
    }
}