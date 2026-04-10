using System.Collections;
using System.Collections.Generic;
using FMODUnity;
using FMOD.Studio;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Aspirator : MonoBehaviour
{
    public static Aspirator instance;

    [Header("INPUTS")]
    [SerializeField] private InputActionReference _aspirate;
    [SerializeField] private InputActionReference _launchObject;

    [Header("REFERENCES")]
    [SerializeField] private ObjectsDetector _objectsDetector;
    [SerializeField] private SuctionPoint _suctionPoint;
    [SerializeField] private Inventory _inventory;
    [SerializeField] private Transform _aspiratePoint;

    [Header("SETTINGS")]
    [SerializeField] private float _aspirateForce = 10f;
    [SerializeField] private float _launchForce = 20f;
    [SerializeField] private float _maxDistance = 10f;

    [Header("AUDIO (FMOD)")]
    [SerializeField] private EventReference _vacuumShotSoundSlime;
    [SerializeField] private EventReference _vacuumSuckSoundSlime;

    private List<GameObject> _aspirableObjectsList = new List<GameObject>();
    private bool _aspirating;

    private EventInstance _suckEventInstance;
    private EventInstance _shotEventInstance;

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(this.gameObject);
    }

    private void Start()
    {
        // 1. Suscribimos el disparo: Performed para empezar, Canceled para parar
        _aspirate.action.performed += SetAspirate;
        _aspirate.action.canceled += SetAspirate;

        _launchObject.action.performed += LaunchObject;
        _launchObject.action.canceled += StopShotSound; // NUEVO: Detener al soltar click

        _aspirate.action.Enable();
        _launchObject.action.Enable();

        if (!_vacuumSuckSoundSlime.IsNull)
            _suckEventInstance = RuntimeManager.CreateInstance(_vacuumSuckSoundSlime);

        if (!_vacuumShotSoundSlime.IsNull)
            _shotEventInstance = RuntimeManager.CreateInstance(_vacuumShotSoundSlime);
    }

    private void Update()
    {
        if (_aspirating)
        {
            AspirateObjects();
            if (_suckEventInstance.isValid())
                _suckEventInstance.set3DAttributes(RuntimeUtils.To3DAttributes(gameObject));
        }
    }

    private void AspirateObjects()
    {
        _aspirableObjectsList = _objectsDetector.GetAspirableObjects();
        foreach (GameObject obj in _aspirableObjectsList)
        {
            if (obj.TryGetComponent<IAspirable>(out var aspirable)) aspirable.BeingAspired();
            if (obj.TryGetComponent<Rigidbody>(out var rb))
            {
                float distance = Vector3.Distance(obj.transform.position, _aspiratePoint.position);
                float forceFactor = Mathf.Clamp01(1f - (distance / _maxDistance));
                Vector3 aspirateDirection = (_aspiratePoint.position - obj.transform.position).normalized;
                rb.AddForce(aspirateDirection * _aspirateForce * forceFactor, ForceMode.Force);
            }
        }
    }

    public void LaunchObject(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            string itemID = _inventory.QuitarUno();
            if (string.IsNullOrEmpty(itemID)) return;

            GameObject objectToLaunch = PoolManager.Instance.GetFirstAvailableObject(itemID);

            if (objectToLaunch != null)
            {
                PlayShotSound(); // Inicia el sonido

                _objectsDetector.RemoveTargetFromAspirableObjectList(objectToLaunch);
                objectToLaunch.transform.position = _aspiratePoint.position;

                if (objectToLaunch.TryGetComponent<Rigidbody>(out var rb))
                {
                    rb.velocity = Vector3.zero;
                    rb.angularVelocity = Vector3.zero;
                    rb.AddForce(_aspiratePoint.forward * _launchForce, ForceMode.Impulse);
                }
            }
        }
    }

    private void PlayShotSound()
    {
        if (_shotEventInstance.isValid())
        {
            _shotEventInstance.set3DAttributes(RuntimeUtils.To3DAttributes(gameObject));
            _shotEventInstance.start();
        }
    }

    // NUEVO: Método para detener el sonido de disparo al soltar el ratón
    private void StopShotSound(InputAction.CallbackContext ctx)
    {
        if (_shotEventInstance.isValid())
        {
            // Usamos ALLOWFADEOUT para que no sea un corte seco y feo
            _shotEventInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
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
        if (_suckEventInstance.isValid())
        {
            _suckEventInstance.getPlaybackState(out PLAYBACK_STATE state);
            if (state != PLAYBACK_STATE.PLAYING) _suckEventInstance.start();
        }
    }

    private void StopAspire()
    {
        _aspirating = false;
        _suctionPoint.SetCanSuck(false);
        if (_suckEventInstance.isValid()) _suckEventInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);

        foreach (GameObject obj in _aspirableObjectsList)
        {
            if (obj.TryGetComponent<IAspirable>(out var aspirable)) aspirable.StopBeingAspired();
        }
    }

    private void OnDestroy()
    {
        if (_suckEventInstance.isValid())
        {
            _suckEventInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
            _suckEventInstance.release();
        }
        if (_shotEventInstance.isValid())
        {
            _shotEventInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
            _shotEventInstance.release();
        }

        _aspirate.action.performed -= SetAspirate;
        _aspirate.action.canceled -= SetAspirate;
        _launchObject.action.performed -= LaunchObject;
        _launchObject.action.canceled -= StopShotSound; // Limpieza importante
    }
    public void RemoveAspirableObject(GameObject aspirableObject)
    {
        // 1. Antes de quitarlo, le decimos que deje de ser aspirado 
        // (Esto detiene sus partículas o efectos locales)
        if (aspirableObject.TryGetComponent<IAspirable>(out var aspirable))
        {
            aspirable.StopBeingAspired();
        }

        // 2. Lo eliminamos de la lista
        if (_aspirableObjectsList.Contains(aspirableObject))
        {
            _aspirableObjectsList.Remove(aspirableObject);
        }
    }
}
