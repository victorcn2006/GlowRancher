using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class House : MonoBehaviour
{
    [SerializeField] private GameObject _spawnPoint;
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player")) other.transform.position = _spawnPoint.transform.position;
    }
}
