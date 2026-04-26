using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimePlayed : MonoBehaviour
{
    private TextMeshProUGUI _playedText;
    private void Start()
    {
        _playedText = GetComponent<TextMeshProUGUI>();
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
        float timePlayed = GameManager.Instance.GetCurrentTimePlayed();
        TimeSpan t = TimeSpan.FromSeconds(timePlayed);
        _playedText.text = string.Format("{0:D2}h {1:D2}m {2:D2}s", 
            (int)t.TotalHours, 
            t.Minutes, 
            t.Seconds);
    }
}
