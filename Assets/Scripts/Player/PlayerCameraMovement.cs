using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class PlayerCameraMovement : MonoBehaviour
{

    [Header("View")]
    public float rotationSense = 150f; //la sens
    private float cameraVerticaleAngle;
    Vector3 rotationInput = Vector3.zero; // iniciem la rotacio a 0
    [SerializeField] private Camera playerCamera;

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }
    void Update()
    {
        Look();
    }

    private void Look()
    {
        rotationInput.x = Input.GetAxis("Mouse X") * rotationSense * Time.deltaTime;
        rotationInput.y = Input.GetAxis("Mouse Y") * rotationSense * Time.deltaTime;

        cameraVerticaleAngle = cameraVerticaleAngle + rotationInput.y;
        cameraVerticaleAngle = Mathf.Clamp(cameraVerticaleAngle, -70, 70);

        transform.Rotate(Vector3.up * rotationInput.x);
        playerCamera.transform.localRotation = Quaternion.Euler(-cameraVerticaleAngle, 0f, 0f);
    }

    public Vector3 GetCameraRotation()
    {
        return playerCamera.transform.rotation.eulerAngles;
    }

}