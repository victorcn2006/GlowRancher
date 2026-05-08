using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePuzzle : MonoBehaviour
{
    [SerializeField] private ParkourPuzzle _parkourPuzzle;

    private List<bool> _platesStates = new List<bool>(3) { false, false, false };

    private void Awake()
    {
        //_monolitoManager = GetComponentInChildren<MonolitoManager>();
    }

    public void SetActivePlate(int plateIndex, bool state)
    {
        _platesStates[plateIndex] = state;

        bool allPlatesActivated = true;
        foreach (var plate in _platesStates)
        {
            if (!plate)
            {
                allPlatesActivated = false;
            }
        }
        if (allPlatesActivated)
        {
            GetComponentInChildren<KeyRock>().SpawnKey();
            _parkourPuzzle.UnlockParkour();
        }
        
        //if (allPlatesActivated) _monolitoManager.ActivateMonolito();

    }
}
