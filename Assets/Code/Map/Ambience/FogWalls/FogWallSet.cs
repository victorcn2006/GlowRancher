using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class FogWallSet : MonoBehaviour
{


    private float _transitionTime;
    private Ease _easeType;

    private void Start()
    {
        _transitionTime = AmbienceController.Instance.GetTransitionBetweenAmbienceTime;
        _easeType = AmbienceController.Instance.GetEaseTransitionType;
    }

    public void OnFogPass(AmbienceController.AmbienceStates ambience)
    {
            AmbienceController.Instance.SetAmbience(ambience);
    }


}
