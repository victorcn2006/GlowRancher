using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RFoodDetector : MonoBehaviour
{

    private RSlime _RSlime;
    public List<GameObject> inRangeFoodList = new List<GameObject>();

    private void Start()
    {
        _RSlime = GetComponentInParent<RSlime>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Food"))
        {    

            foreach (GameObject food in _RSlime.hungerSystem.GetSlimeLikedFoods())
            {
                if (other.GetComponent<Food>().foodName == food.GetComponent<Food>().foodName)
                {
                    inRangeFoodList.Add(other.gameObject);
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (inRangeFoodList.Contains(other.gameObject))
        {
            inRangeFoodList.Remove(other.gameObject);
        }
    }

    public GameObject GetClosestFood()
    {
        float foodDistance = float.PositiveInfinity;
        GameObject closestFood = null;
        if (FoodOnInRangeFoodList())
        {
            foreach (GameObject food in inRangeFoodList)
            {
                float ActualFoodDistance = Vector3.SqrMagnitude(transform.position - food.transform.position);
                if (ActualFoodDistance < foodDistance)
                {
                    closestFood = food;
                    foodDistance = ActualFoodDistance;
                }
            }

            return closestFood;
        }
        else
        {
            return null;
        }
    }

    public void RemoveFood(GameObject food)
    {
        inRangeFoodList.Remove(food);
    }

    public bool FoodOnInRangeFoodList()
    {
        return inRangeFoodList.Any();
    }

    public List<GameObject> GetInRangeFoodList()
    {
        return inRangeFoodList;
    }

}
