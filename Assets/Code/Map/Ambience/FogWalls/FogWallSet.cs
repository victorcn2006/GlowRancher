using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class FogWallSet : MonoBehaviour
{

    [Header("No vision Walls")]
    [SerializeField] private List<GameObject> _noVisionWalls = new List<GameObject>();

    private float _transitionTime;
    private Ease _easeType;

    private void Start()
    {
        _transitionTime = AmbienceController.Instance.GetTransitionBetweenAmbienceTime;
        _easeType = AmbienceController.Instance.GetEaseTransitionType;
    }

    public void OnFogEntry()
    {
        foreach (var wall in _noVisionWalls)
        {
            Renderer wallRenderer = wall.GetComponent<Renderer>();
            wallRenderer.material.DOFade(0f, _transitionTime).SetEase(_easeType);

            AmbienceController.Instance.SetAmbience(AmbienceController.AmbienceStates.CORRUPTED);
        }
    }

    public void OnFogExit()
    {
        foreach (var wall in _noVisionWalls)
        {

            Renderer wallRenderer = wall.GetComponent<Renderer>();
            wallRenderer.material.DOFade(1f, _transitionTime).SetEase(_easeType);

            AmbienceController.Instance.SetAmbience(AmbienceController.AmbienceStates.ALIVE);
        }
    }


}
