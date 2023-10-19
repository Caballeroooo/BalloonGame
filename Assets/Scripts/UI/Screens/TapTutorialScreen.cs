using UnityEngine;

public class TapTutorialScreen : ScreenBase
{
    [SerializeField] private Animator _animator;

    private void Start()
    {
        _animator.enabled = false;
    }

    public override void Show()
    {
        base.Show();
        _animator.enabled = true;
    }

    public override void Hide()
    {
        base.Hide();
        _animator.enabled = false;
    }
}
