using UnityEngine;
using UnityEngine.SceneManagement;
public class WinCondition : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if(GameManager.Instance != null)
            {
                GameManager.Instance.AddPlayerGamePassed();
                GameManager.Instance.AddMoneyAmount(WalletCurrency.instance.bank);
            }
            GameManager.Instance.SaveStats();
            SceneManager.LoadScene("Stats");
        }
    }
}
