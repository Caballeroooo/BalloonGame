using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

[CustomEditor(typeof(TimeScaler))]
public class TimeScalerEditor : Editor
{
    private const string ObjectName = "TimeScaler";
    private const string MenuPath = "GameObject/TimeScaler";

    [MenuItem(MenuPath)]
    public static void Create()
    {
        var gameObject = new GameObject(ObjectName);
        gameObject.transform.SetParent(Selection.activeTransform);
        gameObject.AddComponent<TimeScaler>();
        EditorUtility.SetDirty(gameObject);
        EditorSceneManager.MarkSceneDirty(gameObject.scene);
    }
}