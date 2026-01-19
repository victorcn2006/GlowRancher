using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDEnergy : MonoBehaviour
{
    [Header("References")] 
    [SerializeField] private Player _player;
    [SerializeField] private Slider _slider;

    private int _stamina;

    private void Start()
    {

        _stamina = _player.stamina;
        _slider.maxValue = _stamina;
        _slider.value = _stamina;
        
    }

    private void Update()
    {
        _slider.value = _stamina;
    }
}
