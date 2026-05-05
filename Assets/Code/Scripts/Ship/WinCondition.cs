using System.Collections;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;
public class WinCondition : MonoBehaviour
{
    [SerializeField] private PlayableDirector _timeline;

    private bool _triggered = false;
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player") || _triggered)
            return;

        _triggered = true;

        StartCoroutine(WinSequence());
    }

    private IEnumerator WinSequence()
    {
        GameManager.Instance.SaveStats();
        // lanzar timeline
        _timeline.Play();

        // esperar hasta que termine
        yield return new WaitWhile(() => _timeline.state == PlayState.Playing);

        SceneManager.LoadScene("Stats");
    }
}
