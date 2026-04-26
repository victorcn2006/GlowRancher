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
        float deathCounter = GameManager.Instance.GetDeathCounter();
        _deathCounterText.text = deathCounter.ToString();
    }
}
