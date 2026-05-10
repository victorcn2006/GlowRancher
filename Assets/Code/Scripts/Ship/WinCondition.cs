using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Playables;

public class WinCondition : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private PlayableDirector _director;
    [Tooltip("The root object of the cutscene (e.g., Cutscene_Final) which should be inactive by default.")]
    [SerializeField] private GameObject _cutsceneRoot;
    [SerializeField] private GameObject _player;

    private bool _alreadyTriggered;

    private void Start()
    {
        // Ensure the cutscene is hidden at the start
        if (_cutsceneRoot != null)
        {
            _cutsceneRoot.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player") || _alreadyTriggered) return;

        _alreadyTriggered = true;
        StartCoroutine(PlayEndingAndLoadScene());
    }

    private IEnumerator PlayEndingAndLoadScene()
    {
        // 1. Handle Data/Stats
        if (GameManager.Instance != null)
        {
            GameManager.Instance.AddPlayerGamePassed();
            if (WalletCurrency.instance != null)
                GameManager.Instance.AddMoneyAmount(WalletCurrency.instance.bank);
            GameManager.Instance.SaveStats();
        }

        // 2. Switch Cameras & Disable Player
        if (_player != null) 
        {
            _player.SetActive(false); // This also deactivates the child camera and stops all player logic
        }
        else
        {
            // Fallback just in case it wasn't assigned in the inspector
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
            if (playerObj != null) playerObj.SetActive(false);
        }

        // Activate the cutscene object
        if (_cutsceneRoot != null) 
        {
            _cutsceneRoot.SetActive(true);
        }

        // 3. Play Cinematic
        if (_director != null)
        {
            _director.Play();
            // Wait until the director stops playing
            yield return new WaitUntil(() => _director.state != PlayState.Playing);
        }
        else
        {
            Debug.LogWarning("[WinCondition] No PlayableDirector assigned for the final cinematic!");
            yield return new WaitForSeconds(1f); // Brief fallback wait
        }

        // 4. Transition to Stats scene
        SceneManager.LoadScene("Stats");
    }
}
