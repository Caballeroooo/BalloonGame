using System;

public class ComboCounter : BaseCounter
{
    public event Action Reached;

    public override void Increase()
    {
        base.Increase();
        if (Current >= GameplaySettings.MaxComboForBonus)
        {
            Reached?.Invoke();
            Reset();
        }
    }

    public void Reset()
    {
        Current = 0;
    }
}