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
        // Handle standard food
        if (other.CompareTag("Food") && hungerSystem != null && hungerSystem.IsHungry())
        {
            if (other.TryGetComponent<Food>(out Food food))
            {
                if (food.isBeingEaten) return;
                
                food.isBeingEaten = true;
                hungerSystem.Feed(food.gameObject);
            }
        }

        // Handle Predation (IEatable)
        if (other.GetComponent<IEatable>() != null)
        {
            // Only Corrupt Slimes (or entities with the Attack parameter) should "eat" other slimes
            Animator anim = GetComponentInParent<Animator>();
            if (anim != null)
            {
                // Check if we are a predator (e.g., by checking if we have the Attack trigger)
                // In a more robust system, we might check for a PredatorySlime component
                if (transform.parent.GetComponent<CorruptSlime>() != null)
                {
                    EatPrey(other.gameObject, anim);
                }
            }
        }
    }

    private void EatPrey(GameObject prey, Animator anim)
    {
        anim.SetTrigger("Attack");
        StartCoroutine(PredationSequence(prey));
    }

    private IEnumerator PredationSequence(GameObject prey)
    {
        // Increased delay to better sync with the "bite" moment of the animation
        yield return new WaitForSeconds(0.7f);

        if (prey != null)
        {
            prey.SetActive(false);
        }
    }
}
