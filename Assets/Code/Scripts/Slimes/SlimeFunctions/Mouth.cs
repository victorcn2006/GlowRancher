using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mouth : MonoBehaviour
{
    [Header("LINKED SCRIPTS")]
    private HungerSystem hungerSystem;
    private void Start()
    {
        hungerSystem = GetComponentInParent<HungerSystem>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Food") && hungerSystem.IsHungry())
        {
            if (other.TryGetComponent<Food>(out Food food))
            {
                if (food.isBeingEaten) return;
                
                food.isBeingEaten = true;
                hungerSystem.Feed(food.gameObject);
            }
        }
    }
}
