using UnityEngine;

public class BalloonKillVFXPool : ObjectPool<BalloonKillVFX>
{
#if UNITY_EDITOR
    [ContextMenu("InitPool")]
    protected override void InitPool()
    {
        base.InitPool();
    }
#endif
}