using System.Collections;
using System.Collections.Generic;
using FMODUnity; // Necesario para acceder a RuntimeManager
using UnityEngine;

public class LightKeySphere : MonoBehaviour, IInteractive
{
    [SerializeField] private MonolitoManager _monolitoManager;
    [SerializeField] private EventReference _purifySound;

    public void OnInteract()
    {
        _monolitoManager.ActivateMonolito();

        if (!_purifySound.IsNull)
        {
            RuntimeManager.PlayOneShot(_purifySound, transform.position);
        }
    }

    private void OnEnable()
    {
        // efecto guay para aparecer
    }
}
