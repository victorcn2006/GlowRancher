using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization.Settings;

public class TranslLanguageManagerator : MonoBehaviour
{
    [SerializeField] private UICarrousel _languageCarrousel;
    private bool _isChanging = false;

    private void Start()
    {
        // Connect to carousel buttons directly
        _languageCarrousel.GetComponent<UICarrousel>()._prevButton.onClick.AddListener(OnLanguageChanged);
        _languageCarrousel.GetComponent<UICarrousel>()._nextButton.onClick.AddListener(OnLanguageChanged);

        string savedLanguage = PlayerPrefs.GetString("Language", "English");
        ChangeLanguage(savedLanguage);
    }

    public void OnLanguageChanged()
    {
        ChangeLanguage(_languageCarrousel.GetValue());
    }

    private void ChangeLanguage(string languageCode)
    {
        if (_isChanging) return;
        StartCoroutine(SetLanguage(languageCode));
    }

    private IEnumerator SetLanguage(string languageCode)
    {
        _isChanging = true;
        yield return LocalizationSettings.InitializationOperation;

        var locale = LocalizationSettings.AvailableLocales.GetLocale(languageCode);
        if (locale != null)
        {
            LocalizationSettings.SelectedLocale = locale;
            PlayerPrefs.SetString("Language", languageCode);
        }

        _isChanging = false;
    }
}
