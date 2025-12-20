using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class WikiManager : MonoBehaviour
{
    [Header("Wiki panel to display")]
    [SerializeField] private WikiSlime wikiMenu;

    private bool isWikiActive = false;

    private void Start()
    {

        wikiMenu.DesactiveWiki();
        if (InputManager.Instance != null)
        {

            InputManager.Instance.OnWikiToggled += ToggleWiki;
        }
        else
        {
            Debug.LogError("WikiManager: InputManager instance not found!");
        }
    }
    private void OnDestroy() {

        if (InputManager.Instance != null)
        {
            InputManager.Instance.OnWikiToggled -= ToggleWiki;
        }
    }
    private void ToggleWiki() {
        // Don't allow opening wiki if game is paused
        if (InputManager.Instance != null && InputManager.Instance.isPaused)
            return;

        isWikiActive = !isWikiActive;

        if (isWikiActive)
        {
            OpenWiki();
        }
        else
        {
            CloseWiki();
        }
    }
    private void OpenWiki() {
        wikiMenu.ActiveWiki();
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        Time.timeScale = 0f;

        if (InputManager.Instance != null)
        {
            InputManager.Instance.SetWikiOpen(true);
        }
        InputManager.Instance.inventoryMap.Disable();
    }

    private void CloseWiki() {
        wikiMenu.DesactiveWiki();
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = 1f;

        if (InputManager.Instance != null)
        {
            InputManager.Instance.SetWikiOpen(false);
        }
        InputManager.Instance.inventoryMap.Enable();
    }
}
