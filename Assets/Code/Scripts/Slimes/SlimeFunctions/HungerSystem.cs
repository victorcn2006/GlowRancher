using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HungerSystem : MonoBehaviour
{
    private ISlime _slime;
    // --------------------------------------------LINKED SCRIPTS--------------------------------------------\\
    [SerializeField] private GemSystem gemSystem;


    [Header("TEMPORIZADOR HAMBRE")]
    [SerializeField] private float hungerTimeReset;
    private float hungerTimer;
    [SerializeField] private bool hungry;

    private void Start()
    {
        _slime = GetComponent<ISlime>();
        if (gemSystem == null) gemSystem = GetComponentInChildren<GemSystem>();
        hungry = true;
        hungerTimer = 0;
    }
    private void Update()
    {
        if (hungerTimer > 0) hungerTimer -= Time.deltaTime;
        else hungry = true;
    }

    public void Feed(GameObject food)
    {
        hungry = false;
        hungerTimer = hungerTimeReset;
        StartCoroutine(EatSequence(food));
    }

    private IEnumerator EatSequence(GameObject food)
    {
        if (food != null)
        {
            // Disable physics immediately so it doesn't fight the movement
            if (food.TryGetComponent<Rigidbody>(out Rigidbody rb)) rb.isKinematic = true;
            
            // Move food to the mouth position smoothly
            food.transform.DOMove(_slime.mouth.transform.position, 0.2f).SetEase(Ease.InSine);
            food.transform.SetParent(_slime.mouth.transform);
        }

        // Small delay to let the food reach the mouth before opening it
        yield return new WaitForSeconds(0.2f);

        _slime.animator.SetBool("Eat", true);
        
        yield return new WaitForSeconds(1f); // Duration of eat animation
        
        if (food != null) 
        {
            food.SetActive(false);
            // Prepare for reuse: reset parent and kinematic state
            food.transform.SetParent(null);
            if (food.TryGetComponent<Rigidbody>(out Rigidbody rb)) rb.isKinematic = false;
            if (food.TryGetComponent<Food>(out Food foodScript)) foodScript.isBeingEaten = false;
        }
        
        _slime.animator.SetBool("Eat", false);
        
        // Now start spawning the gem
        StartCoroutine(gemSystem.SpawnGem());
    }

    public bool IsHungry()
    {
        return hungry;
    }

}
