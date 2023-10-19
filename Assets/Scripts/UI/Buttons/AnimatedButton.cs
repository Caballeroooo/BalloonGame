using UnityEngine;

[RequireComponent(typeof(ButtonAnimation))]
public class AnimatedButton : DefaultButton
{
    [SerializeField] private ButtonAnimation _animation;

    public override void Enable()
    {
        base.Enable();
        _animation.Enable();
    }

    public override void Disable()
    {
        base.Disable();
        _animation.Disable();
    }
}
