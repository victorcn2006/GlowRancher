using UnityEngine;

public class SlimeBehaviorCat : MonoBehaviour
{
    [SerializeField] private float healingRadius = 5f;
    [SerializeField] private int healAmount = 1;
    [SerializeField] private float healCooldown = 2f;

    private float _lastHealTime;

    private void Update()
    {
        if (Time.time >= _lastHealTime + healCooldown)
        {
            HealNearbyPlayers();
        }
    }

    private void HealNearbyPlayers()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, healingRadius);
        bool healedAny = false;
        foreach (var col in colliders)
        {
            if (col.CompareTag("Player"))
            {
                IDamageable damageable = col.GetComponent<IDamageable>();
                if (damageable != null)
                {
                    damageable.Heal(healAmount);
                    healedAny = true;
                }
            }
        }

        if (healedAny)
        {
            _lastHealTime = Time.time;
            // Optional: Play VFX/Audio here
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, healingRadius);
    }
}
