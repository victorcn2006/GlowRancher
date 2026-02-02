using UnityEngine;
public class WinCondition : MonoBehaviour
{
    [SerializeField] private GameObject _winConditionPanel;
    [SerializeField] private Player _player;
    private void Awake()
    {
        if (_winConditionPanel == null) return;
        _winConditionPanel.SetActive(false);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) _winConditionPanel.SetActive(true);
        Invoke("FreezeTime", 1f);
        //Destroy(this.player.gameObject);
    }
    private void FreezeTime()
    {
        Time.timeScale = 0f;
    }
}
