using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialPuzzle : MonoBehaviour
{

    [SerializeField] private Dialogue _dialogue;


    public void FinishPuzzle() => IANarratorManager.Instance.AddNewDialogueToQueue(_dialogue);
}
