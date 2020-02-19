using UnityEngine;
using UnityEditor;

public class InstantiateSO
{
    [MenuItem("SOTools/CreateScriptableObject")]
    public static void CreateScriptableObject()
    {
        string assetExtension = "asset";
        Object[] selection = Selection.objects;
        
        foreach (Object selectedObject in selection)
        {
            if (!(selectedObject is GameObject))
            {
                try
                {
                    ScriptableObject createdObject = ScriptableObject.CreateInstance(selectedObject.name);
                    string path = EditorUtility.SaveFilePanel("Save scriptable object", Application.dataPath, selectedObject.name, assetExtension);
                    int indexOfAssetsPath = path.IndexOf("Assets");
                    path = path.Remove(0, indexOfAssetsPath);

                    if (path.Length != 0)
                        AssetDatabase.CreateAsset(createdObject, path);
                }
                catch
                {
                    EditorUtility.DisplayDialog("Operation Failed!", "ScriptableObject creation is failed ! Make sure that selected asset derives from ScriptableObject class.", "Ok");
                }
            }
            else
            {
                EditorUtility.DisplayDialog("Operation Failed!", "ScriptableObject creation is failed ! Select a ScriptableObject child from assets , not a scene!", "Ok");
            }
        }
    }
}