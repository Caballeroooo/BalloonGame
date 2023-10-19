using System;
using TMPro;
using UnityEngine;

public class FirstMeetScreen : ScreenBase
{
    [SerializeField] private TMP_InputField _inputField;
    [SerializeField] private AnimatedButton _applyButton;

    public event Action<string> ApplyButtonClicked;
    
    private void OnEnable()
    {
        _applyButton.Clicked += OnApplyButtonClicked;
        _inputField.onValueChanged.AddListener(OnInputFieldValueChanged);
    }

    private void Start()
    {
        _applyButton.Disable();
    }

    private void OnDisable()
    {
        _applyButton.Clicked -= OnApplyButtonClicked;
        _inputField.onValueChanged.RemoveListener(OnInputFieldValueChanged);
    }

    private void OnApplyButtonClicked()
    {
        ApplyButtonClicked?.Invoke(_inputField.text);
    }

    private void OnInputFieldValueChanged(string inputFieldValue)
    {
        if (string.IsNullOrEmpty(inputFieldValue))
        {
            _applyButton.Disable();
        }
        else if (inputFieldValue.Length == 1)
        {
            _applyButton.Enable();
        }
    }
}
