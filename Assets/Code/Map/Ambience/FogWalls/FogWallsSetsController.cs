using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FogWallsSetsController : MonoBehaviour
{
    [SerializeField] private int _fogSet;

    [SerializeField] private List<GameObject> _fogWallSets = new List<GameObject>();

    private void Update()
    {
        UpdateFogSet(_fogSet);
    }

    public void UpdateFogSet(int fogSet)
    {
        foreach (GameObject fogWallSet in _fogWallSets)
        {
            if (fogWallSet.gameObject == _fogWallSets[fogSet].gameObject)
            {
                fogWallSet.SetActive(true);
            }
            else fogWallSet.SetActive(false);
        }

    }

}
