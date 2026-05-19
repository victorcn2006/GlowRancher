using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class MonolitoManager : MonoBehaviour
{

    [SerializeField] private AmbienceController.AmbienceStates _ambience;
    [SerializeField] private int _fogSetOnActive;
    [SerializeField] private int _puzzleNumber;
    [SerializeField] private bool _monolitoUnlocked;
    [SerializeField] private List<GameObject> _spawnsAsigned;

    
    [SerializeField] private GameObject _lightCrystal;
    [SerializeField] private GameObject _crystalPivot;
    [SerializeField] private Transform _lightCrystalFinalPosition;

    public bool IsActivated => _activated;



    private bool _activated = false;

    public void ActivateMonolito()
    {
        if (!_activated)
        {
            _activated = true;
            AmbienceController.Instance.SetAmbience(_ambience);
            FogWallsSetsManager.Instance.UpdateFogSet(_fogSetOnActive);
            BigWallsManager.Instance.PuzzleCompleted(_puzzleNumber);

            GetComponent<MonolitoTimer>().StopTimer();


            foreach (GameObject spawn in _spawnsAsigned)
            {
                  spawn.GetComponent<SlimeSpawner>().SetCorrupted(false);
            }

            StartCoroutine(CrystalAnimation());

            _monolitoUnlocked = true;
        }
    }

    public void OnTimelineFinished()
    {
        if (this.gameObject.CompareTag("FirstMonolito")) {
            Debug.Log("First Monolito timeline finished, unlocking tutorial.");
            DeathScript.instance.firstMonolitoUnlocked = true;
            GetComponentInParent<TutorialPuzzle>().FinishPuzzle();
            if(GameManager.Instance != null) GameManager.Instance.TutorialUnlocked();
        }
    }

    private IEnumerator CrystalAnimation()
    {
        yield return new WaitForSeconds(20f);
        _crystalPivot.transform.DOMove(_crystalPivot.transform.position + new Vector3(0f, 5f, 0f), 5f).SetEase(Ease.OutCubic)
        .OnComplete(() =>
        {
            //_crystalPivot.transform.DOLocalRotate(new Vector3(0f, 180f, 0f), 1f, RotateMode.LocalAxisAdd).SetEase(Ease.Linear).SetLoops(-1, LoopType.Incremental);
            _crystalPivot.GetComponent<FloatingFX>().enabled = true;
        });
    }
}
