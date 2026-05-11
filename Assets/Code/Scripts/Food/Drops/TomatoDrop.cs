using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TomatoDrop : MonoBehaviour
{
    [SerializeField] private GameObject _seed;
    [SerializeField] private GameObject _crop;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Vector3 spawnPosition = transform.position + Vector3.up;
            Instantiate(_crop, spawnPosition, transform.rotation);
            Instantiate(_seed, spawnPosition, transform.rotation);

            if (Aspirator.instance != null)
                Aspirator.instance.RemoveAspirableObject(this.gameObject); // ← afegeix

            Destroy(this.gameObject);
        }

    }
}
