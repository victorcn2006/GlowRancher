using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemsPool : MonoBehaviour
{

    public static GemsPool Instance;

    [SerializeField] private GameObject basicGemPrefab;
    [SerializeField] private GameObject grassGemPrefab;
    [SerializeField] private GameObject rockGemPrefab;
    private GameObject currentGemPrefab;

    public List<GameObject> basicGemsList = new List<GameObject>();
    public List<GameObject> grassGemsList = new List<GameObject>();
    public List<GameObject> rockGemsList = new List<GameObject>();

    public enum gemType {BASIC_GEM, GRASS_GEM, ROCK_GEM};

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public GameObject GetGem(gemType gemTypeRequested)
    {
        List<GameObject> currentGemList = new List<GameObject>();
        GameObject gemToRetrun;
        switch (gemTypeRequested)
        {
            case gemType.BASIC_GEM:
                currentGemPrefab = basicGemPrefab;
                currentGemList = basicGemsList;
                gemToRetrun = GetFirstAvailableObject(currentGemList);
                basicGemsList = currentGemList;
                return gemToRetrun;

            case gemType.GRASS_GEM:
                currentGemPrefab = grassGemPrefab;
                currentGemList = grassGemsList;
                gemToRetrun = GetFirstAvailableObject(currentGemList);
                grassGemsList = currentGemList;
                return gemToRetrun;

            case gemType.ROCK_GEM:
                currentGemPrefab = rockGemPrefab;
                currentGemList = rockGemsList;
                gemToRetrun = GetFirstAvailableObject(currentGemList);
                rockGemsList = currentGemList;
                return gemToRetrun;
        }
        return null;
    }

    private GameObject GetFirstAvailableObject(List<GameObject> gemList)
    {
        
        foreach (GameObject obj in gemList)
        {
            if (!obj.activeInHierarchy)
            {
                return obj;
            }
        }

        GameObject newSlime = Instantiate(currentGemPrefab);
        gemList.Add(newSlime);

        return newSlime;
    }

}
