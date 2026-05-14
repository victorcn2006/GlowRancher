using UnityEngine;
using UnityEditor;
using UnityEditor.Formats.Fbx.Exporter;
using System;
using System.IO;

enum SaveResolution
{
    Full = 0,
    Half,
    Quarter,
    Eighth,
    Sixteenth
}

public class ExportTerrain : EditorWindow
{
    SaveResolution saveResolution = SaveResolution.Half;
    int textureResolution = 2048;

    static TerrainData terrain;
    static Terrain terrainComp;
    static Vector3 terrainPos;

    [MenuItem("Terrain/Export To FBX...")]
    static void Init()
    {
        terrain = null;

        terrainComp = Selection.activeGameObject?.GetComponent<Terrain>();

        if (!terrainComp)
            terrainComp = Terrain.activeTerrain;

        if (terrainComp)
        {
            terrain = terrainComp.terrainData;
            terrainPos = terrainComp.transform.position;
        }

        var window = GetWindow<ExportTerrain>();
        window.titleContent = new GUIContent("Terrain Export");
        window.Show();
    }

    void OnGUI()
    {
        if (!terrain || !terrainComp)
        {
            GUILayout.Label("No terrain found. Please select a Terrain object.");

            if (GUILayout.Button("Close"))
                Close();

            return;
        }

        saveResolution = (SaveResolution)EditorGUILayout.EnumPopup("Mesh Resolution", saveResolution);
        textureResolution = EditorGUILayout.IntField("Texture Resolution", textureResolution);

        if (GUILayout.Button("Export To FBX"))
        {
            Export();
        }
    }

    void Export()
    {
        string fileName = EditorUtility.SaveFilePanel("Export FBX", "", terrainComp.name, "fbx");
        if (string.IsNullOrEmpty(fileName))
            return;

        try
        {
            EditorUtility.DisplayProgressBar("Exporting", "Generating terrain mesh...", 0.2f);

            Mesh mesh = GenerateMesh();

            string texturePath = Path.ChangeExtension(fileName, "png");
            BakeTexture(texturePath);

            AssetDatabase.Refresh();

            GameObject exportGo = new GameObject(terrainComp.name + "_Export");
            exportGo.transform.position = terrainPos;

            exportGo.AddComponent<MeshFilter>().sharedMesh = mesh;

            MeshRenderer mr = exportGo.AddComponent<MeshRenderer>();

            Material tempMat = new Material(Shader.Find("Standard"));

            string relativeTexPath = FileUtil.GetProjectRelativePath(texturePath);
            Texture2D bakedTex = AssetDatabase.LoadAssetAtPath<Texture2D>(relativeTexPath);

            if (bakedTex != null)
                tempMat.mainTexture = bakedTex;

            mr.sharedMaterial = tempMat;

            EditorUtility.DisplayProgressBar("Exporting", "Writing FBX...", 0.9f);

            // ✅ NUEVO API (sin options)
            ModelExporter.ExportObject(fileName, exportGo);

            Debug.Log($"FBX exported successfully:\n{fileName}");
            Debug.Log($"Texture exported successfully:\n{texturePath}");

            DestroyImmediate(exportGo);
            DestroyImmediate(mesh);
        }
        catch (Exception e)
        {
            Debug.LogError($"Export failed:\n{e}");
        }
        finally
        {
            EditorUtility.ClearProgressBar();
        }

        Close();
    }

    Mesh GenerateMesh()
    {
        int res = terrain.heightmapResolution;
        int tRes = (int)Mathf.Pow(2, (int)saveResolution);

        int w = (res - 1) / tRes + 1;
        int h = (res - 1) / tRes + 1;

        Vector3 meshScale = terrain.size;
        float[,] tData = terrain.GetHeights(0, 0, res, res);

        Vector3[] vertices = new Vector3[w * h];
        Vector2[] uv = new Vector2[w * h];
        int[] triangles = new int[(w - 1) * (h - 1) * 6];

        float uvStepX = 1f / (w - 1);
        float uvStepY = 1f / (h - 1);

        for (int y = 0; y < h; y++)
        {
            for (int x = 0; x < w; x++)
            {
                int index = y * w + x;

                float normX = x * uvStepX;
                float normY = y * uvStepY;

                int hx = Mathf.Min(x * tRes, res - 1);
                int hy = Mathf.Min(y * tRes, res - 1);

                float yPos = tData[hy, hx] * meshScale.y;

                vertices[index] = new Vector3(
                    normX * meshScale.x,
                    yPos,
                    normY * meshScale.z
                );

                uv[index] = new Vector2(normX, normY);
            }
        }

        int triIndex = 0;

        for (int y = 0; y < h - 1; y++)
        {
            for (int x = 0; x < w - 1; x++)
            {
                int v = y * w + x;

                triangles[triIndex++] = v;
                triangles[triIndex++] = v + w;
                triangles[triIndex++] = v + 1;

                triangles[triIndex++] = v + 1;
                triangles[triIndex++] = v + w;
                triangles[triIndex++] = v + w + 1;
            }
        }

        Mesh mesh = new Mesh();
        mesh.name = terrain.name + "_Mesh";
        mesh.vertices = vertices;
        mesh.uv = uv;
        mesh.triangles = triangles;

        mesh.RecalculateNormals();
        mesh.RecalculateBounds();

        return mesh;
    }

    void BakeTexture(string savePath)
    {
        RenderTexture rt = RenderTexture.GetTemporary(textureResolution, textureResolution, 24);

        GameObject camGo = new GameObject("BakeCam");
        Camera cam = camGo.AddComponent<Camera>();

        cam.orthographic = true;
        cam.targetTexture = rt;
        cam.clearFlags = CameraClearFlags.Color;
        cam.backgroundColor = Color.black;
        cam.cullingMask = ~0;

        camGo.transform.position =
            terrainPos +
            new Vector3(terrain.size.x * 0.5f, terrain.size.y + 10f, terrain.size.z * 0.5f);

        camGo.transform.rotation = Quaternion.LookRotation(Vector3.down);

        cam.orthographicSize = terrain.size.z * 0.5f;
        cam.aspect = terrain.size.x / terrain.size.z;

        cam.Render();

        RenderTexture.active = rt;

        Texture2D tex = new Texture2D(textureResolution, textureResolution, TextureFormat.RGB24, false);
        tex.ReadPixels(new Rect(0, 0, textureResolution, textureResolution), 0, 0);
        tex.Apply();

        File.WriteAllBytes(savePath, tex.EncodeToPNG());

        RenderTexture.active = null;
        RenderTexture.ReleaseTemporary(rt);

        DestroyImmediate(camGo);
        DestroyImmediate(tex);

        AssetDatabase.Refresh();
    }
}
