using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialPuzzle : MonoBehaviour
{

    [SerializeField] private Dialogue _dialogue;

    public void FinishPuzzle()
    {
        if (_dialogue != null)
        {
            IANarratorManager.Instance.AddNewDialogueToQueue(_dialogue);
        }
        else
        {
            Debug.LogWarning("TutorialPuzzle: Dialogue is not assigned!");
        }
    }
}
