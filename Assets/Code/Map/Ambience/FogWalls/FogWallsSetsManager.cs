using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FogWallsSetsManager : MonoBehaviour
{
    public static FogWallsSetsManager Instance;

    [SerializeField] private int _fogSet;

    [SerializeField] private List<GameObject> _fogWallSets = new List<GameObject>();
    private void Awake()
    {

        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
            return;
        }

    }
    private void Update()
    {
        //UpdateFogSet(_fogSet); //descomentar para debugear
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
