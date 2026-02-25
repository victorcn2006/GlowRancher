using UnityEngine;

public class DoorSilo : MonoBehaviour
{
    private Silo _silo;

    private void Awake()
    {
        _silo = GetComponentInParent<Silo>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _silo.SetPlayerInside(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _silo.SetPlayerInside(false);
        }
    }
}
