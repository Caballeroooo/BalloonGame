using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LeaderBoardLineView : MonoBehaviour, IPoolable
{
    [SerializeField] private CanvasGroup _canvasGroup;
    [SerializeField] private Image _background;
    [SerializeField] private TMP_Text _nickname;
    [SerializeField] private TMP_Text _bestScore;
    [SerializeField] private TMP_Text _place;

    private RectTransform CanvasGroupRectTransform => _canvasGroup.transform as RectTransform;

    public void Initialize()
    {
        Hide();
    }

    public void SetParent(Transform parent)
    {
        transform.SetParent(parent);
    }

    public void SetNickname(string nickname)
    {
        _nickname.text = nickname;
    }

    public void SetPlace(int place)
    {
        _place.text = place.ToString();
    }

    public void SetBackgroundColor(Color color)
    {
        _background.color = color;
    }

    public void SetBestScore(int score)
    {
        _bestScore.text = score.ToString();
    }

    public Sequence Show(float duration)
    {
        var sequence = DOTween.Sequence();
        sequence.Join(_canvasGroup.DOFade(1, duration).SetEase(Ease.OutCubic));
        sequence.Join(CanvasGroupRectTransform.DOAnchorPos(Vector2.zero, duration).SetEase(Ease.OutCubic));

        return sequence;
    }

    public void Hide()
    {
        _canvasGroup.alpha = 0f;
    }

    public void PreparePosition(float xOffset)
    {
        CanvasGroupRectTransform.anchoredPosition += Vector2.right * xOffset;
    }
}