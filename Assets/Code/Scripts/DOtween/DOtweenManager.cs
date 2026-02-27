using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;

public class DOTweenManager : MonoBehaviour
{
    public static DOTweenManager Instance { get; private set; }

    [Header("UI & Motion Settings")]
    [SerializeField] private float _defaultDuration = 0.5f;
    [SerializeField] private Ease _defaultEase = Ease.OutBack;

    [Header("Feedback Settings")]
    [SerializeField] private float _punchStrength = 0.2f;

    // --- ESTADOS ---
    [HideInInspector] public bool IsAnimating { get; private set; }

    // --- EVENTOS (Igual que en tu InputManager) ---
    [HideInInspector] public UnityEvent OnMovementStarted = new UnityEvent();
    [HideInInspector] public UnityEvent OnScaleStarted = new UnityEvent();
    [HideInInspector] public UnityEvent OnAnimationComplete = new UnityEvent();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    // Nota: A diferencia del InputManager, aquí no solemos usar SubscribeEvents para los floats,
    // sino que llamamos a los métodos directamente, pero mantengo la estructura para los eventos.

    #region ANIMACIONES PRINCIPALES

    /// <summary>
    /// Mueve un objeto a una posición local específica.
    /// </summary>
    public void MoveTo(Transform target, Vector3 localPosition, float customDuration = -1f)
    {
        if (target == null) return;

        float duration = customDuration < 0 ? _defaultDuration : customDuration;

        IsAnimating = true;
        OnMovementStarted?.Invoke(); // Disparamos el evento de inicio

        target.DOLocalMove(localPosition, duration)
            .SetEase(_defaultEase)
            .OnComplete(CompleteAnimation);
    }

    /// <summary>
    /// Cambia la escala de un objeto.
    /// </summary>
    public void ScaleTo(Transform target, Vector3 targetScale, float duration)
    {
        if (target == null) return;

        // Matamos cualquier animación previa en este objeto para que no haya pelea
        target.DOKill();

        target.DOScale(targetScale, duration)
            .SetEase(_defaultEase)
            .SetUpdate(true) // Esto hace que funcione incluso si el juego está en pausa (Time.timeScale = 0)
            .OnComplete(() => {
                IsAnimating = false;
                OnAnimationComplete?.Invoke();
            });
    }

    #endregion

    private void CompleteAnimation()
    {
        IsAnimating = false;
        OnAnimationComplete?.Invoke();
    }

    // Ejemplo de cómo podrías "matar" animaciones si fuera necesario
    public void KillTweens(Transform target)
    {
        target.DOKill();
    }
}
