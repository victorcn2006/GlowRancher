using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Serialization;

public class HUDHealth : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Player player;
    [SerializeField] private Slider slider;
    
    private int maxHealth;
    private int currentHealth;

    private void Start()
    {
        // Inicializar maxHealth y currentHealth
        maxHealth = player.GetMaxHealth();
        currentHealth = player.GetCurrentHealth();
        
        // Establecer el valor máximo del slider y el valor inicial
        slider.maxValue = maxHealth;
        slider.value = currentHealth;
    }

    private void Update()
    {
        // Actualizar el valor del slider cada vez que la salud del jugador cambie
        currentHealth = player.GetCurrentHealth();
        slider.value = currentHealth;
    }
}