using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FogWallCollider : MonoBehaviour
{
    [Header("SCRIPTS NEEDED")]
    [SerializeField] private FogWallSet _fogWallSet;

    [Header("SCRIPTS NEEDED")]
    [SerializeField] private AmbienceController.AmbienceStates _ambience;

    private void OnTriggerEnter(Collider other)
    {
        _fogWallSet.OnFogPass(_ambience);
    }
}
