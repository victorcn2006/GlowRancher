using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.IO;

public class BuildingRefactorWindow : EditorWindow
{
    [MenuItem("GlowRancher/Refactor Buildings")]
    public static void ShowWindow()
    {
        GetWindow<BuildingRefactorWindow>("Building Refactor");
    }

    private void OnGUI()
    {
        GUILayout.Label("Refactor Buildings to New System", EditorStyles.boldLabel);
        
        if (GUILayout.Button("Refactor All Buildings"))
        {
            RefactorBuildings();
        }
    }

    private void RefactorBuildings()
    {
        string[] prefabGuids = AssetDatabase.FindAssets("t:Prefab", new[] { "Assets/Level/Prefabs/Buildings" });
        
        foreach (string guid in prefabGuids)
        {
            string path = AssetDatabase.GUIDToAssetPath(guid);
            GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject>(path);
            
            if (prefab == null) continue;

            // Check if it already has the BuildingSystem structure
            if (prefab.name == "BuildingSystem" || prefab.GetComponent<EditBuilding>() != null)
            {
                Debug.Log($"[Refactor] {prefab.name} already processed or has EditBuilding.");
                // We might still need to rename or ensure trigger
                ProcessPrefab(path);
            }
            else
            {
                // If it's a building that should be refactored
                if (prefab.GetComponentInChildren<Building>() != null)
                {
                    ProcessPrefab(path);
                }
            }
        }
        
        AssetDatabase.SaveAssets();
        Debug.Log("[Refactor] Finished refactoring buildings.");
    }

    private void ProcessPrefab(string path)
    {
        GameObject root = PrefabUtility.LoadPrefabContents(path);
        bool changed = false;

        // 1. Ensure Root Name is BuildingSystem
        if (root.name != "BuildingSystem")
        {
            root.name = "BuildingSystem";
            changed = true;
        }

        // 2. Ensure EditBuilding component exists
        EditBuilding editComp = root.GetComponent<EditBuilding>();
        if (editComp == null)
        {
            editComp = root.AddComponent<EditBuilding>();
            changed = true;
        }

        // 3. Ensure BoxCollider isTrigger exists
        BoxCollider boxCol = root.GetComponent<BoxCollider>();
        if (boxCol == null)
        {
            boxCol = root.AddComponent<BoxCollider>();
            changed = true;
        }
        
        if (!boxCol.isTrigger)
        {
            boxCol.isTrigger = true;
            changed = true;
        }

        // 4. Try to auto-assign models if they are null
        // This is speculative but helps
        var serializedObj = new SerializedObject(editComp);
        var buildingModelProp = serializedObj.FindProperty("_buildingModel");
        var hologramModelProp = serializedObj.FindProperty("_hologramModel");

        if (buildingModelProp.objectReferenceValue == null)
        {
            // Try to find a child that is not the hologram
            foreach (Transform child in root.transform)
            {
                if (!child.name.ToLower().Contains("hologram"))
                {
                    buildingModelProp.objectReferenceValue = child.gameObject;
                    break;
                }
            }
        }

        if (hologramModelProp.objectReferenceValue == null)
        {
            foreach (Transform child in root.transform)
            {
                if (child.name.ToLower().Contains("hologram"))
                {
                    hologramModelProp.objectReferenceValue = child.gameObject;
                    break;
                }
            }
        }

        if (serializedObj.ApplyModifiedProperties())
        {
            changed = true;
        }

        if (changed)
        {
            PrefabUtility.SaveAsPrefabAsset(root, path);
            Debug.Log($"[Refactor] Updated {path}");
        }

        PrefabUtility.UnloadPrefabContents(root);
    }
}
