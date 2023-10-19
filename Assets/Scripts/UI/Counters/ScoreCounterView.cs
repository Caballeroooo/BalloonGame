using TMPro;
using UnityEngine;

public class ScoreCounterView : MonoBehaviour
{
    [SerializeField] private ScoreCounter _scoreCounter;
    [SerializeField] private TMP_Text _text;

    private void OnEnable()
    {
        _scoreCounter.Changed += OnChanged;
    }

    private void Start()
    {
        _text.text = "0";
    }

    private void OnDisable()
    {
        _scoreCounter.Changed -= OnChanged;
    }

    private void OnChanged(int newValue)
    {
        _text.text = newValue.ToString();
    }
}
