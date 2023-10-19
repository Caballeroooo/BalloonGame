using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ObjectPool<T> : MonoBehaviour where T : Object, IPoolable
{
    [SerializeField] private T _prefab;
    [SerializeField] private int _poolSize;
    [SerializeField] private List<T> _objects;

    public T Get()
    {
        for (var i = 0; i < _objects.Count;)
        {
            var obj = _objects[i];
            _objects.Remove(obj);
            return obj;
        }

        return AddNew();
    }

    public void Return(T obj)
    {
        _objects.Add(obj);
        obj.SetParent(transform);
    }

    private T AddNew()
    {
        var newObject = Instantiate(_prefab, transform);
        return newObject;
    }

#if UNITY_EDITOR
    [ContextMenu("InitPool")]
    protected virtual void InitPool()
    {
        ClearPool();
        FillPool();
        EditorUtility.SetDirty(this);
    }

    private void ClearPool()
    {
        var gameObjects = new List<GameObject>();
        var children = GetComponentsInChildren<T>();

        for (var i = 0; i < children.Length; i++)
        {
            gameObjects.Add(transform.GetChild(i).gameObject);
        }

        foreach (var go in gameObjects)
        {
            DestroyImmediate(go);
        }

        _objects.Clear();
    }

    private void FillPool()
    {
        for (var i = 0; i < _poolSize; i++)
        {
            var newObject = AddNew();
            _objects.Add(newObject);
            newObject.Initialize();
        }
    }
#endif
}
