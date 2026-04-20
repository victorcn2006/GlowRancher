using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextNarratorTrigger : MonoBehaviour
{
    [SerializeField] private Dialogue dialogueToShow;
    private bool alreadyTriggered = false;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !alreadyTriggered)
        {
            IANarratorManager.Instance.AddNewDialogueToQueue(dialogueToShow);
            alreadyTriggered = true;
        }
    }
}
