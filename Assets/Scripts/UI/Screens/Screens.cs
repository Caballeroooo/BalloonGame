using System;
using UnityEditor;
using UnityEngine;

public class Screens : MonoBehaviour
{
    [SerializeField] private ScreenBase[] _screens;

    private void Awake()
    {
        EnableGameObjectScreens();
        DisableScreens();
    }

    public T GetScreen<T>() where T : ScreenBase
    {
        for (int i = 0; i < _screens.Length; i++)
        {
            if (_screens[i].GetType() == typeof(T))
            {
                return (T)_screens[i];
            }
        }

        throw new Exception("Can't find screen");
    }

    public void DisableScreens()
    {
        for (int i = 0; i < _screens.Length; ++i)
        {
            _screens[i].FastHide();
        }
    }

    private void EnableGameObjectScreens()
    {
        for (int i = 0; i < _screens.Length; ++i)
        {
            _screens[i].gameObject.SetActive(true);
        }
    }

#if UNITY_EDITOR
    [ContextMenu("Set Refs")]
    private void SetRefs()
    {
        _screens = GetComponentsInChildren<ScreenBase>(true);
        foreach (var screenBase in _screens)
        {
            screenBase.SetRefs();
        }
        EditorUtility.SetDirty(this);
    }
#endif
}