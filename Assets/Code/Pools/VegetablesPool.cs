using System.Collections.Generic;
using UnityEngine;

public class VegetablesPool : MonoBehaviour
{
    public static VegetablesPool Instance;

    [Header("VEGETABLES PREFABS")]
    [SerializeField] private GameObject _carrot;
    [SerializeField] private GameObject _eggPlant;
    [SerializeField] private GameObject _tomato;
    [SerializeField] private GameObject _pumpkin;

    [Header("SEEDS PREFABS")]
    [SerializeField] private GameObject _carrotSeed;
    [SerializeField] private GameObject _eggPlantSeed;
    [SerializeField] private GameObject _tomatoSeed;
    [SerializeField] private GameObject _pumpkinSeed;

    [Header("VEGETABLES POOLS")]
    public List<GameObject> carrotList = new List<GameObject>();
    public List<GameObject> eggPlantList = new List<GameObject>();
    public List<GameObject> tomatoList = new List<GameObject>();
    public List<GameObject> pumpkinList = new List<GameObject>();

    [Header("SEEDS POOLS")]
    public List<GameObject> carrotSeedList = new List<GameObject>();
    public List<GameObject> eggPlantSeedList = new List<GameObject>();
    public List<GameObject> tomatoSeedList = new List<GameObject>();
    public List<GameObject> pumpkinSeedList = new List<GameObject>();

    private GameObject _currentObjectPrefab;


    public enum vegetablesType { CARROT, EGGPLANT, PUMPKIN, TOMATO }
    public enum seedsType { CARROT_SEED, EGGPLANT_SEED, PUMPKIN_SEED, TOMATO_SEED }

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
        // Register Vegetables
        RegisterItem(_carrot, "Carrot");
        RegisterItem(_eggPlant, "EggPlant");
        RegisterItem(_tomato, "Tomato");
        RegisterItem(_pumpkin, "Pumpkin");

        // Register Seeds
        RegisterItem(_carrotSeed, "CarrotSeed");
        RegisterItem(_eggPlantSeed, "EggPlantSeed");
        RegisterItem(_tomatoSeed, "TomatoSeed");
        RegisterItem(_pumpkinSeed, "PumpkinSeed");
    }

    private void RegisterItem(GameObject prefab, string defaultName)
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

    public GameObject GetVegetable(vegetablesType vegetable)
    {
        List<GameObject> currentPool = null;

        switch (vegetable)
        {
            case vegetablesType.CARROT:
                _currentObjectPrefab = _carrot;
                currentPool = carrotList;
                break;
            case vegetablesType.EGGPLANT:
                _currentObjectPrefab = _eggPlant;
                currentPool = eggPlantList;
                break;
            case vegetablesType.PUMPKIN:
                _currentObjectPrefab = _pumpkin;
                currentPool = pumpkinList;
                break;
            case vegetablesType.TOMATO:
                _currentObjectPrefab = _tomato;
                currentPool = tomatoList;
                break;
        }

        return GetFromPool(currentPool);
    }

    public GameObject GetSeed(seedsType seed)
    {
        List<GameObject> currentPool = null;

        switch (seed)
        {
            case seedsType.CARROT_SEED:
                _currentObjectPrefab = _carrotSeed;
                currentPool = carrotSeedList;
                break;
            case seedsType.EGGPLANT_SEED:
                _currentObjectPrefab = _eggPlantSeed;
                currentPool = eggPlantSeedList;
                break;
            case seedsType.PUMPKIN_SEED:
                _currentObjectPrefab = _pumpkinSeed;
                currentPool = pumpkinSeedList;
                break;
            case seedsType.TOMATO_SEED:
                _currentObjectPrefab = _tomatoSeed;
                currentPool = tomatoSeedList;
                break;
        }

        return GetFromPool(currentPool);
    }
    /*
    private GameObject GetFromPool(List<GameObject> pool)
    {

        Debug.Log($"GetFromPool cridat. Pool: {pool?.Count} objectes");

        if (pool == null) return null;

        foreach (GameObject obj in pool)
        {
            if (obj != null && !obj.activeInHierarchy)
            {
                obj.SetActive(true);
                return obj;
            }
        }

        if (_currentObjectPrefab != null)
        {
            GameObject newObj = Instantiate(_currentObjectPrefab);
            pool.Add(newObj);
            newObj.SetActive(true);
            return newObj;
        }

        return null;
    }
    */

    private GameObject GetFromPool(List<GameObject> pool)
    {
        if (pool == null) return null;

        foreach (GameObject obj in pool)
        {
            if (obj != null && !obj.activeInHierarchy)
            {
                obj.SetActive(true);
                return obj;
            }
        }

        // Nou objecte
        if (_currentObjectPrefab != null)
        {
            GameObject newObj = Instantiate(_currentObjectPrefab);
            newObj.SetActive(true);
            pool.Add(newObj);
            return newObj;
        }

        return null;
    }
}
