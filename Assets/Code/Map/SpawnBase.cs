using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBase : MonoBehaviour
{
    [SerializeField] private GameObject _house;
    [SerializeField] private GameObject _shop;
    [SerializeField] private GameObject _cage;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _house.SetActive(true);
            _shop.SetActive(true);
            _cage.SetActive(true);

        }
    }
}
