using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(RequiredFieldAttribute))]
public class RequiredFieldDrawer : SceneFieldPropertyDrawer
{
    Texture2D requiredIcon = AssetDatabase.LoadAssetAtPath<Texture2D>("Assets");

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginProperty(position, label, property);
        EditorGUI.BeginChangeCheck();
        Rect fieldRect = new(position.x, position.y, position.z);
    }
}
