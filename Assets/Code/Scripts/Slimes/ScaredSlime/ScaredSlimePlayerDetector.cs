using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaredSlimePlayerDetector : MonoBehaviour
{

    private ScaredSlimeMovement _scaredSlimeMovement;
    private Transform _playerPosition;

    private void Start()
    {
        _scaredSlimeMovement = GetComponentInParent<ScaredSlimeMovement>();
        Debug.Log(_scaredSlimeMovement);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) _scaredSlimeMovement.SetScared(true);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player")) _scaredSlimeMovement.SetScared(false);
    }

    public Transform GetPlayerTransform()
    {
        SphereCollider sphere = GetComponent<SphereCollider>();

        float worldRadius = sphere.radius * Mathf.Max(transform.lossyScale.x, transform.lossyScale.y, transform.lossyScale.z);

        Collider[] hits = Physics.OverlapSphere(transform.position + sphere.center, worldRadius);


        foreach (Collider hit in hits)
        {
            if (hit.CompareTag("Player"))
            {
                return hit.transform;
            }
        }
        return null;
    }
}
