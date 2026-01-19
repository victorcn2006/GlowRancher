using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour{
    public static InputManager Instance { get; private set; }

    [Header("InputAction asset with all the _inputs")]
    [RequiredField, SerializeField] private InputActionAsset _inputs;

    [Header("All the input actions and action maps to enable/disable them")]
    //Action maps
    private InputActionMap _playerMap;
    private InputActionMap _aspiratorMap;
    private InputActionMap _inventoryMap;
    private InputActionMap _uIMap;
    //InputActions
    private InputAction _pauseGame;
    private InputAction _wiki;
    private InputAction _inventoryNavigation;


    public bool isPaused { get; private set; } = false;
    public bool wikiOpen { get; private set; } = false;
    private void Awake() {
        //SINGLETON
        if(Instance == null)
            Instance = this;
        else
        {
            Destroy(this.gameObject);
            return;
        }
            

        //_inputs configuration
        if (_inputs == null) return;
        //Input Actions Map references
        _playerMap = _inputs.FindActionMap("Player");
        _aspiratorMap = _inputs.FindActionMap("Aspirator");
        _inventoryMap = _inputs.FindActionMap("Inventory");
        _uIMap = _inputs.FindActionMap("UI");

        //Activate all the _inputs
        _inputs.Enable();
    }

    private void Start()
    {
        if (_inputs == null) return;
        //Input Action references
        _pauseGame = _uIMap.FindAction("PauseGame");
        _wiki = _uIMap.FindAction("OpenWiki");
        _inventoryNavigation = _inventoryMap.FindAction("InventoryNavigation");

        _pauseGame.performed += OnPauseGame;
        CheckPause(isPaused);
    }

    private void OnDisable() {
        if (_pauseGame != null)
            _pauseGame.performed -= OnPauseGame;
    }

    private void OnPauseGame(InputAction.CallbackContext ctx) {
        if (wikiOpen)
            return;
        if (!ctx.performed) return;
        SetPause(!isPaused);
    }
    public void SetPause(bool pause) {
        isPaused = pause;
        CheckPause(isPaused);
        if (pause)
            wikiOpen = false;
        
    }
    public void SetWikiOpen(bool state) {
        wikiOpen = state;
    }
    public bool IsWikiOpen() { 
        return wikiOpen;
    }
    private void CheckPause(bool isPaused) {
        if (isPaused)
        {
            Time.timeScale = 0f;
            _playerMap.Disable();
        }
        else
        {
            Time.timeScale = 1f;
            _playerMap.Enable();
        }
    }
}
