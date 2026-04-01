using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaredSlimePlayerDetector : MonoBehaviour
{
    [SerializeField] private float detectionRange = 5f;
    private ScaredSlimeMovement _scaredSlimeMovement;
    private Transform _playerTransform;

    private void Start()
    {
        _scaredSlimeMovement = GetComponentInParent<ScaredSlimeMovement>();
    }

    private void Update()
    {
        _playerTransform = FindPlayerInRange();
        _scaredSlimeMovement.SetScared(_playerTransform != null);
    }

    private Transform FindPlayerInRange()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, detectionRange);
        foreach (Collider hit in hits)
        {
            if (hit.CompareTag("Player"))
            {
                return hit.transform;
            }
        }
        return null;
    }

    public Transform GetPlayerTransform()
    {
        if (_playerTransform != null) return _playerTransform;
        return FindPlayerInRange();
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
    }
}
