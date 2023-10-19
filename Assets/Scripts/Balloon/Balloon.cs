using System;
using UnityEngine;
using DG.Tweening;

public class Balloon : MonoBehaviour, IClickable, IPoolable
{
    [SerializeField] private BalloonKillAnimation _killAnimation;
    [SerializeField] private Collider2D _collider;
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private float _showAnimationDuration = 0.5f;
    [SerializeField] private Ease[] _eases;
    
    public event Action<Balloon> Killed;
    public event Action<Balloon> ForceKilled;
    public event Action<Balloon> MoveCompleted;

    public Color Color { get; private set; }
    public Vector2 Position => transform.position;
    public Bounds Bounds => _spriteRenderer.bounds;

    private void OnEnable()
    {
        Subscribe();
    }

    private void OnDisable()
    {
        Unsubscribe();
    }

    private void OnDestroy()
    {
        StopMoving();
    }

    public void Initialize()
    {
        Hide();
    }

    public void SetColor(Color color)
    {
        Color = color;
        _spriteRenderer.color = color;
    }

    public void Show()
    {
        _spriteRenderer.enabled = true;
        _collider.enabled = true;
        transform.DOScale(Vector3.one, _showAnimationDuration).SetEase(Ease.OutBack);
    }

    public void Hide()
    {
        _spriteRenderer.enabled = false;
        _collider.enabled = false;
        transform.localScale = Vector3.zero;
    }

    public void Spawn(Vector2 position)
    {
        SetParent(null);
        transform.position = position;
    }
    
    public void SetParent(Transform parent)
    {
        transform.SetParent(parent);
    }

    public void StartMoving(Vector2 endPosition, float speed)
    {
        var duration = (endPosition.y - transform.position.y) / speed;
        transform.DOMoveY(endPosition.y, duration).SetEase(_eases.GetRandom()).OnComplete(() => MoveCompleted?.Invoke(this));
    }

    public void StopMoving()
    {
        transform.DOKill();
    }

    public void OnClick()
    {
        Kill();
    }

    public void ForceKill()
    {
        StopMoving();
        ForceKilled?.Invoke(this);
    }

    public void Kill()
    {
        StopMoving();
        SoundManager.Instance.PlayOneShot(TrackName.Pop);
        _killAnimation.Play();
        Killed?.Invoke(this);
    }

    private void Subscribe()
    {
        _killAnimation.Ended += OnKillAnimationEnded;
    }

    private void Unsubscribe()
    {
        _killAnimation.Ended -= OnKillAnimationEnded;
    }

    private void OnKillAnimationEnded()
    {
        Hide();
    }
}