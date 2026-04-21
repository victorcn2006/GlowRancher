using System.Collections;
using UnityEngine;

public class InteractiveSilo : MonoBehaviour, IInteractive
{
    [Header("Referencias de Interfaz")]
    [SerializeField] private GameObject _siloUIContainer;

    [Header("Referencias de Control")]
    [SerializeField] private PlayerCameraMovement _cameraControl;
    [SerializeField] private SiloLogic _siloLogic;
    [SerializeField] private Inventory _playerInventory;

    private bool _isSiloActive = false;
    private float _timeSinceLastOpenedClosed = 0.16f;
    private const float TIMEBETWEENOPENCLOSE = 0.16f;

    private void Start()
    {
        if (_siloLogic == null)
            _siloLogic = GetComponent<SiloLogic>();
        if (_cameraControl == null)
            _cameraControl = FindObjectOfType<PlayerCameraMovement>();

        if (_playerInventory == null)
            _playerInventory = FindObjectOfType<Inventory>();

        if (_siloUIContainer == null)
        {
            // Busca directament per nom si el tag falla
            GameObject panel = GameObject.FindWithTag("SiloPanel");
            if (panel == null)
                panel = GameObject.Find("SiloPanel"); // fallback per nom

            if (panel != null)
            {
                _siloUIContainer = panel; // El panel ES el container
                Debug.Log("[InteractiveSilo] SiloUIContainer trobat: " + panel.name);
            }
            else
            {
                Debug.LogError("[InteractiveSilo] No s'ha trobat SiloPanel! Comprova el tag o el nom.");
            }
        }
    }

    private void OnEnable()
    {
        if (InputManager.Instance != null)
        {
            InputManager.Instance.OnInteractPerformed.AddListener(HandleKeyboardToggle);
            InputManager.Instance.OnPausePerformed.AddListener(CloseSilo);
        }
    }

    private void OnDisable()
    {
        if (InputManager.Instance != null)
        {
            InputManager.Instance.OnInteractPerformed.RemoveListener(HandleKeyboardToggle);
            InputManager.Instance.OnPausePerformed.RemoveListener(CloseSilo);
        }
    }

    private void HandleKeyboardToggle()
    {
        if (_isSiloActive) CloseSilo();
    }

    public void OpenSilo()
    {
        if (_timeSinceLastOpenedClosed >= TIMEBETWEENOPENCLOSE && !_isSiloActive)
        {
            _isSiloActive = true;
            _timeSinceLastOpenedClosed = 0;
            _siloUIContainer.SetActive(true);
            _playerInventory.siloAbierto = _siloLogic;
            _siloLogic.RefrescarUI();
            UpdateGameState(true);
            StartCoroutine(_InputDelay());
        }
    }

    public void CloseSilo()
    {
        if (_timeSinceLastOpenedClosed >= TIMEBETWEENOPENCLOSE && _isSiloActive)
        {
            _isSiloActive = false;
            _timeSinceLastOpenedClosed = 0;
            _siloUIContainer.SetActive(false);
            _playerInventory.siloAbierto = null;
            UpdateGameState(false);
            StartCoroutine(_InputDelay());
        }
    }

    public void UpdateGameState(bool siloOpen)
    {
        Time.timeScale = siloOpen ? 0f : 1f;
        if (_cameraControl != null) _cameraControl.SetControlState(!siloOpen);
        Cursor.visible = siloOpen;
        Cursor.lockState = siloOpen ? CursorLockMode.None : CursorLockMode.Locked;
    }

    IEnumerator _InputDelay()
    {
        while (_timeSinceLastOpenedClosed < TIMEBETWEENOPENCLOSE)
        {
            _timeSinceLastOpenedClosed += Time.unscaledDeltaTime;
            yield return null;
        }
    }

    public void OnInteract() => OpenSilo();
}
