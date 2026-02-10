using System.Collections;
using UnityEngine;
using UnityEngine.Localization.Settings;

public class LanguageManager : MonoBehaviour
{
    [SerializeField] private UICarrousel _languageCarrousel;
    private bool _isChanging = false;

    private bool active = false;
    private string _language;
    private void Start()
    {
        int ID = PlayerPrefs.GetInt("LocalKey", 0);
        ChangeLanguage(ID);
        _languageCarrousel.onValueChanged.AddListener(OnCarouselValueChanged);
    }

    private void OnCarouselValueChanged()
    {
        string languageValue = _languageCarrousel.GetValue();
        int localeID = GetLocaleIDFromLanguageCode(languageValue);
        ChangeLanguage(localeID);   
    }

    private int GetLocaleIDFromLanguageCode(string languageCode)
    {
        // Map your carousel values to locale IDs
        // Adjust these based on your Available Locales order in Unity
        switch (languageCode.ToLower())
        {
            case "english":
            case "en":
                return 0;
            case "spanish":
            case "es":
                return 1;
            default:
                Debug.LogWarning($"Unknown language: {languageCode}, defaulting to 0");
                return 0;
        }
    }

    public void ChangeLanguage(int localeID)
    {
        if (active)
            return;
        StartCoroutine(SetLocale(localeID));
    }
    IEnumerator SetLocale(int _localeID)
    {
        active = true;
        yield return LocalizationSettings.InitializationOperation;
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[_localeID];
        PlayerPrefs.SetInt("LocalKey", _localeID);
        active = false;
    }
}
