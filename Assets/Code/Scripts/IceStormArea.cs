using UnityEngine;

public class IceStormArea : MonoBehaviour
{
    public bool _iceAreaActivated;
    public int _damageAmount = 10;
    private float _timer = 0f;
    [SerializeField] private float _damageInterval = 1f;
    public float _baseIcePoints = 2f;
    
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
            _timer += Time.deltaTime;
            if (_timer >= _damageInterval)
            {
                IDamageable damageable = other.GetComponent<IDamageable>();
                if (damageable != null)
                {
                    damageable.TakeDamage(_damageAmount);
                }
                _timer = 0f;
            }
        }
        else
        {
            _timer = 0f;
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
            _timer = 0f;
        }
    }
}
