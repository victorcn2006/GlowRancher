using UnityEngine;
using DG.Tweening;

public class VacuumAnimation : MonoBehaviour
{
    [Header("Referencias")]
    [SerializeField] private Transform _vacuumModel;

    [Header("Configuración de Animación")]
    [Tooltip("Movimiento relativo desde la posición inicial (X, Y, Z)")]
    [SerializeField] private Vector3 _moveAmount = new Vector3(0f, 0.05f, 0.04f);
    [SerializeField] private float _cycleSpeed = 0.4f;

    private Tween _moveTween;
    private bool _isAnimating;
    private Vector3 _initialLocalPos;

    void Start()
    {
        if (_vacuumModel == null)
        {
            Debug.LogError("¡No has asignado el Transform del modelo de la aspiradora!");
            return;
        }

        // Guardamos la posición original que pusiste en el Inspector
        _initialLocalPos = _vacuumModel.localPosition;

        // Configuramos el Tween (usamos DOLocalMove para controlar todos los ejes)
        _moveTween = _vacuumModel.DOLocalMove(_initialLocalPos + _moveAmount, _cycleSpeed)
            .SetEase(Ease.InOutSine)
            .SetLoops(-1, LoopType.Yoyo)
            .SetAutoKill(false)
            .Pause();
    }

    void Update()
    {
        // Usamos la instancia Singleton que creaste en tu PlayerMovement
        if (PlayerMovement.instance == null || _vacuumModel == null) return;

        HandleAnimationLogic();
    }

    private void HandleAnimationLogic()
    {
        // Accedemos a las propiedades IsMoving e IsRunning de tu script
        bool moving = PlayerMovement.instance.IsMoving;
        bool running = PlayerMovement.instance.IsRunning;

        if (moving)
        {
            if (!_isAnimating)
            {
                _moveTween.Play();
                _isAnimating = true;
            }

            // Ajustamos la velocidad de la animación basándonos en si corre o camina
            float targetTimeScale = running ? 2.1f : 1.3f;

            // Suavizamos el cambio de velocidad de la animación
            _moveTween.timeScale = Mathf.Lerp(_moveTween.timeScale, targetTimeScale, Time.deltaTime * 5f);
        }
        else
        {
            if (_isAnimating)
            {
                _isAnimating = false;
                _moveTween.Pause();

                // RETORNO AL ORIGEN: 
                // Cuando el player se detiene, la aspiradora vuelve suavemente a su sitio original
                _vacuumModel.DOLocalMove(_initialLocalPos, 0.25f).SetEase(Ease.OutQuad);
            }
        }
    }

    private void OnDestroy()
    {
        // Muy importante matar el tween para evitar errores de memoria
        if (_moveTween != null) _moveTween.Kill();
    }
}
