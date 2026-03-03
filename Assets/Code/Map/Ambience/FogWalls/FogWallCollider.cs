using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FogWallCollider : MonoBehaviour
{
    [Header("AMBIENCE START")]
    [SerializeField] private ColliderType _colliderType;


    [Header("SCRIPTS NEEDED")]
    [SerializeField] private FogWallSet _fogWallSet;

    public enum ColliderType
    {
        ENTRY,
        EXIT
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_colliderType == ColliderType.ENTRY)
        {
            _fogWallSet.OnFogEntry();
        }
        else
        {
            _fogWallSet.OnFogExit();
        }
    }
}
