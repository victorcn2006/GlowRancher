using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightInteractionController : MonoBehaviour
{
    [Header("light Reference")]
    public lightRefrences lRef;

    [Header("Conditioners")]
    public bool luz;
    public bool isOn;

    public void ActivateObject() {

        lRef.sun.intensity = 0.5f;
        //lRef.sun.intensity = Mathf.Lerp(intenPrin, intenenFi, ActTime / duration);            //Llum progresiva

        isOn = !isOn;

        foreach(GameObject g in lRef.lights)
        {
            g.SetActive(isOn);
        }

    }
}
