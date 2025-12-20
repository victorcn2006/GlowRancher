using UnityEngine;
using UnityEngine.InputSystem;

public class CursorManager : MonoBehaviour
{
    public static CursorManager instance { get; private set; }

    private Vector2 lastMousePosition;
    private UIInputManager inputManager;

    private void Awake() {
        if(instance == null) { 
            instance = this;
        }
        else Destroy(this.gameObject);
    }

    private void Start() {
        inputManager = UIInputManager.Instance;

        if (inputManager == null)
        {
            Debug.LogError("CursorToggle: UIInputManager instance not found!");
            enabled = false;
            return;
        }

        lastMousePosition = Mouse.current?.position.ReadValue() ?? Vector2.zero;
    }

    private void Update() {
        if (inputManager == null) return;

        if (HasMouseInput())
        {
            Cursor.visible = true;
            lastMousePosition = Mouse.current.position.ReadValue();
        }

        else if (HasKeyboardOrGamepadInput())
        {
            Cursor.visible = false;
        }
    }
    bool HasMouseInput() {
        if (Mouse.current == null) return false;

        Vector2 currentMousePos = Mouse.current.position.ReadValue();

        return currentMousePos != lastMousePosition ||
               inputManager.IsClickPressed ||
               inputManager.IsMiddleClickPressed ||
               inputManager.IsRightClickPressed ||
               Mouse.current.scroll.ReadValue().y != 0;
    }

    bool HasKeyboardOrGamepadInput() {
        // Check navigation input (arrows, WASD, gamepad stick)
        if (inputManager.NavigateInput.magnitude > 0.1f)
            return true;

        // Check keyboard input
        if (Keyboard.current != null && Keyboard.current.anyKey.isPressed)
        {
            // Ignore mouse buttons
            if (!inputManager.IsClickPressed &&
                !inputManager.IsMiddleClickPressed &&
                !inputManager.IsRightClickPressed)
                return true;
        }

        // Check gamepad buttons
        if (Gamepad.current != null)
        {
            return Gamepad.current.buttonSouth.isPressed ||
                   Gamepad.current.buttonEast.isPressed ||
                   Gamepad.current.buttonWest.isPressed ||
                   Gamepad.current.buttonNorth.isPressed ||
                   Gamepad.current.leftShoulder.isPressed ||
                   Gamepad.current.rightShoulder.isPressed;
        }

        return false;
    }
};
