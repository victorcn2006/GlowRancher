using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialPuzzle : MonoBehaviour
{

    [SerializeField] private Dialogue _dialogue;

    public void FinishPuzzle()
    {
        StartCoroutine(WaitToAnimation());
        IANarratorManager.Instance.AddNewDialogueToQueue(_dialogue);
    }

    private IEnumerator WaitToAnimation()
    {
        yield return new WaitForSeconds(37f);
    }
}
