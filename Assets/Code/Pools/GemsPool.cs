using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemsPool : MonoBehaviour
{

    public static GemsPool Instance;

    [Header("BASIC GEMS")]
    [SerializeField] private GameObject BlueGemPrefab;
    [SerializeField] private GameObject RedGemPrefab;

    [Header("FUSION GEMS")]
    [SerializeField] private GameObject RedBlueGemPrefab;


    [Header("BASIC GEMS POOLS")]
    public List<GameObject> blueGemsList = new List<GameObject>();
    public List<GameObject> redGemsList = new List<GameObject>();

    [Header("FUSION GEMS POOLS")]
    public List<GameObject> redblueGemsList = new List<GameObject>();

    private GameObject currentGemPrefab;


    public enum gemTypes { BLUE_GEM, RED_GEM, REDBLUE_GEM};

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

    public GameObject GetGem(gemTypes gemTypeRequested)
    {
        List<GameObject> currentGemList = new List<GameObject>();
        GameObject gemToRetrun;
        switch (gemTypeRequested)
        {
            case gemTypes.BLUE_GEM:
                currentGemPrefab = BlueGemPrefab;
                currentGemList = blueGemsList;
                gemToRetrun = GetFirstAvailableObject(currentGemList);
                blueGemsList = currentGemList;
                return gemToRetrun;

            case gemTypes.RED_GEM:
                currentGemPrefab = RedGemPrefab;
                currentGemList = redGemsList;
                gemToRetrun = GetFirstAvailableObject(currentGemList);
                redGemsList = currentGemList;
                return gemToRetrun;

            case gemTypes.REDBLUE_GEM:
                currentGemPrefab = RedBlueGemPrefab;
                currentGemList = redblueGemsList;
                gemToRetrun = GetFirstAvailableObject(currentGemList);
                redblueGemsList = currentGemList;
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
