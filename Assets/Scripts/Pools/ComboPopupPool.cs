using UnityEngine;

public class ComboPopupPool : ObjectPool<ComboPopup>
{
#if UNITY_EDITOR
    [ContextMenu("InitPool")]
    protected override void InitPool()
    {
        base.InitPool();
    }
#endif
}
