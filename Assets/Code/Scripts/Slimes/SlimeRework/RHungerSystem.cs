using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RHungerSystem : MonoBehaviour
{
    private RSlime _RSlime;


    [Header("ALIMENTOS QUE COME")]
    [SerializeField] private List<GameObject> likedFoods;

    [Header("TEMPORIZADOR HAMBRE")]
    [SerializeField] private float hungerBaseTime;
    private float hungerTimer;

    [SerializeField] private bool hungry;

    private void Start()
    {
        _RSlime = GetComponent<RSlime>();
        hungerTimer = hungerBaseTime;
    }

    private void Update()
    {
        if (hungerTimer > 0) {

            hungerTimer -= Time.deltaTime;
            hungry = false;
        }
        else hungry = true;
    }

    public void Eat(GameObject food)
    {
        hungry = false;
        hungerTimer = hungerBaseTime;
        _RSlime.foodDetector.RemoveFood(food);
        food.SetActive(false);
        StartCoroutine(_RSlime.gemSystem.SpawnGem());
    }

    public bool IsHungry()
    {
        return hungry;
    }

    public List<GameObject> GetSlimeLikedFoods()
    {
        return likedFoods;
    }
}
