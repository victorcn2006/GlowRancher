using UnityEngine;

public class SlimeBehaviorFire : MonoBehaviour
{
    [SerializeField] private int damageAmount = 20;

    private void OnCollisionEnter(Collision collision)
    {
        IDamageable damageable = collision.gameObject.GetComponent<IDamageable>();
        if (damageable != null)
        {
            damageable.TakeDamage(damageAmount);
        }
    }
}
