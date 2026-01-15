using UnityEngine;
using UnityEngine.UI;

#if UNITY_EDITOR
using UnityEditor;
#endif
public class SceneChangerButton : SceneChanger {
    private Button _button;

    private void Reset() {
    #if UNITY_EDITOR
        Undo.RegisterCompleteObjectUndo(this, "Reset");
        if (!_button) _button = GetComponent<Button>();
    #endif
    }

    private void OnEnable() {
        if (!_button) _button = GetComponent<Button>();
        _button.onClick.AddListener(ChangeScene);
    }

    private void OnDisable() {
        if (!_button) _button = GetComponent<Button>();
        if (_button) _button.onClick.RemoveListener(ChangeScene);
    }
}
