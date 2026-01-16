using UnityEngine;
using FMODUnity;

public class LoseCondition : MonoBehaviour
{
    [Header("Audio de FMOD")]
    public EventReference _sonidoRespawn;
    [Header("References")]
    [SerializeField] private Player player;
    [SerializeField] private Transform spawnPoint;// Posición a la que se teletransportará
    [SerializeField] private float delayTp;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Inicia la corutina para esperar y luego teletransportar
            StartCoroutine(TeleportAfterDelay(delayTp));
        }
    }

    private System.Collections.IEnumerator TeleportAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        if (player != null && spawnPoint != null)
        {
            player.transform.position = spawnPoint.position; // Teletransporta al spawn
            if (!_sonidoRespawn.IsNull)
            {
                RuntimeManager.PlayOneShot(_sonidoRespawn, transform.position);
            }
        }
    }
}
