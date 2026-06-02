using UnityEngine;
using UnityEditor;

public class FindMissingScripts : EditorWindow
{
    [MenuItem("Edit/Find Missing Scripts")]
    public static void ShowWindow()
    {
        GetWindow<FindMissingScripts>("Find Missing Scripts");
    }

    private void OnGUI()
    {
        if (GUILayout.Button("Find and Remove Missing Scripts"))
        {
            FindAndDeleteMissingScripts();
        }
    }

    private static void FindAndDeleteMissingScripts()
    {
        int totalMissingCount = 0;

        foreach (GameObject gameObject in GameObject.FindObjectsOfType<GameObject>(true))
        {
            int count = GameObjectUtility.GetMonoBehavioursWithMissingScriptCount(gameObject);
            if (count > 0)
            {
                GameObjectUtility.RemoveMonoBehavioursWithMissingScript(gameObject);
                totalMissingCount += count;
                Debug.Log($"Removed {count} missing scripts from: {gameObject.name}", gameObject);
            }
        }

        Debug.Log($"Operation Complete. Total missing scripts removed: {totalMissingCount}");
    }
}