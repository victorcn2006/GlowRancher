using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;

public class InputManager : MonoBehaviour{
    public static InputManager Instance { get; private set; }

    [Header("InputAction asset with all the inputs")]
    [RequiredField, SerializeField] private InputActionAsset _gameInputActionAsset;

    #region Game Input Action Maps
        private InputActionMap playerMap;
        private InputActionMap aspiratorMap;
        [HideInInspector] public InputActionMap inventoryMap;

        public InputActionMap PlayerMap => playerMap;
    #endregion

    #region Game Input Actions References
    //Player Input Actions
    private InputAction openWiki;
    private InputAction jump;
    private InputAction rightClick;
    //Inventory Input Actions
    private InputAction inventoryNavigation;
    private InputAction scroll;
    //Aspirator Input Actions

    #endregion

    #region Public References
    public bool isPaused { get; private set; } = false;
    public bool wikiOpen { get; private set; } = false;

    public event System.Action OnWikiToggled;
    public event System.Action<float> OnInventoryScroll;
    public event System.Action<int> OnSlotSelected;
    public event System.Action OnJumpPressed;
    #endregion

    private void Awake() {

        if(Instance == null)
            Instance = this;
        else
        {
            Destroy(this.gameObject);
            return;
        }
        InitializeInputs();
    }

    private void OnEnable() {
        playerMap?.Enable();
        aspiratorMap?.Enable();
        inventoryMap?.Enable();
    }
    private void OnDisable() {
        playerMap?.Disable();
        aspiratorMap?.Disable();
        inventoryMap?.Disable();
    }
    #region Input References
    private void InitializeInputs() {

        if (_gameInputActionAsset == null)
        {
            Debug.LogError("InputManager: InputActionAsset is not assigned!");
            return;
        }

        if ((playerMap = _gameInputActionAsset?.FindActionMap("Player")) is null) return;
        if ((aspiratorMap = _gameInputActionAsset?.FindActionMap("Aspirator")) is null) return;
        if ((inventoryMap = _gameInputActionAsset?.FindActionMap("Inventory")) is null) return;
        
        openWiki = GetAction(playerMap, "OpenWiki");
        jump = GetAction(playerMap, "Jump");
        inventoryNavigation = GetAction(inventoryMap, "InventoryNavigation");
        scroll = GetAction(inventoryMap, "Scroll");
        SubscribeToEvents();
    }
    private InputAction GetAction(InputActionMap map, string actionName) {
        var action = map.FindAction(actionName);
        if (action == null)
        {
            Debug.LogWarning($"InputManager: Action '{actionName}' not found in the ActionMap!");
        }
        return action;
    }
    #endregion

    #region Event Subscription
    private void SubscribeToEvents() {

        if (inventoryNavigation != null)
        {
            inventoryNavigation.performed += OnInventoryNavigation;
        }

        if (openWiki != null)
        {
            openWiki.performed += OnWikiOpen;
        }

        if (scroll != null) {
            scroll.performed += OnScroll;
        }

        if (jump != null)
        {
            jump.performed += OnJump;
        }
    }

    private void UnsubscribeFromEvents() {

        if (inventoryNavigation != null)
        {
            inventoryNavigation.performed -= OnInventoryNavigation;
        }

        if (openWiki != null)
        {
            openWiki.performed -= OnWikiOpen;
        }
        if (scroll != null)
        {
            scroll.performed -= OnScroll;
        }
        if (jump != null)
        {
            jump.performed -= OnJump;
        }
    }
    #endregion

    #region Input Callbacks
    
    private void OnInventoryNavigation(InputAction.CallbackContext ctx) {
        if (!ctx.performed) return;

        string keyName = ctx.control.name;

        int slotIndex = -1;

        switch (keyName)
        {
            case "1":
                slotIndex = 0;
                break;
            case "2":
                slotIndex = 1;
                break;
            case "3":
                slotIndex = 2;
                break;
            case "4":
                slotIndex = 3;
                break;
        }

        if (slotIndex >= 0)
        {
            OnSlotSelected?.Invoke(slotIndex);
        }
    }
    private void OnScroll(InputAction.CallbackContext ctx) {
        if (!ctx.performed) return;

        Vector2 scrollValue = ctx.ReadValue<Vector2>();
        if (scrollValue.y != 0)
        {
            OnInventoryScroll?.Invoke(scrollValue.y);
        }
        else if (scrollValue.x != 0)
        {
            OnInventoryScroll?.Invoke(scrollValue.x);
        }
    }
    private void OnWikiOpen(InputAction.CallbackContext ctx) {
        if (!ctx.performed) return;
        OnWikiToggled?.Invoke();
    }
    private void OnJump(InputAction.CallbackContext ctx) {
        if (!ctx.performed) return;
        OnJumpPressed?.Invoke();
    }
    #endregion

    private void OnDestroy() {
        UnsubscribeFromEvents();
    }

    
    public void SetWikiOpen(bool state) {
        wikiOpen = state;
    }
    
}
