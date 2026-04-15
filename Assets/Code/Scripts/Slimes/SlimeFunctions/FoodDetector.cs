using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodDetector : MonoBehaviour
{
    private List<Transform> _detectedFood = new List<Transform>();

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Food"))
        {
            if (other.TryGetComponent<Food>(out Food food))
            {
                if (!food.isBeingEaten)
                {
                    _detectedFood.Add(other.transform);
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Food"))
        {
            _detectedFood.Remove(other.transform);
        }
    }

    public Transform GetClosestFood(Vector3 currentPosition)
    {
        // Clean up the list for any null (destroyed/inactive) entries
        _detectedFood.RemoveAll(food => food == null || !food.gameObject.activeInHierarchy);

        Transform closest = null;
        float minDistance = Mathf.Infinity;

        foreach (Transform food in _detectedFood)
        {
            if (food.TryGetComponent<Food>(out Food foodScript) && foodScript.isBeingEaten)
                continue;

            float distance = Vector3.Distance(currentPosition, food.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                closest = food;
            }
        }

        return closest;
    }
}
