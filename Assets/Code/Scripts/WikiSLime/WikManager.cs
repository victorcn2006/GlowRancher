using UnityEngine;

public class WikiManager : MonoBehaviour
{
    public static WikiManager instance { get; private set; }

    [Header("Referencias de UI")]
    [SerializeField] private WikiSlime _wikiMenu;
    [SerializeField] private PanelShopController _panelShop;
    [SerializeField] private GameObject _inventari;

    [Header("Referencias de Control")]
    [Tooltip("Arrastra aquí el objeto que tiene el script PlayerCameraMovement")]
    [SerializeField] private PlayerCameraMovement _cameraControl;

    private bool _isWikiActive = false;

    public bool IsWikiActive => _isWikiActive;//per el status de si esta obert o tancat
    private void OnEnable()
    {
        // Suscripción al evento del InputManager
        if (InputManager.Instance != null)
        {
            InputManager.Instance.OnWikiPerformed.AddListener(ToggleWiki);
        }
    }

    private void OnDisable()
    {
        // Limpieza de eventos para evitar errores de memoria
        if (InputManager.Instance != null)
        {
            InputManager.Instance.OnWikiPerformed.RemoveListener(ToggleWiki);
        }
    }

    private void Awake()
    {
        if(instance == null) instance = this;
        else Destroy(this.gameObject);
    }
    private void Start()
    {
        // Estado inicial: Wiki cerrada al empezar
        _isWikiActive = false;
        CloseWiki();
    }


    private void ToggleWiki()
    {
        // Si el juego está en el menú de pausa principal, no permitimos abrir la wiki
        if (InputManager.Instance.IsPaused) return;

        _isWikiActive = !_isWikiActive;

        if (_isWikiActive)
            OpenWiki();
        else
            CloseWiki();
    }

    public void OpenWiki()
    {
        _wikiMenu.ActiveWiki(); // Activa el panel visual
        UpdateGameState(true);  // Bloquea cámara y tiempo
        _inventari.SetActive(false);
        InputManager.Instance.SetWikiOpen(true);
    }


    public void CloseWiki()
    {
        _wikiMenu.DesactiveWiki(); // Desactiva el panel visual
        UpdateGameState(false);    // Desbloquea cámara y tiempo
        _inventari.SetActive(true);
    }


    /// <summary>
    /// Este método centraliza todo lo que debe cambiar al abrir/cerrar menús
    /// </summary>
    private void UpdateGameState(bool wikiIsOpening)
    {
        // 1. Congelar o reanudar el tiempo (física, animaciones, etc.)
        Time.timeScale = wikiIsOpening ? 0f : 1f;

        // 2. Avisar al InputManager para que otros sistemas sepan el estado
        InputManager.Instance.SetWikiOpen(wikiIsOpening);

        // 3. BLOQUEO DE CÁMARA Y RATÓN
        // Si la wiki se abre (true), pasamos 'false' a la cámara para que se apague
        if (_cameraControl != null)
        {
            _cameraControl.SetControlState(!wikiIsOpening);
        }
        else
        {
            Debug.LogError("¡Falta asignar la cámara en el WikiManager!");
        }
    }

    public void OnPauseResumed()
    {
        if (_isWikiActive)
        {
            // La wiki estava oberta abans de pausar, restaurem el seu estat
            _wikiMenu.ActiveWiki();
            _inventari.SetActive(false);
            // No cal tocar timeScale ni càmera, ho fa PauseManager
        }
    }
}
