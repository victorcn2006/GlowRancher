using System.Linq;
using UnityEngine;
using UnityEngine.Localization.Settings;

public class LanguageManager : MonoBehaviour
{
    [SerializeField] private UICarrousel languageCarrousel;

    //Key to save the language in PlayerPrefs
    private const string LANGUAGE_PREF_KEY = "Language";

    private const string DEFAULT_LANGUAGE = "English";

    private string languageText;

    private void Start()
    {
        if (languageCarrousel == null)
        {
            Debug.LogError("LanguageManager: UICarrousel reference is missing!");
            return;
        }
        //Check if there is a saved language, if not, use the default language
        string savedLanguage = PlayerPrefs.GetString(LANGUAGE_PREF_KEY, DEFAULT_LANGUAGE);
        languageCarrousel.SetOptionByName(savedLanguage);

        UpdateLocalization(savedLanguage);

        languageCarrousel.OnValueChanged += OnLanguageChanged;
    }

    private void OnLanguageChanged(string newLanguage)
    {
        PlayerPrefs.SetString(LANGUAGE_PREF_KEY, newLanguage);
        PlayerPrefs.Save();
        UpdateLocalization(newLanguage);
    }

    private void UpdateLocalization(string languageName)
    {
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
        }
        else
        {
            Debug.LogWarning($"No matching locale found for: {languageName}. Available locales: {string.Join(", ", availableLocales.Select(l => l.LocaleName))}");
        }
    }

    private void OnDestroy()
    {
        // Unsubscribe to prevent memory leaks
        if (languageCarrousel != null)
        {
            languageCarrousel.OnValueChanged -= OnLanguageChanged;
        }
    }

    private string GetLanguageCode(string languageName)
    {
        // Map language names to ISO codes
        switch (languageName)
        {
            case "English": return "en";
            case "Spanish": return "es";
            default: return languageName.ToLower();
        }
    }
}
