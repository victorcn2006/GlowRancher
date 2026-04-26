using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DeathCounter : MonoBehaviour
{
    private TextMeshProUGUI _deathCounterText;
    private void Start()
    {
        _deathCounterText = GetComponent<TextMeshProUGUI>();
        UpdateUI();

        if (GameManager.Instance != null)
            GameManager.Instance.OnStatsLoaded += UpdateUI;
    }

    private void OnDestroy()
    {
        if (GameManager.Instance != null)
            GameManager.Instance.OnStatsLoaded -= UpdateUI;
    }

    private void UpdateUI()
    {
        int deathCounter = GameManager.Instance.GetDeathCounter();
        _deathCounterText.text = deathCounter.ToString();
    }
}
