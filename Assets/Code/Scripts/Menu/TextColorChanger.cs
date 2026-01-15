using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public class TextColorChanger : MonoBehaviour {
    private TextMeshProUGUI _textToChange;

    private void Awake() {
        if (_textToChange == null) _textToChange = GetComponentInChildren<TextMeshProUGUI>();
    }

    void Update() {
        if (_textToChange == null) return;
        //Selected color
        if (EventSystem.current.currentSelectedGameObject == gameObject) _textToChange.color = Color.red;
        //Default color
        else _textToChange.color = Color.white;
    }
}
