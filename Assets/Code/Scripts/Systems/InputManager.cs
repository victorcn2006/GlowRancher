using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour {
    public static InputManager Instance { get; private set; }

    [Header("Input Action Asset")]
    [RequiredField, SerializeField] private InputActionAsset _inputs;

    //Action Maps
    private InputActionMap _ui;
    private InputActionMap _player;
    private InputActionMap _aspirator;
    private InputActionMap _inventory;

    [Header("UI Inputs")]
    [RequiredField, SerializeField] private InputActionReference _rightClick;
    [RequiredField, SerializeField] private InputActionReference _wikiOpen;

    [Header("Player Inputs")]

    [RequiredField, SerializeField] private InputActionReference _move;
    [RequiredField, SerializeField] private InputActionReference _jump;
    [RequiredField, SerializeField] private InputActionReference _look;
    [RequiredField, SerializeField] private InputActionReference _interact;
    [RequiredField, SerializeField] private InputActionReference _pauseGame;

    [Header("Aspirator Inputs")]
    [RequiredField, SerializeField] private InputActionReference _aspirate;
    [RequiredField, SerializeField] private InputActionReference _launchObject;

    [Header("Inventory Inputs")]
    [RequiredField, SerializeField] private InputActionReference _inventoryNavigation;
    [RequiredField, SerializeField] private InputActionReference _scroll;


    [HideInInspector] public bool IsJumpPressed { get; private set; }
    [HideInInspector] public bool IsPaused;
    [HideInInspector] public bool IsWikiOpen { get; private set; }

    [HideInInspector] public UnityEvent OnJumpPerformed = new UnityEvent();
    [HideInInspector] public UnityEvent OnPausePerformed = new UnityEvent();
    [HideInInspector] public UnityEvent OnWikiPerformed = new UnityEvent();

    public UnityEvent<float> OnInventoryScroll = new UnityEvent<float>();
    public UnityEvent<int> OnInventorySlotKey = new UnityEvent<int>();
    public UnityEvent OnInventoryRightClick = new UnityEvent();

    private void Awake()
    {
        if (Instance == null) {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
            return;
        }

        InitializeInputActionMaps();
    }

    private void OnEnable()
    {
        InitializeInputs();
        SubscribeEvents();
    }

    private void OnDisable()
    {
        DisableInputs();
        UnSubscribeEvents();
    }

    private void InitializeInputActionMaps() {
        _ui = _inputs.FindActionMap("UI");
        _player = _inputs.FindActionMap("Player");
        _aspirator = _inputs.FindActionMap("Aspirator");
        _inventory = _inputs.FindActionMap("Inventory");
    }

    public void InitializeInputs()
    {
        _ui.Enable();
        _player.Enable();
        _aspirator.Enable();
        _inventory.Enable();
    }

    public void DisableInputs()
    {
        _ui.Disable();
        _player.Disable();
        _aspirator.Disable();
        _inventory.Disable();
    }

    private void SubscribeEvents()
    {
        _jump.action.performed += OnJump;
        _pauseGame.action.performed += OnPauseGame;
        _wikiOpen.action.performed += OnWikiOpen;

        _scroll.action.performed += OnInventoryScrollPerformed;
        _inventoryNavigation.action.performed += OnInventorySlotKeyPerformed;
        _rightClick.action.performed += OnInventoryRightClickPerformed;
    }
    private void UnSubscribeEvents()
    {
        _jump.action.performed -= OnJump;
        _pauseGame.action.performed -= OnPauseGame;
        _wikiOpen.action.performed -= OnWikiOpen;

        _scroll.action.performed -= OnInventoryScrollPerformed;
        _inventoryNavigation.action.performed -= OnInventorySlotKeyPerformed;
        _rightClick.action.performed -= OnInventoryRightClickPerformed;
    }

    private void OnJump(InputAction.CallbackContext ctx) {
        IsJumpPressed = true;
        OnJumpPerformed?.Invoke();
    }

    private void OnPauseGame(InputAction.CallbackContext ctx)
    {
        IsPaused = true;
        OnPausePerformed?.Invoke();
    }

    private void OnWikiOpen(InputAction.CallbackContext ctx) {
        IsWikiOpen = true;
        OnWikiPerformed?.Invoke();
    }

    private void OnInventoryScrollPerformed(InputAction.CallbackContext ctx)
    {
        float scroll = ctx.ReadValue<Vector2>().y;
        OnInventoryScroll?.Invoke(scroll);
    }

    private void OnInventorySlotKeyPerformed(InputAction.CallbackContext ctx)
    {
        // The control name will be "1", "2", "3", or "4"
        if (ctx.control == null) return;

        int slot = -1;
        switch (ctx.control.name)
        {
            case "1": slot = 0; break;
            case "2": slot = 1; break;
            case "3": slot = 2; break;
            case "4": slot = 3; break;
        }

        if (slot >= 0)
            OnInventorySlotKey?.Invoke(slot);
    }

    private void OnInventoryRightClickPerformed(InputAction.CallbackContext ctx)
    {
        OnInventoryRightClick?.Invoke();
    }
    public void SetWikiOpen(bool state)
    {
        IsWikiOpen = state;
    }
}

    


