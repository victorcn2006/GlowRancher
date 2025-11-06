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

    /*
    [Header("Var")]                                         º                                  //variables de lallum progresiva
    private float intenPrin = 0.0f;
    private float intenenFi = 0.7f;
    private float ActTime = 0.0f;
    private float duration = 10f;
    */
    
    /*
    public void Update()
    {
        ActTime += Time.deltaTime;                                                              //Temps desde que s'inicia, component nessesari per la llum progresiva
    }
    */

    public void ActivateObject() {

        lRef.sun.intensity = 0.2f;
        //lRef.sun.intensity = Mathf.Lerp(intenPrin, intenenFi, ActTime / duration);            //Llum progresiva

        isOn = !isOn;

        foreach(GameObject g in lRef.lights)
        {
            g.SetActive(isOn);
        }

    }
}
