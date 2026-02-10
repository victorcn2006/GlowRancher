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
    }

    [Header("UI")]
    [SerializeField] private CarouselItem[] _items;
    [SerializeField] private TextMeshProUGUI _titleText;
    [SerializeField] private Button _prevButton;
    [SerializeField] private Button _nextButton;

    [SerializeField] private const float TRANSITION_DURATION = 0.5f;

    private int _currentIndex = 0;
    private bool _isTransitioning = false;
    private string _value;

    private void Start()
    {
        _prevButton.onClick.AddListener(Previous);
        _nextButton.onClick.AddListener(Next);
        UpdateDisplay();
    }

    public void Next()
    {
        if (_isTransitioning) return;
        _currentIndex = (_currentIndex + 1) % _items.Length;
        StartCoroutine(TransitionTo(_currentIndex));
    }

    public void Previous()
    {
        if (_isTransitioning) return;
        _currentIndex = (_currentIndex - 1 + _items.Length) % _items.Length;
        StartCoroutine(TransitionTo(_currentIndex));
    }

    private IEnumerator TransitionTo(int index)
    {
        _isTransitioning = true;

        // Fade out
        yield return StartCoroutine(FadeText(0f, TRANSITION_DURATION / 2));

        // Update content
        _titleText.text = _items[index].title;
        _titleText.color = _items[index].textColor;
        _value = _items[index].title;

        // Fade in
        yield return StartCoroutine(FadeText(1f, TRANSITION_DURATION / 2));

        _isTransitioning = false;
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
            _value = _items[_currentIndex].title;
        }
    }

    public string GetValue() {
        return _value;
    }
}
