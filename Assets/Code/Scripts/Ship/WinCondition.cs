
using UnityEngine;


public class WinCondition : MonoBehaviour
{
    [SerializeField] private GameObject winConditionPanel;

    private void Awake()
    {
        if (winConditionPanel == null) return;
        winConditionPanel.SetActive(false);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) winConditionPanel.SetActive(true);
        Invoke("FreezeTime", 1f);
        
    }
    private void FreezeTime()
    {
        Time.timeScale = 0f;
    }
}
