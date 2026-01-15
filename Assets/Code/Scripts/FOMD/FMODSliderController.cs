using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using UnityEngine.UI;
using JetBrains.Annotations;

public class FMODSliderController : MonoBehaviour
{

    [Header("FMOD Settings")]
    public string vcaPath;

    private FMOD.Studio.VCA _vcaController;
    private Slider _slider;

    private float _currentVolume;

    private void Start()
    {

        //Referencia VCA
        _vcaController = RuntimeManager.GetVCA(vcaPath);

        //Componente Slider
        _slider = GetComponent<Slider>();

        if (_slider != null)
        {
            //Iniciar valor slider con el volumen actual del VCA
            _vcaController.getVolume(out _currentVolume);
            _slider.value = _currentVolume;

            //Cambios en el Slider
            _slider.onValueChanged.AddListener(SetVolume);
        }
    }

    public void SetVolume(float volume)
    {
        //El slider debe ir de 0 a 1
        _vcaController.setVolume(volume);
    }

}
