using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TomatoDrop : MonoBehaviour
{
    [SerializeField] private GameObject _seed;
    [SerializeField] private GameObject _crop;
    private void OnTriggerEnter(Collider other)
    {
        Vector3 spawnPosition = transform.position + Vector3.up;

        if (other.gameObject.CompareTag("Player")) {
            Instantiate(_crop, spawnPosition, transform.rotation);
            Instantiate(_seed, spawnPosition, transform.rotation);
            Destroy(this.gameObject);
        }
    }
}
