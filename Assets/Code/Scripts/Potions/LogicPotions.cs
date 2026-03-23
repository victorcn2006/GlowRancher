using UnityEngine;

public class LogicPotions : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private Potions potionData; // Tu ScriptableObject de poción

    private void OnTriggerEnter(Collider other)
    {
        // Intentamos obtener el componente Player del objeto que choca
        if (other.TryGetComponent<Player>(out Player player))
        {
            // 1. Obtenemos la salud actual y la máxima directamente del Player
            int current = player.GetCurrentHealth();
            int max = player.GetMaxHealth();

            // 2. Verificamos si el jugador necesita curación
            if (current < max)
            {
                // 3. Curamos al jugador usando el valor 'cure' del ScriptableObject
                player.Heal(potionData.cure);

                // Feedback en consola
                Debug.Log($"Poción {potionData.potionName} usada. Curado: {potionData.cure}. Vida total: {player.GetCurrentHealth()}");

                // 4. Destruimos el objeto de la poción del mundo
                Destroy(gameObject);
            }
            else
            {
                Debug.Log("El jugador ya tiene la vida al máximo. No se gasta la poción.");
            }
        }
    }
}
