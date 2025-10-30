using UnityEngine;
using UnityEngine.UI;

public class MenuButtonOptions : MonoBehaviour {
    public enum ButtonType {
        Exit,
        Start,
        Options
    }

    [SerializeField] private ButtonType buttonType;
    private Button button;

    private void Awake() {
        button = GetComponent<Button>();

        if (button != null)
        {
            button.onClick.AddListener(() => OnButtonClicked(buttonType));
        }
    }

    private void OnButtonClicked(ButtonType type) {
        switch (type)
        {
            case ButtonType.Exit:
                Exit();
                break;

            case ButtonType.Start:
                break;

            case ButtonType.Options:
                break;
        }
    }

    private void Exit() {
        Debug.Log("Saliendo del juego...");
        Application.Quit();
    }

}
