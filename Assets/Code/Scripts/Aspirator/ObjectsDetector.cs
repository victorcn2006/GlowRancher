using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ObjectsDetector : MonoBehaviour
{

    private List<GameObject> aspirableObjectsList = new List<GameObject>();

    private Vector3 point1;
    private Vector3 point2;


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Food") || other.CompareTag("Slime") || other.CompareTag("Gem"))
        {
            aspirableObjectsList.Add(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (aspirableObjectsList.Contains(other.gameObject))
        {
            other.GetComponent<IAspirable>().StopBeingAspired();
            aspirableObjectsList.Remove(other.gameObject);
        }
    }

    public List<GameObject> GetAspirableObjects()
    {
        return aspirableObjectsList;
    }

}
