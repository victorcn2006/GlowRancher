using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HungerSystem : MonoBehaviour
{

    // --------------------------------------------LINKED SCRIPTS--------------------------------------------\\
    private FoodDetector _foodDetector;
    [SerializeField] private GemSystem _gemSystem;



    [Header("TEMPORIZADOR HAMBRE")]
    [SerializeField] private float _hungerTimeReset;
    private float _hungerTimer;
    [SerializeField] private bool _hungry;

    private void Start()
    {
        _foodDetector = GetComponent<FoodDetector>();
        _hungry = true;
        _hungerTimer = 0;
    }
    private void Update()
    {
        if (_hungerTimer > 0) _hungerTimer -= Time.deltaTime;
        else _hungry = true;
    }

    public void Feed(GameObject food)
    {
        _hungry = false;
        _hungerTimer = _hungerTimeReset;
        _foodDetector.RemoveFood(food);
        StartCoroutine(_gemSystem.SpawnGem());
    }

    public bool IsHungry()
    {
        return _hungry;
    }

}
