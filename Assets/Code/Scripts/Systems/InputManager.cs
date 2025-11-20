using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;

public class InputManager : MonoBehaviour{
    public static InputManager Instance { get; private set; }

    [Header("InputAction asset with all the inputs")]
    [RequiredField, SerializeField] private InputActionAsset inputs;

    [Header("All the input actions and action maps to enable/disable them")]
    //Action maps
    private InputActionMap playerMap;
    private InputActionMap aspiratorMap;
    private InputActionMap inventoryMap;
    private InputActionMap UIMap;
    //InputActions
    private InputAction pauseGame;
    private InputAction wiki;
    private InputAction inventoryNavigation;


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
            

        //Inputs configuration
        if (inputs == null) return; 
        //Input Actions Map references
        playerMap = inputs.FindActionMap("Player");
        aspiratorMap = inputs.FindActionMap("Aspirator");
        inventoryMap = inputs.FindActionMap("Inventory");
        UIMap = inputs.FindActionMap("UI");

        //Activate all the inputs
        inputs.Enable();
    }

    private void Start()
    {
        if (inputs == null) return;
        //Input Action references
        pauseGame = UIMap.FindAction("PauseGame");
        wiki = UIMap.FindAction("OpenWiki");
        inventoryNavigation = inventoryMap.FindAction("InventoryNavigation");

        pauseGame.performed += OnPauseGame;
        CheckPause(isPaused);
    }

    private void OnDisable() {
        if (pauseGame != null)
            pauseGame.performed -= OnPauseGame;
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
            playerMap.Disable();
        }
        else
        {
            Time.timeScale = 1f;
            playerMap.Enable();
        }
    }
}
