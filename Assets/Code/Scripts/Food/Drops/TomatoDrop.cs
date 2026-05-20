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
            // Check if we are part of a growing plant
            VegetableData plant = GetComponentInParent<VegetableData>();
            if (plant != null)
            {
                plant.Harvest();
                return;
            }

            // Original logic for wild drops
            Vector3 spawnPosition = transform.position + Vector3.up;
            if (_crop != null) Instantiate(_crop, spawnPosition, transform.rotation);
            if (_seed != null) Instantiate(_seed, spawnPosition, transform.rotation);

            if (Aspirator.instance != null)
                Aspirator.instance.RemoveAspirableObject(this.gameObject);

            Destroy(this.gameObject);
        }
    }
}
