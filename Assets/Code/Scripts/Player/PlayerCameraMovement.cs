using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.InputSystem;

public class PlayerCameraMovement : MonoBehaviour
{

    [Header("View")]
    public float rotationSense = 150f;
    private float cameraVerticalAngle;
    
    [SerializeField] private Camera playerCamera;

    private InputAction lookAction;
    Vector2 rotationInput = Vector2.zero;
    private PlayerInput playerInput;

    private void Awake()
    {
        if(playerInput == null) playerInput = GetComponent<PlayerInput>();
        Cursor.lockState = CursorLockMode.Locked;
        lookAction = playerInput.actions["Look"];

    }

    private void OnEnable() {
        lookAction?.Enable();
    }

    private void OnDisable() {
        lookAction?.Disable();
    }


    void Update()
    {
        ReadLookInput();
        Look();
    }
    private void ReadLookInput() {
        if (lookAction != null)
        {
            rotationInput = lookAction.ReadValue<Vector2>() * rotationSense * Time.deltaTime;
        }
    }
    private void Look() {
        cameraVerticalAngle += rotationInput.y;
        cameraVerticalAngle = Mathf.Clamp(cameraVerticalAngle, -70f, 70f);

        // Rota al jugador horizontalmente
        transform.Rotate(Vector3.up * rotationInput.x);

        // Rota la cámara verticalmente
        playerCamera.transform.localRotation = Quaternion.Euler(-cameraVerticalAngle, 0f, 0f);
    }

    public Vector3 GetCameraRotation()
    {
        return playerCamera.transform.rotation.eulerAngles;
    }

}
