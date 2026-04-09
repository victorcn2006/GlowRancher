using UnityEngine;

public class DoorSilo : MonoBehaviour
{
    private Silo _silo;

    private void Awake()
    {
        _silo = GetComponentInParent<Silo>();
    }

    public void Interact()
    {
        if (_silo != null)
        {
            _silo.ToggleInventory();
        }
    }
}
