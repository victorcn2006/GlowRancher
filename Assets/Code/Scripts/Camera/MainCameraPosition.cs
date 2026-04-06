using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCameraPosition : MonoBehaviour
{
    private Vector3 _mainCameraPosition;

    private void Update()
    {
        _mainCameraPosition = this.transform.position;
    }

    public Vector3 GetCameraPosition() { return _mainCameraPosition; }  
}
