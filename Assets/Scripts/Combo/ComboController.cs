using System;
using System.Collections.Generic;
using UnityEngine;

public class ComboController : MonoBehaviour
{
    [SerializeField] private ComboCounter _comboCounter;
    [SerializeField] private ComboPopupPool _comboPopupPool;

    private List<Vector2> _popupPositions;
    
    public event Action ComboReached;
    
    private void OnEnable()
    {
        Subscribe();
    }

    private void Start()
    {
        _popupPositions = new List<Vector2>();
    }

    private void OnDisable()
    {
        Unsubscribe();
    }

    public void Increase(Vector2 balloonPosition)
    {
        _popupPositions.Add(balloonPosition);
        _comboCounter.Increase();
    }

    public void Reset()
    {
        _popupPositions.Clear();
        _comboCounter.Reset();
    }

    private void Subscribe()
    {
        _comboCounter.Reached += OnComboReached;
        _comboCounter.Changed += OnComboIncreased;
    }

    private void Unsubscribe()
    {
        _comboCounter.Reached -= OnComboReached;
        _comboCounter.Changed -= OnComboIncreased;
    }

    private void OnComboReached()
    {
        ComboReached?.Invoke();

    }

    private void OnComboIncreased(int currentComboValue)
    {
        var popup = _comboPopupPool.Get();
        popup.Show(currentComboValue, _popupPositions[currentComboValue - 1]);
        popup.Showed += OnComboPopupShowed;
    }

    private void OnComboPopupShowed(ComboPopup popup)
    {
        popup.Showed -= OnComboPopupShowed;
        _comboPopupPool.Return(popup);
    }
}
