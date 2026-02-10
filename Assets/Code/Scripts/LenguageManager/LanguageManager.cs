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
        //languageDropdown.value = ID;
        ChangeLanguage(ID);
        //languageDropdown.onValueChanged.AddListener(ChangeLanguage);
    }

    private void Update()
    {
        ChangeLanguage(0);
    }

    public void ChangeLanguage(int localeID)
    {
        _language = _languageCarrousel.GetValue();
        Debug.Log(_language);
        if (active == true)
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
