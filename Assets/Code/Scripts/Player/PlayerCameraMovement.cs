using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.InputSystem;

public class PlayerCameraMovement : MonoBehaviour
{

    [Header("View")]
    [HideInInspector] public float rotationSense = 10f;
    private float _cameraVerticalAngle;
    
    [SerializeField] private Camera _playerCamera;

    private InputAction _lookAction;
    private Vector2 _rotationInput = Vector2.zero;
    private PlayerInput _playerInput;

    private void Awake()
    {
        if(_playerInput == null) _playerInput = GetComponent<PlayerInput>();
        Cursor.lockState = CursorLockMode.Locked;
        _lookAction = _playerInput.actions["Look"];

    }

    private void OnEnable() {
        _lookAction?.Enable();
    }

    private void OnDisable() {
        _lookAction?.Disable();
    }


    void Update()
    {
        ReadLookInput();
        Look();
    }
    private void ReadLookInput() {
        if (_lookAction != null)
        {
            _rotationInput = _lookAction.ReadValue<Vector2>() * rotationSense * Time.deltaTime;
        }
    }
    private void Look() {
        _cameraVerticalAngle += _rotationInput.y;
        _cameraVerticalAngle = Mathf.Clamp(_cameraVerticalAngle, -70f, 70f);

        // Rota al jugador horizontalmente
        transform.Rotate(Vector3.up * _rotationInput.x);

        // Rota la cámara verticalmente
        _playerCamera.transform.localRotation = Quaternion.Euler(-_cameraVerticalAngle, 0f, 0f);
    }

    public Vector3 GetCameraRotation()
    {
        return _playerCamera.transform.rotation.eulerAngles;
    }

}
