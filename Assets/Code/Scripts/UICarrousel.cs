using TMPro;
using UnityEngine;

using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UICarrousel : MonoBehaviour
{
    [System.Serializable]
    public class CarouselItem
    {
        public string title;
        public Color textColor;
        public GameObject panel; // ADD THIS - The panel to show for this item
    }

    [Header("UI")]
    [SerializeField] private CarouselItem[] _items;
    [SerializeField] private TextMeshProUGUI _titleText;
    [SerializeField] private Button _prevButton;
    [SerializeField] private Button _nextButton;

    [Header("Transition Settings")]
    [SerializeField] private float _transitionDuration = 0.5f;
    [SerializeField] private TransitionType _panelTransition = TransitionType.Fade;

    private int _currentIndex = 0;
    private bool _isTransitioning = false;

    public enum TransitionType
    {
        Instant,
        Fade,
        SlideHorizontal,
        Scale
    }

    private void Start()
    {
        _prevButton.onClick.AddListener(Previous);
        _nextButton.onClick.AddListener(Next);

        // Initialize panels
        InitializePanels();
        UpdateDisplay();
    }

    private void InitializePanels()
    {
        // Add CanvasGroup to all panels if using fade/scale animations
        for (int i = 0; i < _items.Length; i++)
        {
            if (_items[i].panel != null)
            {
                // Ensure CanvasGroup exists for smooth transitions
                if (_items[i].panel.GetComponent<CanvasGroup>() == null)
                {
                    _items[i].panel.AddComponent<CanvasGroup>();
                }

                // Show only the first panel
                _items[i].panel.SetActive(i == 0);
            }
        }
    }

    public void Next()
    {
        if (_isTransitioning) return;
        int nextIndex = (_currentIndex + 1) % _items.Length;
        StartCoroutine(TransitionTo(nextIndex, true));
    }

    public void Previous()
    {
        if (_isTransitioning) return;
        int prevIndex = (_currentIndex - 1 + _items.Length) % _items.Length;
        StartCoroutine(TransitionTo(prevIndex, false));
    }

    public void GoToIndex(int index)
    {
        if (_isTransitioning || index < 0 || index >= _items.Length) return;
        bool forward = index > _currentIndex;
        StartCoroutine(TransitionTo(index, forward));
    }

    private IEnumerator TransitionTo(int index, bool forward)
    {
        _isTransitioning = true;

        GameObject currentPanel = _items[_currentIndex].panel;
        GameObject nextPanel = _items[index].panel;

        // Fade out text
        yield return StartCoroutine(FadeText(0f, _transitionDuration / 2));

        // Transition panels
        if (currentPanel != null && nextPanel != null)
        {
            yield return StartCoroutine(TransitionPanels(currentPanel, nextPanel, forward));
        }

        // Update text content
        _titleText.text = _items[index].title;
        _titleText.color = new Color(_items[index].textColor.r,
                                     _items[index].textColor.g,
                                     _items[index].textColor.b, 0f);

        // Fade in text
        yield return StartCoroutine(FadeText(1f, _transitionDuration / 2));

        _currentIndex = index;
        _isTransitioning = false;
    }

    private IEnumerator TransitionPanels(GameObject currentPanel, GameObject nextPanel, bool forward)
    {
        CanvasGroup currentCG = currentPanel.GetComponent<CanvasGroup>();
        CanvasGroup nextCG = nextPanel.GetComponent<CanvasGroup>();
        RectTransform currentRT = currentPanel.GetComponent<RectTransform>();
        RectTransform nextRT = nextPanel.GetComponent<RectTransform>();

        // Activate next panel
        nextPanel.SetActive(true);

        // Setup initial states
        switch (_panelTransition)
        {
            case TransitionType.Instant:
                currentPanel.SetActive(false);
                yield break;

            case TransitionType.Fade:
                nextCG.alpha = 0f;
                break;

            case TransitionType.SlideHorizontal:
                nextCG.alpha = 1f;
                Vector2 startPos = new Vector2(forward ? 1920 : -1920, 0);
                nextRT.anchoredPosition = startPos;
                break;

            case TransitionType.Scale:
                nextCG.alpha = 0f;
                nextRT.localScale = Vector3.zero;
                break;
        }

        float elapsed = 0f;

        // Animate transition
        while (elapsed < _transitionDuration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.SmoothStep(0f, 1f, elapsed / _transitionDuration);

            switch (_panelTransition)
            {
                case TransitionType.Fade:
                    currentCG.alpha = 1f - t;
                    nextCG.alpha = t;
                    break;

                case TransitionType.SlideHorizontal:
                    Vector2 exitPos = new Vector2(forward ? -1920 : 1920, 0);
                    Vector2 enterPos = new Vector2(forward ? 1920 : -1920, 0);

                    currentRT.anchoredPosition = Vector2.Lerp(Vector2.zero, exitPos, t);
                    nextRT.anchoredPosition = Vector2.Lerp(enterPos, Vector2.zero, t);
                    break;

                case TransitionType.Scale:
                    currentCG.alpha = 1f - t;
                    currentRT.localScale = Vector3.Lerp(Vector3.one, Vector3.zero, t);
                    nextCG.alpha = t;
                    nextRT.localScale = Vector3.Lerp(Vector3.zero, Vector3.one, t);
                    break;
            }

            yield return null;
        }

        // Cleanup - ensure final states
        currentPanel.SetActive(false);
        currentCG.alpha = 0f;
        currentRT.anchoredPosition = Vector2.zero;
        currentRT.localScale = Vector3.one;

        nextCG.alpha = 1f;
        nextRT.anchoredPosition = Vector2.zero;
        nextRT.localScale = Vector3.one;
    }

    private IEnumerator FadeText(float targetAlpha, float duration)
    {
        Color startColor = _titleText.color;
        Color targetColor = new Color(startColor.r, startColor.g, startColor.b, targetAlpha);

        float elapsed = 0f;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / duration;
            _titleText.color = Color.Lerp(startColor, targetColor, t);
            yield return null;
        }

        _titleText.color = targetColor;
    }

    private void UpdateDisplay()
    {
        if (_items.Length > 0)
        {
            _titleText.text = _items[_currentIndex].title;
            _titleText.color = _items[_currentIndex].textColor;
        }
    }
}
