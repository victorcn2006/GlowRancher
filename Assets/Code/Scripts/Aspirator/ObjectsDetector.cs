using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ObjectsDetector : MonoBehaviour
{

    private List<GameObject> aspirableObjectsList = new List<GameObject>();

    private Vector3 point1;
    private Vector3 point2;

    void Update()
    {
        foreach (GameObject obj in aspirableObjectsList)
        {
            Debug.Log(obj.gameObject.name);
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Food") || other.CompareTag("Slime"))
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
