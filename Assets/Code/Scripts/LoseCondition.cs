using UnityEngine;

public class LoseCondition : MonoBehaviour
{

    [SerializeField] private Player _player;
    [SerializeField] private Transform _spawnPoint;// Posición a la que se teletransportará
    [SerializeField] private float _delayTp;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Inicia la corutina para esperar y luego teletransportar
            StartCoroutine(TeleportAfterDelay(_delayTp));
        }
    }

    private System.Collections.IEnumerator TeleportAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        if (_player != null && _spawnPoint != null)
        {
            _player.transform.position = _spawnPoint.position; // Teletransporta al spawn
        }
    }
}
