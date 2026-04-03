using UnityEngine;

public class IceStormArea : MonoBehaviour
{
    public bool _iceAreaActivated;
    [SerializeField] private int _damageAmount;
    private float _timer = 0f;
    [SerializeField] private float _damageInterval = 1f;
    private const float ICE_POINTS = 2f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player")) _iceAreaActivated = true;
    }

    private void OnTriggerStay(Collider other)
    {
        if (_iceAreaActivated && ICE_POINTS > 0)
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
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player")) _iceAreaActivated = false;
        _timer = 0f;
    }

}
