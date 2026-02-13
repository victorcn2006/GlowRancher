using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UICarrousel : MonoBehaviour
{

    public event Action<string> OnValueChanged;

    [Header("Settings")]
    [SerializeField] private List<string> options;

    private TextMeshProUGUI textToChange;
    private int currentIndex = 0;

    private void Start()
    {
        textToChange = GetComponentInChildren<TextMeshProUGUI>();
        UpdateText();
    }

    public void Next()
    {
        if (options == null || options.Count == 0) return;

        currentIndex++;
        if (currentIndex >= options.Count)
        {
            currentIndex = 0; // Wrap around to the beginning
        }
        UpdateText();
    }

    public void Previous()
    {
        if (options == null || options.Count == 0) return;

        currentIndex--;
        if (currentIndex < 0)
        {
            currentIndex = options.Count - 1; // Wrap around to the end
        }
        UpdateText();
    }

    private void UpdateText()
    {
        if (textToChange != null && options != null && options.Count > 0)
        {
            textToChange.text = options[currentIndex];
            OnValueChanged?.Invoke(options[currentIndex]);
        }
    }

    // Helper methods to programmatically control the carousel
    public void SetOptionByName(string optionName)
    {
        int index = options.IndexOf(optionName);
        if (index >= 0)
        {
            currentIndex = index;
            UpdateText();
        }
    }
    public void SetOptionByIndex(int index)
    {
        if (index >= 0 && index < options.Count)
        {
            currentIndex = index;
            UpdateText();
        }
    }

    public string GetValue()
    {
        return textToChange.text;
    }

    public int GetCurrentIndex()
    {
        return currentIndex;
    }
}
