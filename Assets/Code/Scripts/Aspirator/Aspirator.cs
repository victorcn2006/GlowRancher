using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Aspirator : MonoBehaviour
{
    public static Aspirator instance;

    [SerializeField] private InputActionReference _aspirate;
    [SerializeField] private InputActionReference _launchObject;

    [SerializeField] private ObjectsDetector _objectsDetector;

    [SerializeField] private SuctionPoint _suctionPoint;

    [SerializeField] private Inventory _inventory;
    private List<GameObject> _aspirableObjectsList = new List<GameObject>();

    private bool _aspirating;

    [SerializeField] private float _aspirateForce;
    [SerializeField] private float _launchForce;
    [SerializeField] private Transform _aspiratePoint;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }

    }

    private void Start()
    {

        _aspirate.action.performed += SetAspirate;
        _aspirate.action.canceled += SetAspirate;

        _launchObject.action.performed += LaunchObject;

        _aspirate.action.Enable();
        _aspirating = false;  
    }

    private void Update()
    {

        if (_aspirating)
        {
            AspirateObjects();
        }
    }

    private void AspirateObjects()
    {
        _aspirableObjectsList = _objectsDetector.GetAspirableObjects();

        foreach (GameObject obj in _aspirableObjectsList)
        {
            obj.GetComponent<IAspirable>().BeingAspired();

            float distance = Vector3.Distance(obj.transform.position, _aspiratePoint.position);
            float forceFactor = Mathf.Clamp01(1f - (distance / 10f)); // 10 = rango máximo de succión
            Vector3 aspirateDirection = (_aspiratePoint.position - obj.transform.position).normalized;
            obj.GetComponent<Rigidbody>().AddForce(aspirateDirection * _aspirateForce * forceFactor, ForceMode.Force);

        }
    }
    public void LaunchObject(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            //codigo para disparar el objeto 

            GameObject objectToLaunch = PoolManager.Instance.GetFirstAvailableObject(_inventory.UsarItemSeleccionado());
            _objectsDetector.RemoveTargetFromAspirableObjectList(objectToLaunch);
            if (objectToLaunch == null)
            {
                return;
            }
            objectToLaunch.transform.position = _aspiratePoint.position;

            objectToLaunch.GetComponent<Rigidbody>().velocity = Vector3.zero;
            objectToLaunch.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
            objectToLaunch.GetComponent<Rigidbody>().AddForce(_aspiratePoint.forward.normalized * _launchForce, ForceMode.Impulse);
        }
    }

    public void SetAspirate(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            StartAspire();
        }
        else if (ctx.canceled)
        {
            StopAspire();
        }
    }

    private void StartAspire()
    {
        _aspirating = true;
        _suctionPoint.SetCanSuck(true);

    }

    private void StopAspire()
    {
        _aspirating = false;
        _suctionPoint.SetCanSuck(false);

        foreach (GameObject obj in _aspirableObjectsList)
        {
            obj.GetComponent<IAspirable>().StopBeingAspired();
        }
    }

    public void RemoveAspirableObject(GameObject aspirableObject)
    {
        _aspirableObjectsList.Remove(aspirableObject);
    }



}
