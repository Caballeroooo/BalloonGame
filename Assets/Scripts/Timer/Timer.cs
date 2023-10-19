using System;
using System.Collections;
using UnityEngine;

public class Timer : MonoBehaviour
{
    private GameplaySettings _gameplaySettings;
    private float _endTime;
    private Coroutine _timerJob;

    public event Action Ended;
    public event Action<float> Changed;
    public event Action CriticalValueReached;

    private void Awake()
    {
        _gameplaySettings = SettingsProvider.Get<GameplaySettings>();
    }

    public void SetEndTime(float endTime)
    {
        _endTime = endTime;
    }

    public void Play()
    {
        _timerJob = StartCoroutine(Wait(_endTime));
    }

    public void Stop()
    {
        if (_timerJob != null)
        {
            StopCoroutine(_timerJob);
        }
    }

    private IEnumerator Wait(float time)
    {
        while (time > 0f)
        {
            time -= Time.deltaTime;
            Changed?.Invoke(time / _endTime);
            if (time / _endTime < _gameplaySettings.CriticalTimerNormalizedValue)
            {
                CriticalValueReached?.Invoke();
            }
            yield return null;
        }

        Ended?.Invoke();
    }
}