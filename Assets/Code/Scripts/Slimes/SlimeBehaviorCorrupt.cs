using UnityEngine;

public class SlimeBehaviorCorrupt : MonoBehaviour
{
    [SerializeField] private float detectionRadius = 5f;
    [SerializeField] private float chaseForce = 200f;
    [SerializeField] private int damageAmount = 1;
    [SerializeField] private float damageCooldown = 1f;

    private Transform _playerTransform;
    private Rigidbody _rb;
    private float _lastDamageTime;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        DetectPlayer();
        if (_playerTransform != null)
        {
            ChasePlayer();
        }
    }

    private void DetectPlayer()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, detectionRadius);
        foreach (var col in colliders)
        {
            if (col.CompareTag("Player"))
            {
                _playerTransform = col.transform;
                return;
            }
        }
        _playerTransform = null;
    }

    private void ChasePlayer()
    {
        Vector3 direction = (_playerTransform.position - transform.position).normalized;
        direction.y = 0; // Keep movement on the ground plane
        _rb.AddForce(direction * chaseForce * Time.deltaTime);
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (Time.time >= _lastDamageTime + damageCooldown)
            {
                IDamageable damageable = collision.gameObject.GetComponent<IDamageable>();
                if (damageable != null)
                {
                    damageable.TakeDamage(damageAmount);
                    _lastDamageTime = Time.time;
                }
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}
