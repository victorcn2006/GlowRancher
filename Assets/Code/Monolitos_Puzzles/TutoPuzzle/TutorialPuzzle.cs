using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialPuzzle : MonoBehaviour
{
    private MonolitoManager _monolitoManager;

    private void Awake()
    {
        _monolitoManager = GetComponentInChildren<MonolitoManager>();
    }

    public void ActivateMonolito() => _monolitoManager.ActivateMonolito();
}
