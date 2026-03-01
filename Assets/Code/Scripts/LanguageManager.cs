using System.Linq;
using UnityEngine;
using UnityEngine.Localization.Settings;
using System.Collections;

public class LanguageManager : MonoBehaviour
{
    [SerializeField] private UICarrousel languageCarrousel;

    //Key to save the language in PlayerPrefs
    private const string LANGUAGE_PREF_KEY = "Language";
    private const string DEFAULT_LANGUAGE = "English";

    private string languageText;
    private bool _isChangingLanguage = false;

    private void Start()
    {
        if (languageCarrousel == null)
        {
            Debug.LogError("LanguageManager: UICarrousel reference is missing!");
            return;
        }

        languageCarrousel.OnValueChanged += OnLanguageSelected;

        string savedLanguage = PlayerPrefs.GetString(LANGUAGE_PREF_KEY, DEFAULT_LANGUAGE);
        StartCoroutine(UpdateLocalization(savedLanguage));
    }

    private void OnLanguageSelected(string newLanguage)
    {
        OnLanguageChanged(newLanguage);
    }
    private void OnLanguageChanged(string newLanguage)
    {
        PlayerPrefs.SetString(LANGUAGE_PREF_KEY, newLanguage);
        PlayerPrefs.Save();
        StartCoroutine(UpdateLocalization(newLanguage));
    }

    private void SetCarouselToLanguage(string languageName)
    {
        for (int i = 0; i < languageCarrousel._items.Length; i++)
        {
            if (languageCarrousel._items[i].title == languageName)
            {
                languageCarrousel.GoToIndex(i);
                break;
            }
        }
    }

    private IEnumerator UpdateLocalization(string languageName)
    {
        if (_isChangingLanguage) yield break;

        _isChangingLanguage = true;

        // Wait for localization to initialize
        yield return LocalizationSettings.InitializationOperation;

        // Get all available locales from Unity Localization
        var availableLocales = LocalizationSettings.AvailableLocales.Locales;

        // Try to find a matching locale by name or code
        var matchingLocale = availableLocales.FirstOrDefault(locale =>
            locale.LocaleName == languageName ||
            locale.Identifier.Code == languageName.ToLower() ||
            locale.Identifier.Code == GetLanguageCode(languageName)
        );

        if (matchingLocale != null)
        {
            LocalizationSettings.SelectedLocale = matchingLocale;
            Debug.Log($"Language changed to: {languageName} ({matchingLocale.Identifier.Code})");
        }
        else
        {
            Debug.LogWarning($"No matching locale found for: {languageName}. Available locales: {string.Join(", ", availableLocales.Select(l => l.LocaleName))}");
        }

        _isChangingLanguage = false;
    }

    private void OnDestroy()
    {
        // Unsubscribe to prevent memory leaks
        if (languageCarrousel != null)
        {
            languageCarrousel.OnValueChanged -= OnLanguageSelected;
        }
    }

    private void OnCarouselChanged()
    {
        string newLanguage = languageCarrousel.GetValue();
        if (string.IsNullOrEmpty(newLanguage))
        {
            Debug.LogWarning("LanguageManager: Carousel returned null or empty value!");
            return;
        }

        OnLanguageChanged(newLanguage);
    }

    private string GetLanguageCode(string languageName)
    {
        // Map language names to ISO codes
        switch (languageName)
        {
            case "English": return "en";
            case "Spanish": return "es";
            case "Español": return "es";
            default: return languageName.ToLower();
        }
    }
}
