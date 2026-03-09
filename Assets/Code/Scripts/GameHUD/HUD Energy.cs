using UnityEngine;
using UnityEngine.UI;

public class HUDEnergy : MonoBehaviour
{
    [SerializeField] private Player player;
    [SerializeField] private Slider energySlider;

    private void Start()
    {
        if (player != null && energySlider != null)
        {
            // Configurar el slider con los valores iniciales del Player
            energySlider.minValue = 0;
            energySlider.maxValue = player.GetMaxEnergy();
            energySlider.value = player.GetCurrentEnergy();
        }
        else
        {
            Debug.LogWarning("Faltan referencias en HUDEnergy: Player o Slider no asignados.");
        }
    }

    private void Update()
    {
        if (player != null && energySlider != null)
        {
            // Actualizar el valor visual cada frame
            energySlider.value = player.GetCurrentEnergy();
        }
    }
}
