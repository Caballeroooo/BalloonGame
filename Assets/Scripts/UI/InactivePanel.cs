using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(LayoutElement))]
public class InactivePanel : MonoBehaviour
{
    [SerializeField] private LayoutElement _inactivePanel;

    private void Awake()
    {
        Init();
        var height = Screen.height - Screen.safeArea.y - Screen.safeArea.height;
        _inactivePanel.minHeight = height;
    }

    private void OnValidate()
    {
        Init();
    }

    private void Init()
    {
        if (_inactivePanel == null)
        {
            _inactivePanel = GetComponent<LayoutElement>();
        }
    }
}