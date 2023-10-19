using System;
using UnityEngine;

public abstract class Booster : MonoBehaviour
{
    [SerializeField] private BoosterType _type;
    [SerializeField] private Timer _timer;
    [SerializeField] private float _boostTime = 10f;

    protected Camera Camera;
    
    public event Action TimerEnded;
    public event Action ForceCompleted;

    public BoosterType Type => _type;
    public abstract bool Enabled { get; }

    public abstract void Enable();
    public abstract void Disable();

    private void OnEnable()
    {
        Subscribe();
    }

    protected virtual void Awake()
    {
        Camera = Camera.main;
    }

    private void Start()
    {
        Disable();
    }

    private void OnDisable()
    {
        Unsubscribe();
        _timer.Stop();
    }

    protected virtual void Subscribe()
    {
        _timer.Ended += OnTimerEnded;
    }

    protected virtual void Unsubscribe()
    {
        _timer.Ended -= OnTimerEnded;
    }

    protected void StartTimer()
    {
        _timer.SetEndTime(_boostTime);
        _timer.Play();
    }

    protected void StopTimer()
    {
        _timer.Stop();
    }

    protected void ForceComplete()
    {
        ForceCompleted?.Invoke();
    }

    private void OnTimerEnded()
    {
        TimerEnded?.Invoke();
    }
}