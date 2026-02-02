using UnityEngine;

public class WikiManager : MonoBehaviour
{
    [SerializeField] private WikiSlime _wikiMenu;
    private bool _isWikiActive;

    private void OnEnable()
    {
        InputManager.Instance.OnWikiPerformed.AddListener(ToggleWiki);
    }

    private void OnDisable()
    {
        InputManager.Instance.OnWikiPerformed.RemoveListener(ToggleWiki);
    }

    private void Start()
    {
        _wikiMenu.DesactiveWiki();
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        InputManager.Instance.SetWikiOpen(false);
    }

    private void ToggleWiki()
    {
        if (InputManager.Instance.IsPaused)
            return;

        _isWikiActive = !_isWikiActive;

        if (_isWikiActive)
            OpenWiki();
        else
            CloseWiki();
    }

    private void OpenWiki()
    {
        _wikiMenu.ActiveWiki();
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        Time.timeScale = 0f;
        InputManager.Instance.SetWikiOpen(true);
    }

    private void CloseWiki()
    {
        _wikiMenu.DesactiveWiki();
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = 1f;
        InputManager.Instance.SetWikiOpen(false);
    }
}
