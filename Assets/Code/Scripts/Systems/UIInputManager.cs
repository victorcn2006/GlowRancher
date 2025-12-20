using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class UIInputManager : MonoBehaviour
{
    public static UIInputManager Instance { get; private set; }

    [SerializeField] private InputActionAsset _controlsInputActionAsset;

    private InputActionMap uiInputs;

    #region UI Input Actions
    private InputAction click;
    private InputAction middleClick;
    private InputAction rightClick;
    private InputAction navigate;
    private InputAction submit;
    private InputAction cancel;
    private InputAction pauseGame;
    #endregion

    #region Public Properties
    public bool isPaused { get; private set; } = false;
    public bool IsClickPressed => click?.IsPressed() ?? false;
    public bool IsMiddleClickPressed => middleClick?.IsPressed() ?? false;
    public bool IsRightClickPressed => rightClick?.IsPressed() ?? false;
    public Vector2 NavigateInput => navigate?.ReadValue<Vector2>() ?? Vector2.zero;

    public event System.Action<bool> OnPauseStateChanged;
    #endregion


    private void Awake() {
        if (Instance == null) {
            Instance = this;
        } 
        else Destroy(this.gameObject);
        InitializeInputs();
    }

    private void OnEnable() {
        uiInputs?.Enable();
    }
    private void OnDisable() {
        uiInputs.Disable();
    }

    #region InputReferences
    private void InitializeInputs() {

        if (_controlsInputActionAsset == null)
        {
            Debug.LogError("UIInputManager: InputActionAsset is not assigned!");
            return;
        }

        uiInputs = _controlsInputActionAsset.FindActionMap("UI");

        if (uiInputs == null)
        {
            Debug.LogError("UIInputManager: 'UI' ActionMap not found in InputActionAsset!");
            return;
        }

        click = GetAction("Click");
        middleClick = GetAction("MiddleClick");
        rightClick = GetAction("RightClick");
        navigate = GetAction("Navigate");
        submit = GetAction("Submit");
        cancel = GetAction("Cancel");
        pauseGame = GetAction("PauseGame");
        SubscribeToEvents();
    }

    private InputAction GetAction(string actionName) {
        var action = uiInputs.FindAction(actionName);
        if (action == null)
        {
            Debug.LogWarning($"UIInputManager: Action '{actionName}' not found in UI ActionMap!");
        }
        return action;
    }
    #endregion

    #region Event Subscription
    private void SubscribeToEvents() {
        if (click != null)
        {
            click.performed += OnClick;
            click.canceled += OnClickReleased;
        }

        if (middleClick != null)
        {
            middleClick.performed += OnMiddleClick;
        }

        if (rightClick != null)
        {
            rightClick.performed += OnRightClick;
        }

        if (submit != null)
        {
            submit.performed += OnSubmit;
        }

        if (cancel != null)
        {
            cancel.performed += OnCancel;
        }
        if (pauseGame != null)
        {
            pauseGame.performed += OnPauseGame;
        }
    }

    private void UnsubscribeFromEvents() {
        if (click != null)
        {
            click.performed -= OnClick;
            click.canceled -= OnClickReleased;
        }

        if (middleClick != null)
        {
            middleClick.performed -= OnMiddleClick;
        }

        if (rightClick != null)
        {
            rightClick.performed -= OnRightClick;
        }

        if (submit != null)
        {
            submit.performed -= OnSubmit;
        }

        if (cancel != null)
        {
            cancel.performed -= OnCancel;
        }
        if (pauseGame != null)
        {
            pauseGame.performed -= OnPauseGame;
        }
    }
    #endregion

    #region Input Callbacks
    private void OnClick(InputAction.CallbackContext ctx) {
        // Handle click logic or broadcast event
        Debug.Log("Click performed");
    }

    private void OnClickReleased(InputAction.CallbackContext ctx) {
        // Handle click release
    }

    private void OnMiddleClick(InputAction.CallbackContext ctx) {
        // Handle middle click release
    }

    private void OnRightClick(InputAction.CallbackContext ctx) {
        // Handle right click release
    }

    private void OnSubmit(InputAction.CallbackContext ctx) {
        //Hangle submit click release
    }

    private void OnCancel(InputAction.CallbackContext ctx) {
        //Handle Cancel click release
    }
    private void OnPauseGame(InputAction.CallbackContext ctx) {
        if (!ctx.performed) return;
        SetPause(!isPaused);
    }
    #endregion
    private void OnDestroy() {
        UnsubscribeFromEvents();
    }
    public void SetPause(bool pause) {
        isPaused = pause;
        OnPauseStateChanged?.Invoke(isPaused);

        // Control time scale and player input from here
        if (isPaused)
        {
            Time.timeScale = 0f;
            InputManager.Instance?.PlayerMap.Disable();
        }
        else
        {
            Time.timeScale = 1f;
            InputManager.Instance?.PlayerMap.Enable();
        }
    }
}


