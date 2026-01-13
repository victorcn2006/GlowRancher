using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prueba : MonoBehaviour
{
    [SerializeField] GameObject gameObject;
    private GameObject obj;
    private void Start()
    {
        obj = gameObject;   
    }
}
