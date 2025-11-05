using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HungerSystem : MonoBehaviour
{

    // --------------------------------------------LINKED SCRIPTS--------------------------------------------\\
    private FoodDetector foodDetector;
    [SerializeField] private GemSystem gemSystem;



    [Header("TEMPORIZADOR HAMBRE")]
    [SerializeField] private float hungerTimeReset;
    private float hungerTimer;
    [SerializeField] private bool hungry;

    private void Start()
    {
        foodDetector = GetComponent<FoodDetector>();
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
        foodDetector.RemoveFood(food);
        StartCoroutine(gemSystem.SpawnGem());
    }

    public bool IsHungry()
    {
        return hungry;
    }

}
