using UnityEngine;
using DG.Tweening;

public class VacuumAnimationFX : MonoBehaviour
{
    [Header("Referencias")]
    [SerializeField] private PlayerMovement _playerMovement;
    [SerializeField] private Transform _vacuumModel;

    [Header("Configuración de Animación")]
    [SerializeField] private Vector3 _moveAmount = new Vector3(0f, 0.03f, 0.02f); // Movimiento sutil en Y y Z
    [SerializeField] private float _speed = 0.4f;

    private Tween _moveTween;
    private bool _isAnimating;
    private Vector3 _initialLocalPos; // Guardará la posición original

    void Start()
    {
        if (_vacuumModel == null) return;

        // 1. Guardamos la posición exacta que tiene la aspiradora en el Inspector al empezar
        _initialLocalPos = _vacuumModel.localPosition;

        // 2. Configuramos la animación como relativa
        _moveTween = _vacuumModel.DOLocalMove(_moveAmount, _speed)
            .SetRelative(true)
            .SetEase(Ease.InOutSine)
            .SetLoops(-1, LoopType.Yoyo)
            .SetAutoKill(false)
            .Pause();
    }

    void Update()
    {
        if (_playerMovement == null || _vacuumModel == null) return;

        if (_playerMovement.IsMoving)
        {
            if (!_isAnimating)
            {
                _moveTween.Play();
                _isAnimating = true;
            }

            float targetTimeScale = _playerMovement.IsRunning ? 1.8f : 1.0f;
            _moveTween.timeScale = Mathf.Lerp(_moveTween.timeScale, targetTimeScale, Time.deltaTime * 5f);
        }
        else
        {
            if (_isAnimating)
            {
                _isAnimating = false;
                _moveTween.Pause();

                // 3. Al detenerse, vuelve suavemente a la posición GUARDADA inicialmente
                _vacuumModel.DOLocalMove(_initialLocalPos, 0.25f).SetEase(Ease.OutQuad);
            }
        }
    }

    private void OnDestroy()
    {
        if (_moveTween != null) _moveTween.Kill();
    }
}
