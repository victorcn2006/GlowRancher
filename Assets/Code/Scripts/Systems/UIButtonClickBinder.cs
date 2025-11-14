using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

// Small helper that wires UIAudioManager.PlayClick to every Button.onClick in the scene.
// Attach this to a persistent UI/GameManager object (or the same object as UIAudioManager).
public class UIButtonClickBinder : MonoBehaviour
{
    private readonly Dictionary<Button, UnityAction> registered = new Dictionary<Button, UnityAction>();

    void Awake()
    {
        // Register on existing buttons now
        RegisterAllButtons();
        // Also register when scenes load
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDestroy()
    {
        UnregisterAll();
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        RegisterAllButtons();
    }

    // Finds all Buttons (including inactive) in the scene and adds a click listener.
    public void RegisterAllButtons()
    {
        // FindObjectsOfType with includeInactive = true is available in newer Unity versions.
        // If unavailable, this will still find active buttons.
        var buttons = FindObjectsOfType<Button>(true);
        foreach (var b in buttons)
        {
            if (b == null || registered.ContainsKey(b)) continue;
            UnityAction action = () => UIAudioManager.Instance?.PlayClick();
            b.onClick.AddListener(action);
            registered[b] = action;
        }
    }

    // Remove all listeners we added (cleanup)
    public void UnregisterAll()
    {
        foreach (var kv in registered)
        {
            if (kv.Key == null) continue;
            try { kv.Key.onClick.RemoveListener(kv.Value); } catch { }
        }
        registered.Clear();
    }
}
