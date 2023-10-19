using UnityEngine;

public interface IPoolable
{
    void Initialize();
    void SetParent(Transform parent);
}
