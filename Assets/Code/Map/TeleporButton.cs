using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI; // Necesario para Button e Image

public class TeleporButton : MonoBehaviour
{
    [Header("Configuración del Monolito")]
    [SerializeField] private MonolitoManager _monolito; // Referencia al script del monolito

    [Header("Referencias de Teletransporte")]
    [SerializeField] private GameObject _player;
    [SerializeField] private Transform _destination; // El transform a donde irá el player
    [SerializeField] private float _delayTime = 2.0f;

    [Header("UI & Mapa")]
    [SerializeField] private GameObject _map;
    [SerializeField] private InteractiveMap _interactiveMap;
    [SerializeField] private GameObject _cloudOverlay; // La imagen de la nube que oculta el botón
    [SerializeField] private Button _buttonComponent;  // El componente Button de este objeto

    private Coroutine _teleportCoroutine;

    private void Awake()
    {
        // Si no asignaste el botón manualmente, lo buscamos
        if (_buttonComponent == null) _buttonComponent = GetComponent<Button>();
    }

    private void Start()
    {
        RefreshStatus();
    }

    private void OnEnable()
    {
        // Actualizamos cada vez que el mapa se abre para asegurar que refleje el progreso
        RefreshStatus();
    }

    /// <summary>
    /// Controla si el botón es visible o está tapado por nubes
    /// </summary>
    public void RefreshStatus()
    {
        if (_monolito == null) return;

        bool isUnlocked = _monolito.IsActivated;

        // Si el monolito está activado, ocultamos la nube. Si no, la mostramos.
        if (_cloudOverlay != null)
        {
            _cloudOverlay.SetActive(!isUnlocked);
        }

        // El botón solo es interactuable si el monolito está desbloqueado
        if (_buttonComponent != null)
        {
            _buttonComponent.interactable = isUnlocked;
        }
    }

    public void OnButtonClick()
    {
        // Verificación de seguridad por código
        if (_monolito != null && !_monolito.IsActivated)
        {
            Debug.Log("Teletransporte bloqueado: El monolito no ha sido purificado.");
            return;
        }

        if (_teleportCoroutine == null)
        {
            _teleportCoroutine = GameManager.Instance.StartCoroutine(TeleportSequence());
        }
    }

    private IEnumerator TeleportSequence()
    {
        // 1. Cerramos el mapa
        if (_map != null) _map.SetActive(false);

        // 2. Iniciamos animación de rotación/cámara
        DoAnimationRotate();

        // 3. Esperas (usando Realtime por si el juego está pausado en el mapa)
        yield return new WaitForSecondsRealtime(2f);
        yield return new WaitForSecondsRealtime(_delayTime);

        // 4. Ejecutamos el movimiento
        ExecuteTeleport();

        // 5. Limpiamos estado
        FinalizeSequence();
    }

    private void ExecuteTeleport()
    {
        if (_player != null && _destination != null)
        {
            _player.transform.position = _destination.position;
            _player.transform.rotation = _destination.rotation;
        }
    }

    private void FinalizeSequence()
    {
        if (_interactiveMap != null)
        {
            _interactiveMap.UpdateGameState(false);
        }
        _teleportCoroutine = null;
    }

    private void DoAnimationRotate()
    {
        if (_player != null && _destination != null)
        {
            // Usamos la posición del último altar interactuado para la animación inicial
            Transform lastAltar = InteractiveMap.GetLastInteractiveMapInteractedAltarTransform();
            Vector3 targetPos = lastAltar != null ? lastAltar.position : _player.transform.position;

            DOTween.Sequence()
                .Append(_player.transform.DOMove(targetPos, _delayTime))
                .Join(_player.transform.DORotate(_destination.eulerAngles, _delayTime)
                    .SetEase(Ease.InOutSine))
                .SetUpdate(UpdateType.Normal, true); // true para que funcione aunque Time.timeScale sea 0
        }
    }
}
