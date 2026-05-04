using System;
using TMPro;
using UnityEngine;

using UnityEngine.UI;
using static UnityEditor.Progress;

public class UICarrousel : MonoBehaviour
{
    [System.Serializable]
    public class CarrouselItem
    {
        public string title;
        public Color textColor;
    }

    public Action<CarrouselItem> OnItemChanged;

    [SerializeField] private Button _prevButton;
    [SerializeField] private Button _nextButton;
    [SerializeField] private TextMeshProUGUI _textToChange;
    [SerializeField] public CarrouselItem[] _items;

    private int _currentIndex = 0;

    private void OnEnable()
    {
        _prevButton.onClick.AddListener(PrevButton);
        _nextButton.onClick.AddListener(NextButton);
    }
    private void Start()
    {
        _textToChange.text = _items[0].title;

    }

    private void Update()
    {
        UpdateUI();
    }

    private void OnDisable()
    {
        _nextButton.onClick.RemoveListener(NextButton);
        _prevButton.onClick.RemoveListener(PrevButton);
    }
    private void PrevButton()
    {
        _currentIndex--;

        if (_currentIndex < 0)
            _currentIndex = _items.Length - 1;
        UpdateUI();
    }

    private void NextButton()
    {
        _currentIndex++;

        if (_currentIndex >= _items.Length)
            _currentIndex = 0;
        UpdateUI();
    }

    private void UpdateUI()
    {
        var item = _items[_currentIndex];
        _textToChange.text = _items[_currentIndex].title;
        _textToChange.color = _items[_currentIndex].textColor;
        OnItemChanged?.Invoke(item);
    }

    public CarrouselItem GetItem() {
        return _items[_currentIndex];
    }
}
