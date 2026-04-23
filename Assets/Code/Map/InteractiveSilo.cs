using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class InteractiveSilo : MonoBehaviour, IInteractive
{
    [Header("Referencias")]
    [SerializeField] private GameObject _siloUIContainer;
    [SerializeField] private PlayerCameraMovement _cameraControl;
    [SerializeField] private SiloLogic _siloLogic;
    [SerializeField] private Inventory _playerInventory;

    private bool _isSiloActive = false;
    private float _timeSinceLastOpenedClosed = 0.2f;
    private const float TIMEBETWEENOPENCLOSE = 0.2f;

    private void Start()
    {
        if (_siloLogic == null) _siloLogic = GetComponent<SiloLogic>();
        if (_cameraControl == null) _cameraControl = FindObjectOfType<PlayerCameraMovement>();
        if (_playerInventory == null) _playerInventory = FindObjectOfType<Inventory>();

        FindSiloPanel();
    }

    private void FindSiloPanel()
    {
        if (_siloUIContainer != null) return;

        GameObject hud = GameObject.Find("CanvasHUD") ?? GameObject.FindGameObjectWithTag("HUD");

        if (hud != null)
        {
            Transform panelTransform = hud.transform.Find("SiloPanel");
            if (panelTransform != null) _siloUIContainer = panelTransform.gameObject;
        }

        if (_siloUIContainer == null)
        {
            foreach (GameObject go in Resources.FindObjectsOfTypeAll<GameObject>())
            {
                if (go.name == "SiloPanel" && go.hideFlags == HideFlags.None)
                {
                    _siloUIContainer = go;
                    break;
                }
            }
        }
    }

    public void OpenSilo()
    {
        if (_isSiloActive || _timeSinceLastOpenedClosed < TIMEBETWEENOPENCLOSE) return;

        if (_siloUIContainer == null) FindSiloPanel();

        if (_siloUIContainer != null)
        {
            _isSiloActive = true;
            _timeSinceLastOpenedClosed = 0;

            _siloUIContainer.SetActive(true);

            // Vinculamos y refrescamos
            _siloLogic.VincularUI(_siloUIContainer);
            ConfigurarBotonesUI();

            _playerInventory.siloAbierto = _siloLogic;
            _siloLogic.RefrescarUI();

            UpdateGameState(true);
            StartCoroutine(_InputDelay());
        }
    }

    private void ConfigurarBotonesUI()
    {
        SlotUI[] uiSlots = _siloUIContainer.GetComponentsInChildren<SlotUI>(true);
        for (int i = 0; i < uiSlots.Length; i++)
        {
            int indice = i;
            Button btn = uiSlots[i].GetComponent<Button>();
            if (btn != null)
            {
                btn.onClick.RemoveAllListeners();
                btn.onClick.AddListener(() => _siloLogic.ExtraerDelSilo(indice));
            }
        }
    }

    public void CloseSilo()
    {
        if (!_isSiloActive || _timeSinceLastOpenedClosed < TIMEBETWEENOPENCLOSE) return;

        _isSiloActive = false;
        _timeSinceLastOpenedClosed = 0;

        if (_siloUIContainer != null) _siloUIContainer.SetActive(false);
        _playerInventory.siloAbierto = null;

        UpdateGameState(false);
        StartCoroutine(_InputDelay());
    }

    public void UpdateGameState(bool siloOpen)
    {
        Time.timeScale = siloOpen ? 0f : 1f;
        if (_cameraControl != null) _cameraControl.SetControlState(!siloOpen);
        Cursor.visible = siloOpen;
        Cursor.lockState = siloOpen ? CursorLockMode.None : CursorLockMode.Locked;
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

    private void HandleKeyboardToggle() { if (_isSiloActive) CloseSilo(); }

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
