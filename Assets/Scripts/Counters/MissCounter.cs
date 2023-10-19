using System;

public class MissCounter : BaseCounter
{
    public event Action Reached;

    public int Max => GameplaySettings.MissForGameOver;

    public override void Increase()
    {
        base.Increase();
        if (Current >= Max)
        {
            Reached?.Invoke();
        }
    }
}