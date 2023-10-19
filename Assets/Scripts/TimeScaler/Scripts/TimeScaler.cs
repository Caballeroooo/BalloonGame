using System.Collections;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class TimeScaler : Singleton<TimeScaler>
{
    [SerializeField, HideInInspector] private TimeScaleCurveSettings _settings;

    private Coroutine _changeJob;
    private float _startFixedDeltaTime;

    private void Start()
    {
        _startFixedDeltaTime = Time.fixedDeltaTime;
    }

    public void StartChange(float endValue, float duration, CurveType curveType = CurveType.Linear,
        TimeScalingType type = TimeScalingType.Default)
    {
        StartChange(Time.timeScale, endValue, duration, curveType, type);
    }

    public void StartChange(float startValue, float endValue, float duration,
        CurveType curveType = CurveType.Linear, TimeScalingType type = TimeScalingType.Default)
    {
        if (Mathf.Approximately(startValue, endValue))
            return;

        if (startValue < 0)
        {
            Debug.LogWarning($"TimeScale cannot be less than zero! Start value = {startValue}, will be equated to 0");
            startValue = 0;
        }

        if (endValue < 0)
        {
            Debug.LogWarning($"TimeScale cannot be less than zero! End value = {endValue}, will be equated to 0");
            endValue = 0;
        }

        var curve = _settings.GetCurve(curveType);

        if (_changeJob != null)
        {
            StopCoroutine(_changeJob);
        }

        switch (type)
        {
            case TimeScalingType.Default:
            {
                _changeJob = StartCoroutine(Change(startValue, endValue, duration, curve));
                break;
            }
            case TimeScalingType.PingPong:
            {
                _changeJob = StartCoroutine(ChangePingPong(startValue, endValue, duration, curve));
                break;
            }
        }
    }

    private IEnumerator ChangePingPong(float startValue, float endValue, float duration, AnimationCurve curve)
    {
        yield return StartCoroutine(Change(startValue, endValue, duration, curve));
        var curveStartTime = curve.keys[curve.keys.Length - 1].time;
        yield return StartCoroutine(Change(startValue, endValue, duration, curve, curveStartTime));
    }

    private IEnumerator Change(float startValue, float endValue, float duration, AnimationCurve curve,
        float curveStartTime = 0f)
    {
        var currentTime = 0f;

        while (currentTime < duration)
        {
            var curvePoint = curve.Evaluate(Mathf.Abs(curveStartTime - currentTime / duration));
            Time.timeScale = startValue + (endValue - startValue) * curvePoint;
            Time.fixedDeltaTime = _startFixedDeltaTime * Time.timeScale;
            currentTime += Time.unscaledDeltaTime;
            yield return null;
        }

        var endTimeScale = Mathf.Approximately(curveStartTime, 0f) ? endValue : startValue;
        Time.timeScale = endTimeScale;
        Time.fixedDeltaTime = _startFixedDeltaTime;
    }
}