using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mouth : MonoBehaviour
{

    [Header("LINKED SCRIPTS")]
    [SerializeField] private HungerSystem _hungerSystem;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Food") && _hungerSystem.IsHungry())
        {
            _hungerSystem.Feed(other.gameObject);
            other.gameObject.SetActive(false);
        }
    }
}
