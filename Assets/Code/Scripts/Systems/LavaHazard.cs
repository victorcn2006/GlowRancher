using UnityEngine;

public class LavaHazard : MonoBehaviour
{
    [SerializeField] private int _damageAmount = 999; // Default to lethal

    private void OnTriggerEnter(Collider other)
    {
        // Check if the entity is immune
        if (other.GetComponent<LavaImmune>() != null)
        {
            return;
        }

        if (other.CompareTag("Player"))
        {
            DeathScript.instance.Die();
            return;
        }

        // Apply damage if the entity is damageable
        IDamageable damageable = other.GetComponent<IDamageable>();
        if (damageable != null)
        {
            damageable.TakeDamage(_damageAmount);
        }
        else
        {
            // Fallback for non-Character slimes or other objects that might not implement IDamageable but should be destroyed
            if (other.CompareTag("Slime"))
            {
                Destroy(other.gameObject);
            }
        }
    }
}
