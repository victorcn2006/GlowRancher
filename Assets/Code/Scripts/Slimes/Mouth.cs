using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mouth : MonoBehaviour
{

    [Header("LINKED SCRIPTS")]
    [SerializeField] private HungerSystem hungerSystem;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Food") && hungerSystem.IsHungry())
        {
            hungerSystem.Feed(other.gameObject);
            other.gameObject.SetActive(false);
        }
    }
}
