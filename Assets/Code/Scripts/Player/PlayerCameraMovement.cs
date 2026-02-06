using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.InputSystem;

public class PlayerCameraMovement : MonoBehaviour
{

    [Header("View")]
    [SerializeField] private float _rotationSense = 15f;
    [SerializeField] private float _minVerticalAngle = -70f;
    [SerializeField] private float _maxVerticalAngle = 70f;

    [Header("Rerences")]
    [SerializeField] private Camera _playerCamera;
    [SerializeField]private InputActionReference _lookAction;

    private float _cameraVerticalAngle;
    private Vector2 _rotationInput;

    private void Awake()
    {
        if(_playerCamera == null) _playerCamera = GetComponentInChildren<Camera>();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

    }

    private void OnEnable() =>_lookAction.action.Enable();
    private void OnDisable() => _lookAction.action.Disable();
    


    void Update()
    {
        ReadLookInput();
        ApplyRotation();
    }
    private void ReadLookInput() {
        if (_lookAction != null)
        {
            _rotationInput = _lookAction.action.ReadValue<Vector2>();
        }
    }
    private void ApplyRotation()
    {
        float horizontalRotation = _rotationInput.x * _rotationSense * Time.deltaTime;
        transform.Rotate(Vector3.up * horizontalRotation);

        _cameraVerticalAngle -= _rotationInput.y * _rotationSense * Time.deltaTime;
        _cameraVerticalAngle = Mathf.Clamp(_cameraVerticalAngle, _minVerticalAngle, _maxVerticalAngle);

        // Rota la cámara verticalmente
        _playerCamera.transform.localRotation = Quaternion.Euler(_cameraVerticalAngle, 0f, 0f);
    }

    public Quaternion GetCameraRotation()
    {
        return _playerCamera.transform.rotation;
    }

}
