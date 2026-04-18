using UnityEngine;

public class Incinerator : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        ProcessIncineration(collision.gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        ProcessIncineration(other.gameObject);
    }

    private void ProcessIncineration(GameObject target)
    {
        // Use IDamageable if available for proper death logic
        IDamageable damageable = target.GetComponent<IDamageable>();
        if (damageable != null)
        {
            damageable.TakeDamage(999);
            return;
        }

        // Fallback for other aspirable items that might not be damageable
        if (target.GetComponent<IAspirable>() != null)
        {
            target.SetActive(false);
        }
    }
}
