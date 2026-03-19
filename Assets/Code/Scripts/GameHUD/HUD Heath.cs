using UnityEngine;
using UnityEngine.UI;

public class HUDHealth : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Player player;
    [SerializeField] private Slider slider;

    private void Start()
    {
        if (player != null && slider != null)
        {
            // Configuramos el valor máximo del Slider al inicio
            slider.maxValue = player.GetMaxHealth();
            slider.value = player.GetCurrentHealth();
        }
    }

    private void Update()
    {
        if (player != null && slider != null)
        {
            // El slider se actualiza cada frame reflejando la vida actual del Player
            slider.value = player.GetCurrentHealth();
        }
    }
}
