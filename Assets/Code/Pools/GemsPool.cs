using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemsPool : MonoBehaviour
{

    public static GemsPool Instance;

    [Header("BASIC GEMS")]
    [SerializeField] private GameObject BlueGemPrefab;
    [SerializeField] private GameObject RedGemPrefab;
    [SerializeField] private GameObject FireGemPrefab;
    [SerializeField] private GameObject ScaredGemPrefab;

    [Header("FUSION GEMS")]
    [SerializeField] private GameObject RedBlueGemPrefab;


    [Header("BASIC GEMS POOLS")]
    public List<GameObject> blueGemsList = new List<GameObject>();
    public List<GameObject> redGemsList = new List<GameObject>();
    public List<GameObject> fireGemsList = new List<GameObject>();
    public List<GameObject> scaredGemsList = new List<GameObject>();

    [Header("FUSION GEMS POOLS")]
    public List<GameObject> redblueGemsList = new List<GameObject>();

    private GameObject currentGemPrefab;


    public enum gemTypes { BLUE_GEM, RED_GEM, FIRE_GEM, SCARED_GEM, REDBLUE_GEM};

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

    private void Start()
    {
        RegisterGem(BlueGemPrefab, "BlueGem");
        RegisterGem(RedGemPrefab, "RedSlimeGem");
        RegisterGem(RedBlueGemPrefab, "RedBlueGem");
        RegisterGem(RedBlueGemPrefab, "FireGem");
        RegisterGem(RedBlueGemPrefab, "ScaredGem");
    }

    private void RegisterGem(GameObject prefab, string defaultName)
    {
        if (prefab == null) return;
        
        string name = defaultName;
        ItemPickUp pickUp = prefab.GetComponent<ItemPickUp>();
        if (pickUp != null && !string.IsNullOrEmpty(pickUp.nombre))
        {
            name = pickUp.nombre;
        }
        
        PoolManager.Instance.AddPool(name, prefab);
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
            case gemTypes.FIRE_GEM:
                currentGemPrefab = FireGemPrefab;
                currentGemList = fireGemsList;
                gemToRetrun = GetFirstAvailableObject(currentGemList);
                fireGemsList = currentGemList;
                return gemToRetrun;
            case gemTypes.SCARED_GEM:
                currentGemPrefab = ScaredGemPrefab;
                currentGemList = scaredGemsList;
                gemToRetrun = GetFirstAvailableObject(currentGemList);
                scaredGemsList = currentGemList;
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
