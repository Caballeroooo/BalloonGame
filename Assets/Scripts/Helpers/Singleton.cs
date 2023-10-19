using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : Singleton<T>
{
    private static T _instance;

    public static T Instance
    {
        get => _instance;

        private set
        {
            if (_instance != null && value != null)
            {
                Debug.LogError($"There is another instance of {typeof(T)} object in the scene.");
            }

            _instance = value;
        }
    }

    protected virtual void Awake() => Instance = (T)this;

    protected virtual void OnDestroy() => Instance = null;
}