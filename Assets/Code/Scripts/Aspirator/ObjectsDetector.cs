using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class ObjectsDetector : MonoBehaviour
{

    private List<GameObject> _aspirableObjectsList = new List<GameObject>();

    [SerializeField] private GameObject _keyboardSprite;
    [SerializeField] private GameObject _controllerSprite;

    private Vector3 _point1;
    private Vector3 _point2;


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Food") || other.CompareTag("Slime") || other.CompareTag("Gem") || other.CompareTag("Seed"))
        {
            _aspirableObjectsList.Add(other.gameObject);
        }

        if (other.CompareTag("InteractuableShop") || other.CompareTag("Monolito") || other.CompareTag("InteractuableObject"))
        {
            if (Gamepad.current != null && Gamepad.current.wasUpdatedThisFrame)
            {
                _controllerSprite.SetActive(true);
            }
            else
            {
                _keyboardSprite.SetActive(true);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (_aspirableObjectsList.Contains(other.gameObject))
        {
            if (other.TryGetComponent<IAspirable>(out var aspirable))
            {
                aspirable.StopBeingAspired();
            }
            _aspirableObjectsList.Remove(other.gameObject);
        }

        _keyboardSprite.SetActive(false);
        _controllerSprite.SetActive(false);

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
