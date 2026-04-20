using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;

public class EmissionController : MonoBehaviour
{
    private Material _material;

    private void Start()
    {
        _material = GetComponent<Renderer>().material;
        _material.EnableKeyword("_EMISSION");
    }

    public void ActivateEmmision(bool activateEmission)
    {
        Color targetColor = activateEmission ? Color.white * 2f : Color.black;


        Tween emissionTween = _material.DOColor(targetColor, "_EmissionColor", 1f).SetEase(Ease.OutCubic);

        if (activateEmission) _material.SetColor("_EmissionColor", Color.white * 2f);
        else _material.SetColor("_EmissionColor", Color.black);
    }

}
