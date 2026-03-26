using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ObjectsDetector : MonoBehaviour
{

    private List<GameObject> _aspirableObjectsList = new List<GameObject>();

    private Vector3 _point1;
    private Vector3 _point2;


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Food") || other.CompareTag("Slime") || other.CompareTag("Gem"))
        {
            _aspirableObjectsList.Add(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (_aspirableObjectsList.Contains(other.gameObject))
        {
            other.GetComponent<IAspirable>().StopBeingAspired();
            _aspirableObjectsList.Remove(other.gameObject);
        }
    }

    public void RemoveTargetFromAspirableObjectList(GameObject target)
    {
        if (_aspirableObjectsList.Contains(target))
        {
            _aspirableObjectsList.Remove(target);
        }
    }

    public List<GameObject> GetAspirableObjects()
    {
        return _aspirableObjectsList;
    }

}
