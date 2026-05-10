using UnityEngine;

public class IceArea : MonoBehaviour
{
    public bool _iceAreaActivated;
    public int _damageAmount = 5;
    private float timer = 0f;
    [SerializeField] private float _damageInterval = 1f;
    public float _baseIcePoints = 1f;
    
    [SerializeField] private float _catSlimeDetectionRadius = 5f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _iceAreaActivated = true;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        // Check for nearby Cat Slimes around the player to provide protection
        int resistance = GetNearbyCatSlimeResistance(other.transform.position);
        
        float activeIcePoints = _baseIcePoints - resistance;
        if (activeIcePoints > 0)
        {
            timer += Time.deltaTime;
            if (timer >= _damageInterval)
            {
                IDamageable damageable = other.GetComponent<IDamageable>();
                if (damageable != null)
                {
                    damageable.TakeDamage(_damageAmount);
                }
                timer = 0f;
            }
        }
        else
        {
            // Reset timer if protected to avoid "burst" damage when stepping out of protection
            timer = 0f;
        }
    }

    private int GetNearbyCatSlimeResistance(Vector3 playerPosition)
    {
        int totalResistance = 0;
        Collider[] nearbySlimes = Physics.OverlapSphere(playerPosition, _catSlimeDetectionRadius);
        
        foreach (var slimeCollider in nearbySlimes)
        {
            SlimeBehaviorCat catSlime = slimeCollider.GetComponent<SlimeBehaviorCat>();
            if (catSlime != null)
            {
                // We could sum them up, or just take the max. Let's sum them for now.
                totalResistance += catSlime.ResistanceValue;
            }
        }
        return totalResistance;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _iceAreaActivated = false;
            timer = 0f;
        }
    }

    private void OnDrawGizmosSelected()
    {
        // Visual indicator of protection radius around the area (or just for debugging)
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, _catSlimeDetectionRadius);
    }
}
