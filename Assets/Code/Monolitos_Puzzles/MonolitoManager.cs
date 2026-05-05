using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonolitoManager : MonoBehaviour
{

    [SerializeField] private AmbienceController.AmbienceStates _ambience;
    [SerializeField] private int _fogSetOnActive;
    [SerializeField] private int _puzzleNumber;
    [SerializeField] private bool _monolitoUnlocked;
    [SerializeField] private List<GameObject> _spawnsAsigned;

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

            //agregar animación de cristal y cinematica
            _monolitoUnlocked = true;
            if (this.gameObject.CompareTag("FirstMonolito")) DeathScript.instance.firstMonolitoUnlocked = true;
        }
    }




}
