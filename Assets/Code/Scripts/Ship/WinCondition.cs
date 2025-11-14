
using UnityEngine;


public class WinCondition : MonoBehaviour
{
    [SerializeField] private GameObject winConditionPanel;
    [SerializeField] private Player player;
    private void Awake()
    {
        if (winConditionPanel == null) return;
        winConditionPanel.SetActive(false);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) winConditionPanel.SetActive(true);
        Invoke("FreezeTime", 1f);
        //Destroy(this.player.gameObject);
    }
    private void FreezeTime()
    {
        Time.timeScale = 0f;
    }
}
