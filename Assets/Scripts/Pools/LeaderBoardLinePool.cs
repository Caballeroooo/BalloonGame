using UnityEngine;

public class LeaderBoardLinePool : ObjectPool<LeaderBoardLineView>
{
#if UNITY_EDITOR
    [ContextMenu("InitPool")]
    protected override void InitPool()
    {
        base.InitPool();
    }
#endif
}
