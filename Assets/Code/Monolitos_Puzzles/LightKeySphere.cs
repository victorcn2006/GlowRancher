using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightKeySphere : MonoBehaviour, IInteractive
{
    [SerializeField] private MonolitoManager _monolitoManager;
    public void OnInteract()
    {
        _monolitoManager.ActivateMonolito();
    }

    private void OnEnable()
    {
        //efecto guay para aparecer
    }
}
