using UnityEngine;
using UnityEngine.UI;

public class WikiSlime : MonoBehaviour
{
    [SerializeField] private GameObject _wikiMenu;
    [SerializeField] private GameObject _slime1;
    [SerializeField] private GameObject _slime2;
    [SerializeField] private GameObject _slime3;
    [SerializeField] private GameObject _slime4;
    [SerializeField] private GameObject _slime5;
    [SerializeField] private GameObject _slime6;
    [SerializeField] private GameObject _slime7;
    [SerializeField] private Button _slime1Button;
    [SerializeField] private Button _slime2Button;
    [SerializeField] private Button _slime3Button;
    [SerializeField] private Button _slime4Button;
    [SerializeField] private Button _slime5Button;
    [SerializeField] private Button _slime6Button;
    [SerializeField] private Button _slime7Button;

    private GameObject _currentActiveSlime;

    private void Start()
    {
        // Amaga TOTS els panells d'info al començar
        _slime1.SetActive(false);
        _slime2.SetActive(false);
        _slime3.SetActive(false);
        _slime4.SetActive(false);
        _slime5.SetActive(false);
        _slime6.SetActive(false);
        _slime7.SetActive(false);

        _currentActiveSlime = null; // Cap seleccionat inicialment

        _slime1Button.onClick.AddListener(() => SwitchSlime(_slime1));
        _slime2Button.onClick.AddListener(() => SwitchSlime(_slime2));
        _slime3Button.onClick.AddListener(() => SwitchSlime(_slime3));
        _slime4Button.onClick.AddListener(() => SwitchSlime(_slime4));
        _slime5Button.onClick.AddListener(() => SwitchSlime(_slime5));
        _slime6Button.onClick.AddListener(() => SwitchSlime(_slime6));
        _slime7Button.onClick.AddListener(() => SwitchSlime(_slime7));
    }

    public void ActiveWiki()
    {
        _wikiMenu.SetActive(true);
    }

    public void DesactiveWiki()
    {
        _wikiMenu.SetActive(false);

        // Amaga la info activa quan tanques la wiki
        if (_currentActiveSlime != null)
        {
            _currentActiveSlime.SetActive(false);
            _currentActiveSlime = null;
        }
    }

    private void SwitchSlime(GameObject newSlime)
    {
        // Si cliques el mateix botó dues vegades, el tanca
        if (_currentActiveSlime == newSlime)
        {
            _currentActiveSlime.SetActive(false);
            _currentActiveSlime = null;
            return;
        }

        // Amaga l'anterior i mostra el nou
        if (_currentActiveSlime != null)
            _currentActiveSlime.SetActive(false);

        _currentActiveSlime = newSlime;
        _currentActiveSlime.SetActive(true);
    }
}
