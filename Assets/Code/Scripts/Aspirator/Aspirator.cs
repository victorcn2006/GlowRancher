using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Aspirator : MonoBehaviour
{
    public static Aspirator instance;

    [Header("Input References")]
    [SerializeField] private InputActionReference _aspirate;
    [SerializeField] private InputActionReference _launchObject;

    [Header("Detection & Suction")]
    [SerializeField] private ObjectsDetector _objectsDetector;
    [SerializeField] private SuctionPoint _suctionPoint;
    [SerializeField] private Transform _aspiratePoint; // Punto desde donde se succiona/lanza
    [SerializeField] private float _aspirateForce = 10f;
    [SerializeField] private float _launchForce = 20f;

    [Header("Inventory Connection")]
    [SerializeField] private Inventory _inventory;

    private List<GameObject> _aspirableObjectsList = new List<GameObject>();
    private bool _aspirating;

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(this.gameObject);
    }

    private void Start()
    {
        // Suscribimos los eventos de Input
        _aspirate.action.performed += SetAspirate;
        _aspirate.action.canceled += SetAspirate;

        // Aquí conectamos el botón de lanzar con el método LanzarObjeto
        _launchObject.action.performed += ctx => LanzarObjeto();

        _aspirate.action.Enable();
        _launchObject.action.Enable();
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
            if (obj == null) continue;

            obj.GetComponent<IAspirable>().BeingAspired();

            float distance = Vector3.Distance(obj.transform.position, _aspiratePoint.position);
            float forceFactor = Mathf.Clamp01(1f - (distance / 10f));
            Vector3 aspirateDirection = (_aspiratePoint.position - obj.transform.position).normalized;

            if (obj.TryGetComponent(out Rigidbody rb))
            {
                rb.AddForce(aspirateDirection * _aspirateForce * forceFactor, ForceMode.Force);
            }
        }
    }

    // ESTE ES EL MÉTODO QUE HACÍA FALTA CONFIGURAR BIEN
    public void LanzarObjeto()
    {
        string nombreDelItem = _inventory.QuitarUno();

        if (string.IsNullOrEmpty(nombreDelItem))
        {
            Debug.Log("Inventario vacío o nombre no encontrado");
            return;
        }

        GameObject obj = PoolManager.Instance.GetFirstAvailableObject(nombreDelItem);

        if (obj != null)
        {
            // Re-configurar el objeto para que sea "aspirable" de nuevo al caer
            if (obj.TryGetComponent(out ItemPickUp script))
            {
                script.nombre = nombreDelItem;
            }

            obj.transform.position = _aspiratePoint.position;
            obj.transform.rotation = _aspiratePoint.rotation;
            obj.SetActive(true);

            if (obj.TryGetComponent(out Rigidbody rb))
            {
                rb.velocity = Vector3.zero;
                rb.AddForce(_aspiratePoint.forward * _launchForce, ForceMode.Impulse);
            }
        }
    }

    public void SetAspirate(InputAction.CallbackContext ctx)
    {
        if (ctx.performed) StartAspire();
        else if (ctx.canceled) StopAspire();
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
            if (obj != null)
                obj.GetComponent<IAspirable>().StopBeingAspired();
        }
    }

    public void RemoveAspirableObject(GameObject aspirableObject)
    {
        _aspirableObjectsList.Remove(aspirableObject);
    }
}
