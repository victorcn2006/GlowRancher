using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FoodDetector : MonoBehaviour
{
    
    private List<GameObject> inRangeFoodList = new List<GameObject>();

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Food")) 
        { 
            inRangeFoodList.Add(other.gameObject);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Food"))
        {
            inRangeFoodList.Remove(other.gameObject);
        }
    }

    public GameObject GetClosestFood()
    {
        float foodDistance = 999;
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
}
