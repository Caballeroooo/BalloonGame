using System;
using System.Linq;
using UnityEngine;

public class Boosters : MonoBehaviour
{
    [SerializeField] private Booster[] _boosters;

    private Booster _currentBooster;
    
    public event Action TimerEnded;
    public event Action ForceCompleted;

    public bool AnyBoosterEnable => _boosters.Any(booster => booster.Enabled);

    private void OnEnable()
    {
        foreach (var booster in _boosters)
        {
            booster.TimerEnded += OnBoosterTimerEnded;
            booster.ForceCompleted += OnBoosterForceCompleted;
        }
    }

    private void Start()
    {
        foreach (var booster in _boosters)
        {
            booster.Disable();
        }
    }

    private void OnDisable()
    {
        foreach (var booster in _boosters)
        {
            booster.TimerEnded -= OnBoosterTimerEnded;
            booster.ForceCompleted -= OnBoosterForceCompleted;
        }
    }

    public void SetCurrent(BoosterType type)
    {
        _currentBooster = GetCurrentBooster(type);
    }

    public void EnableCurrent()
    {
        if (_currentBooster != null)
        {
            _currentBooster.Enable();
        }
    }

    public void DisableCurrent()
    {
        if (_currentBooster != null)
        {
            _currentBooster.Disable();
        }
    }
    
    private Booster GetCurrentBooster(BoosterType type)
    {
        return _boosters.First(booster => booster.Type == type);
    }

    private void OnBoosterTimerEnded()
    {
        TimerEnded?.Invoke();
    }

    private void OnBoosterForceCompleted()
    {
        ForceCompleted?.Invoke();
    }
}