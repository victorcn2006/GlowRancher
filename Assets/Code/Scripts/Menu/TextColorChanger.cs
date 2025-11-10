using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public class TextColorChanger : MonoBehaviour {
    private TextMeshProUGUI textToChange;

    private void Awake() {
        if (textToChange == null) textToChange = GetComponentInChildren<TextMeshProUGUI>();
    }

    void Update() {
        if (textToChange == null) return;
        //Selected color
        if (EventSystem.current.currentSelectedGameObject == gameObject) textToChange.color = Color.red;
        //Default color
        else textToChange.color = Color.white;
    }
}