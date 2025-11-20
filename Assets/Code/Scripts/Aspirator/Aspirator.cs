using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Aspirator : MonoBehaviour
{
    public static Aspirator instance;

    [SerializeField] private InputActionReference aspirate;
    [SerializeField] private InputActionReference launchObject;

    [SerializeField] private ObjectsDetector objectsDetector;

    [SerializeField] private SuctionPoint suctionPoint;

    [SerializeField] private Inventory inventory;
    private List<GameObject> aspirableObjectsList = new List<GameObject>();

    private bool aspirating;

    [SerializeField] private float aspirateForce;
    [SerializeField] private float launchForce;
    [SerializeField] private Transform aspiratePoint;

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

        aspirate.action.performed += SetAspirate;
        aspirate.action.canceled += SetAspirate;

        launchObject.action.performed += LaunchObject;

        aspirate.action.Enable();
        aspirating = false;  
    }

    private void Update()
    {

        if (aspirating)
        {
            AspirateObjects();
        }
    }

    private void AspirateObjects()
    {
        aspirableObjectsList = objectsDetector.GetAspirableObjects();

        foreach (GameObject obj in aspirableObjectsList)
        {
            obj.GetComponent<IAspirable>().BeingAspired();

            float distance = Vector3.Distance(obj.transform.position, aspiratePoint.position);
            float forceFactor = Mathf.Clamp01(1f - (distance / 10f)); // 10 = rango máximo de succión
            Vector3 aspirateDirection = (aspiratePoint.position - obj.transform.position).normalized;
            obj.GetComponent<Rigidbody>().AddForce(aspirateDirection * aspirateForce * forceFactor, ForceMode.Force);

        }
    }
    public void LaunchObject(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            //codigo para disparar el objeto 

            GameObject objectToLaunch = PoolManager.Instance.GetFirstAvailableObject(inventory.QuitarUno());
            objectToLaunch.transform.position = aspiratePoint.position;

            if (objectToLaunch.CompareTag("Slime"))
            {
                objectToLaunch.GetComponentInChildren<Rigidbody>().AddForce(aspiratePoint.forward.normalized * launchForce, ForceMode.Impulse);
            }
            else
            {
                objectToLaunch.GetComponent<Rigidbody>().AddForce(aspiratePoint.forward.normalized * launchForce, ForceMode.Impulse);
            }
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
        aspirating = true;
        suctionPoint.SetCanSuck(true);

    }

    private void StopAspire()
    {
        aspirating = false;
        suctionPoint.SetCanSuck(false);

        foreach (GameObject obj in aspirableObjectsList)
        {
            obj.GetComponent<IAspirable>().StopBeingAspired();
        }
    }

    public void RemoveAspirableObject(GameObject aspirableObject)
    {
        aspirableObjectsList.Remove(aspirableObject);
    }



}
