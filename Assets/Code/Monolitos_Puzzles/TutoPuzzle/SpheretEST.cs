using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpheretEST : MonoBehaviour
{
    private TutorialPuzzle _tutorialPuzzle;

    private void Awake()
    {
        _tutorialPuzzle = GetComponentInParent<TutorialPuzzle>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
           _tutorialPuzzle.ActivateMonolito();
        }
    }

}
