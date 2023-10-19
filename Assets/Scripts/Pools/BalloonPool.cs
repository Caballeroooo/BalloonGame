using UnityEngine;

public class BalloonPool : ObjectPool<Balloon>
{
#if UNITY_EDITOR
    [ContextMenu("InitPool")]
    protected override void InitPool()
    {
        base.InitPool();
    }
#endif
}