using UnityEngine;
using UnityEngine.UI;

public class WikiSlime : MonoBehaviour
{
    [SerializeField] private GameObject _wikiMenu;
    [SerializeField] private GameObject _slime1;
    [SerializeField] private GameObject _slime2;
    [SerializeField] private Button _slime1Button;
    [SerializeField] private Button _slime2Button;

    private GameObject _currentActiveSlime;

    private void Start()
    {
        _wikiMenu.SetActive(false);

        _currentActiveSlime = _slime1;
        _slime1.SetActive(true);
        _slime2.SetActive(false);

        _slime1Button.onClick.AddListener(() => SwitchSlime(_slime1));
        _slime2Button.onClick.AddListener(() => SwitchSlime(_slime2));
    }

    public void ActiveWiki()
    {
        _wikiMenu.SetActive(true);
    }

    public void DesactiveWiki()
    {
        _wikiMenu.SetActive(false);
    }

    private void SwitchSlime(GameObject newSlime)
    {
        if (_currentActiveSlime != null)
            _currentActiveSlime.SetActive(false);

        _currentActiveSlime = newSlime;
        _currentActiveSlime.SetActive(true);
    }
}
