using System;
using UnityEngine;

public abstract class BaseCounter : MonoBehaviour
{
    protected GameplaySettings GameplaySettings;
    
    public int Current { get; protected set; }

    public event Action<int> Changed;

    private void Awake()
    {
        GameplaySettings = SettingsProvider.Get<GameplaySettings>();
    }

    public virtual void Increase()
    {
        Current++;
        Changed?.Invoke(Current);
    }
}