using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDEnergy : MonoBehaviour
{
    [Header("References")] 
    [SerializeField] private Player player;
    [SerializeField] private Slider slider;

    private int stamina;

    private void Start()
    {

        stamina = player.stamina;
        slider.maxValue = stamina;
        slider.value = stamina;
        
    }

    private void Update()
    {
        slider.value = stamina;
    }
}
