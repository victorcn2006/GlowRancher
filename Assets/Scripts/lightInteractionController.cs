using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lightInteractionController : MonoBehaviour
{
    [Header("light Reference")]
    public lightRefrences lRef;

    [Header("Conditioners")]
    public bool luz;
    public bool isOn;


    public void ActivateObject() {

        lRef.sun.intensity = 0.7f;

        isOn = !isOn;

        foreach(GameObject g in lRef.lights)
        {
            g.SetActive(isOn);
        }

    }
}
