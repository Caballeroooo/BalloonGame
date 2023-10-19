using DG.Tweening;
using UnityEngine;

public class MissCounterView : MonoBehaviour
{
    [SerializeField] private MissCounter _missCounter;
    [SerializeField] private CanvasGroup _canvasGroup;
    [SerializeField] private MissCounterItemView[] _items;
    [SerializeField] private float _showDuration = 0.3f;
    [SerializeField] private float _hideDuration = 0.5f;
    [SerializeField] private float _hideDelay = 1f;

    private int _currentIndex;
    
    private void OnEnable()
    {
        Subscribe();
    }

    private void Start()
    {
        _canvasGroup.alpha = 0f;
        HideAllItems();
        ShowItems(_missCounter.Max);
    }

    private void OnDisable()
    {
        Unsubscribe();
    }

    private void OnDestroy()
    {
        _canvasGroup.DOKill();
    }

    private void Subscribe()
    {
        _missCounter.Changed += OnCounterValueChanged;
    }

    private void Unsubscribe()
    {
        _missCounter.Changed -= OnCounterValueChanged;

        foreach (var item in _items)
        {
            item.FillCompleted -= OnItemFillCompleted;
        }
    }

    private void HideAllItems()
    {
        foreach (var item in _items)
        {
            item.Hide();
        }
    }

    private void ShowItems(int count)
    {
        for (int i = 0; i < count; i++)
        {
            _items[i].Show();
        }
    }
    
    private void OnCounterValueChanged(int value)
    {
        _canvasGroup.DOKill();
        var previousIndex = _currentIndex - 1;
        if (previousIndex > 0)
        {
            _items[_currentIndex].FillCompleted -= OnItemFillCompleted;
        }
        Show();
        FillNextItem();
    }

    private void FillNextItem()
    {
        _items[_currentIndex].Fill();
        _items[_currentIndex].FillCompleted += OnItemFillCompleted;
        _currentIndex++;
    }

    private void OnItemFillCompleted()
    {
        Hide();
    }

    private void Show()
    {
        _canvasGroup.DOFade(1f, _showDuration);
    }

    private void Hide()
    {
        _canvasGroup.DOFade(0f, _hideDuration).SetDelay(_hideDelay);
    }
}