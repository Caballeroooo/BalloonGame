using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Canvas))]
[RequireComponent(typeof(GraphicRaycaster))]
public abstract class ScreenBase : MonoBehaviour
{
    [SerializeField] private Canvas _canvas;

    public virtual void Show()
    {
        _canvas.enabled = true;
    }

    public virtual void Hide()
    {
        _canvas.enabled = false;
    }

    public void FastHide()
    {
        _canvas.enabled = false;
    }

#if UNITY_EDITOR
    public void SetRefs()
    {
        _canvas = GetComponent<Canvas>();
    }
#endif
}