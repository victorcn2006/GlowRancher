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

    [Header("Zonas a Purificar")]
    public ZonaSonora zonaPlaya;
    public ZonaSonora zonaGranja;
    public ZonaSonora zonaForest;
    public ZonaSonora zonaLake;
    public ZonaSonora zonaEntranceMountain;
    public ZonaSonora zonaEntranceMagicForest;
    public ZonaSonora zonaThirdPuzzle;
    public ZonaSonora zonaEnterToMagicForest;
    public ZonaSonora zonaMushroom;
    public ZonaSonora zonaMushroomSmall;
    public ZonaSonora zonaForestMushroom;
    public ZonaSonora zonaPuzzlefFort;
    public ZonaSonora zonaForestSmall;
    public ZonaSonora zonaMountain;
    public ZonaSonora zonaLava  ;

    private bool _activated = false;

    public void ActivateMonolito()
    {

        if (!_activated)
        {
            _activated = true;
            AmbienceController.Instance.SetAmbience(_ambience);
            FogWallsSetsManager.Instance.UpdateFogSet(_fogSetOnActive);
            BigWallsManager.Instance.PuzzleCompleted(_puzzleNumber);

            PurificarZonas();

            foreach (GameObject spawn in _spawnsAsigned)
            {
                  spawn.GetComponent<SlimeSpawner>().SetCorrupted(false);
            }

            CrystalAnimation();

            _monolitoUnlocked = true;
            if (this.gameObject.CompareTag("FirstMonolito")) {
                DeathScript.instance.firstMonolitoUnlocked = true;
                if(GameManager.Instance != null) GameManager.Instance.TutorialUnlocked();
            } 
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


    public void PurificarZonas()
    {
        // Solo estos dos cambiarán a estado 0 (Purificado)
        zonaPlaya.CambiarEstado(0f);
        zonaGranja.CambiarEstado(0f);
        zonaForest.CambiarEstado(0f);
        zonaLake.CambiarEstado(0f);
        zonaThirdPuzzle.CambiarEstado(0f);
        zonaEntranceMagicForest.CambiarEstado(0f);
        zonaEntranceMountain.CambiarEstado(0f);
        zonaEnterToMagicForest.CambiarEstado(0f);
        zonaMushroom.CambiarEstado(0f);
        zonaMushroomSmall.CambiarEstado(0f);
        zonaForestMushroom.CambiarEstado(0f);
        zonaPuzzlefFort.CambiarEstado(0f);
        zonaForestSmall.CambiarEstado(0f);
        zonaMountain.CambiarEstado(0f);
        zonaLava.CambiarEstado(0f);

    }

}
