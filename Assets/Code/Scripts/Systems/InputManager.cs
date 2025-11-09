using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;

public class InputManager : MonoBehaviour{

    public static InputManager Instance { get; private set; }
    [RequiredField, SerializeField] private InputActionAsset inputs;
    private InputAction pauseGame;
    public bool isPaused { get; private set; } = false;
    private void Awake() {
        if(Instance == null)
            Instance = this;
        else
            Destroy(this.gameObject);
        if(inputs != null) inputs.Enable();
        else
            Debug.LogError("InputActionAsset no asignado en InputManager.");
    }
    private void OnEnable() {
        if (pauseGame == null) pauseGame = inputs.FindActionMap("UI").FindAction("PauseGame");
        if (pauseGame != null)
            pauseGame.performed += OnPauseGame;
        CheckPause(isPaused);
    }

    private void OnDisable() {
        if (pauseGame != null)
            pauseGame.performed -= OnPauseGame;
    }

    private void OnPauseGame(InputAction.CallbackContext ctx) {
        if (!ctx.performed) return;
        SetPause(!isPaused);
        
    }
    public void SetPause(bool pause) {
        isPaused = pause;
        CheckPause(isPaused);
        
    }
    private void CheckPause(bool isPaused) {
        if (isPaused)
        {
            Time.timeScale = 0f;
            inputs.FindActionMap("Player").Disable();
        }
        else
        {
            Time.timeScale = 1f;
            inputs.FindActionMap("Player").Enable();
        }

    }
}
