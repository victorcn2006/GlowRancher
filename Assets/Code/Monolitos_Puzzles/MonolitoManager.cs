using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonolitoManager : MonoBehaviour
{

    [SerializeField] private AmbienceController.AmbienceStates _ambience;
    [SerializeField] private int _fogSetOnActive;

    private bool _activated = false;

    public void ActivateMonolito()
    {
        Debug.Log("Monolito Activado");

        if (!_activated)
        {
            _activated = true;
            AmbienceController.Instance.SetAmbience(_ambience);
            FogWallsSetsManager.Instance.UpdateFogSet(_fogSetOnActive);
            //agregar animación de cristal y cinematica

        }

    }



}
