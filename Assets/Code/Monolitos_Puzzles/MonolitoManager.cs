using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class MonolitoManager : MonoBehaviour
{

    [SerializeField] private AmbienceController.AmbienceStates _ambience;
    [SerializeField] private int _fogSetOnActive;
    [SerializeField] private int _puzzleNumber;
    [SerializeField] private bool _monolitoUnlocked;
    [SerializeField] private List<GameObject> _spawnsAsigned;

    [SerializeField] private GameObject _lightCrystal;
    [SerializeField] private Transform _lightCrystalFinalPosition;

    private bool _activated = false;

    public void ActivateMonolito()
    {
        Debug.Log("Monolito Activado");

        if (!_activated)
        {
            _activated = true;
            AmbienceController.Instance.SetAmbience(_ambience);
            FogWallsSetsManager.Instance.UpdateFogSet(_fogSetOnActive);
            BigWallsManager.Instance.PuzzleCompleted(_puzzleNumber);

            foreach (GameObject spawn in _spawnsAsigned)
            {
                spawn.GetComponent<SlimeSpawner>().SetCorrupted(false);
            }

            CrystalAnimation();

            _monolitoUnlocked = true;
            if (this.gameObject.CompareTag("FirstMonolito")) DeathScript.instance.firstMonolitoUnlocked = true;
        }
    }

    private void CrystalAnimation()
    {
        _lightCrystal.transform.DOMove(_lightCrystalFinalPosition.position, 10f).SetEase(Ease.OutCubic);

        _lightCrystal.transform.DOLocalRotate(new Vector3(0f, 0f, 2160f), 10f, RotateMode.LocalAxisAdd).SetEase(Ease.OutCubic)
            .OnComplete(() =>
            {
                _lightCrystal.transform.DOLocalRotate(new Vector3(0f, 0f, 180f), 1f, RotateMode.LocalAxisAdd).SetEase(Ease.InSine)
                    .OnComplete(() =>
                    {
                        _lightCrystal.transform.DOLocalRotate(new Vector3(0f, 0f, 180f), 1f, RotateMode.LocalAxisAdd).SetEase(Ease.Linear).SetLoops(-1, LoopType.Incremental);
                        _lightCrystal.GetComponent<FloatingFX>().enabled = true;
                    });
            });
    }


    

}
