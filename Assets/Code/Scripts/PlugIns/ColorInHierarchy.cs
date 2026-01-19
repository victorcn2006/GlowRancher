using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;

[InitializeOnLoad]

public class ColorInHierarchy : MonoBehaviour {

    #region [ static - Update editor ]
    private static readonly Dictionary<GameObject, Color> coloredText = new();
    private static readonly Dictionary<GameObject, Color> coloredBackground = new();
    private static readonly Dictionary<GameObject, Sprite> coloredIcon = new();

    private static readonly Vector2 offset = new(18, 0);
    private static readonly Color windowBackground = new(0.2196079f, 0.2196079f, 0.2196079f, 1f);

    static ColorInHierarchy()
    {
        EditorApplication.hierarchyWindowItemOnGUI += HandleHierarchyWindowItemOnGUI;
    }

    private static void HandleHierarchyWindowItemOnGUI(int instanceID, Rect selectionRect)
    {
        GameObject obj = EditorUtility.InstanceIDToObject(instanceID) as GameObject;
        if (obj == null) return;

        // Fondo
        if (coloredBackground.ContainsKey(obj))
        {
            if (obj.GetComponent<ColorInHierarchy>())
                HierarchyBackground(selectionRect, coloredBackground[obj]);
            else
                coloredBackground.Remove(obj);
        }

        // Icono
        if (coloredIcon.ContainsKey(obj))
        {
            if (obj.GetComponent<ColorInHierarchy>())
            {
                HierarchyIconBg(selectionRect);
                HierarchyIcon(selectionRect, coloredIcon[obj]);
            }
            else
                coloredIcon.Remove(obj);
        }

        // Texto
        if (coloredText.ContainsKey(obj))
        {
            if (obj.GetComponent<ColorInHierarchy>())
                HierarchyText(obj, selectionRect, coloredText[obj]);
            else
                coloredText.Remove(obj);
        }
    }

    private static void HierarchyText(UnityEngine.Object obj, Rect selectionRect, Color colorText)
    {
        Rect offsetRect = new(selectionRect.position + offset, selectionRect.size);

        EditorGUI.LabelField(offsetRect, obj.name, new GUIStyle
        {
            normal = new GUIStyleState { textColor = colorText },
            fontStyle = FontStyle.Normal
        });
    }

    private static void HierarchyBackground(Rect selectionRect, Color colorBackground)
    {
        Rect bgRect = new(selectionRect.x, selectionRect.y, selectionRect.width + 50, selectionRect.height);
        EditorGUI.DrawRect(bgRect, colorBackground);
    }

    private static void HierarchyIconBg(Rect selectionRect)
    {
        Rect icRect = new(selectionRect.x - 2, selectionRect.y, 20, 18);
        EditorGUI.DrawRect(icRect, windowBackground);
    }

    private static void HierarchyIcon(Rect selectionRect, Sprite colorIcon)
    {
        if (colorIcon == null || colorIcon.texture == null) return;

        Rect icRect = new(selectionRect.x - 2, selectionRect.y + 1, 16, 16);
        GUI.Label(icRect, colorIcon.texture);
    }
    #endregion

    [Header("Hierarchy Appearance")]
    public Sprite icon;

    private void Reset() => SetAppearance();
    private void OnValidate() => SetAppearance();
    private void Awake() => SetAppearance();

    private void SetAppearance(){
        if (icon != null) coloredIcon[gameObject] = icon;
    }

}

#endif
