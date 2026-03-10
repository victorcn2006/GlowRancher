using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestAmbienceTransitions : MonoBehaviour
{

    public int a;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (a == 0) AmbienceController.Instance.SetAmbience(AmbienceController.AmbienceStates.CORRUPTED);
            else AmbienceController.Instance.SetAmbience(AmbienceController.AmbienceStates.ALIVE);
        }
    }
}
