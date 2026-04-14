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
    [SerializeField] private GameObject IceGemPrefab;
    [SerializeField] private GameObject MushroomGemPrefab;
    [SerializeField] private GameObject CatGemPrefab;
    [SerializeField] private GameObject RockGemPrefab;

    [Header("FUSION GEMS")]
    [SerializeField] private GameObject RedBlueGemPrefab;
    [SerializeField] private GameObject FrozenRockGemPrefab;
    [SerializeField] private GameObject BurningMushGemPrefab;
    [SerializeField] private GameObject ScaredCatGemPrefab;
    [SerializeField] private GameObject obsidianGemPrefab;


    [Header("BASIC GEMS POOLS")]
    public List<GameObject> blueGemsList = new List<GameObject>();
    public List<GameObject> redGemsList = new List<GameObject>();
    public List<GameObject> fireGemsList = new List<GameObject>();
    public List<GameObject> scaredGemsList = new List<GameObject>();
    public List<GameObject> iceGemsList = new List<GameObject>();
    public List<GameObject> mushroomGemsList = new List<GameObject>();
    public List<GameObject> catGemsList = new List<GameObject>();
    public List<GameObject> rockGemsList = new List<GameObject>();


    [Header("FUSION GEMS POOLS")]
    public List<GameObject> redblueGemsList = new List<GameObject>();
    public List<GameObject> frozenRockGemsList = new List<GameObject>();
    public List<GameObject> burningMushGemsList = new List<GameObject>();
    public List<GameObject> scaredCatGemsList = new List<GameObject>();
    public List<GameObject> obsidianGemsList = new List<GameObject>();

    private GameObject currentGemPrefab;


    public enum gemTypes { BLUE_GEM, RED_GEM, FIRE_GEM, SCARED_GEM, ICE_GEM, MUSHROOM_GEM, CAT_GEM, ROCK_GEM, REDBLUE_GEM, FROZENROCK_GEM, BURNINGMUSH_GEM, SCAREDCAT_GEM, OBSIDIAN_GEM};

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
        RegisterGem(FireGemPrefab, "FireGem");
        RegisterGem(ScaredGemPrefab, "ScaredGem");
        RegisterGem(IceGemPrefab, "IceGem");
        RegisterGem(MushroomGemPrefab, "MushroomGem");
        RegisterGem(CatGemPrefab, "CatSlimeStone");
        RegisterGem(RockGemPrefab, "RockGem");
        RegisterGem(FrozenRockGemPrefab, "FrozenRockGem");
        RegisterGem(BurningMushGemPrefab, "BurningMushGem");
        RegisterGem(ScaredCatGemPrefab, "ScaredCatGem");
        RegisterGem(obsidianGemPrefab, "ObsidianGem");
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
        GameObject gemToReturn;
        switch (gemTypeRequested)
        {
            case gemTypes.BLUE_GEM:
                currentGemPrefab = BlueGemPrefab;
                currentGemList = blueGemsList;
                gemToReturn = GetFirstAvailableObject(currentGemList);
                blueGemsList = currentGemList;
                return gemToReturn;

            case gemTypes.RED_GEM:
                currentGemPrefab = RedGemPrefab;
                currentGemList = redGemsList;
                gemToReturn = GetFirstAvailableObject(currentGemList);
                redGemsList = currentGemList;
                return gemToReturn;

            case gemTypes.REDBLUE_GEM:
                currentGemPrefab = RedBlueGemPrefab;
                currentGemList = redblueGemsList;
                gemToReturn = GetFirstAvailableObject(currentGemList);
                redblueGemsList = currentGemList;
                return gemToReturn;

            case gemTypes.FIRE_GEM:
                currentGemPrefab = FireGemPrefab;
                currentGemList = fireGemsList;
                gemToReturn = GetFirstAvailableObject(currentGemList);
                fireGemsList = currentGemList;
                return gemToReturn;

            case gemTypes.SCARED_GEM:
                currentGemPrefab = ScaredGemPrefab;
                currentGemList = scaredGemsList;
                gemToReturn = GetFirstAvailableObject(currentGemList);
                scaredGemsList = currentGemList;
                return gemToReturn;

            case gemTypes.ICE_GEM:
                currentGemPrefab = IceGemPrefab;
                currentGemList = iceGemsList;
                gemToReturn = GetFirstAvailableObject(currentGemList);
                iceGemsList = currentGemList;
                return gemToReturn;

            case gemTypes.MUSHROOM_GEM:
                currentGemPrefab = MushroomGemPrefab;
                currentGemList = mushroomGemsList;
                gemToReturn = GetFirstAvailableObject(currentGemList);
                mushroomGemsList = currentGemList;
                return gemToReturn;

            case gemTypes.CAT_GEM:
                currentGemPrefab = CatGemPrefab;
                currentGemList = catGemsList;
                gemToReturn = GetFirstAvailableObject(currentGemList);
                catGemsList = currentGemList;
                return gemToReturn;

            case gemTypes.ROCK_GEM:
                currentGemPrefab = RockGemPrefab;
                currentGemList = rockGemsList;
                gemToReturn = GetFirstAvailableObject(currentGemList);
                rockGemsList = currentGemList;
                return gemToReturn;
            case gemTypes.FROZENROCK_GEM:
                currentGemPrefab = FrozenRockGemPrefab;
                currentGemList = frozenRockGemsList;
                gemToReturn = GetFirstAvailableObject(currentGemList);
                frozenRockGemsList = currentGemList;
                return gemToReturn;
            case gemTypes.BURNINGMUSH_GEM:
                currentGemPrefab = BurningMushGemPrefab;
                currentGemList = burningMushGemsList;
                gemToReturn = GetFirstAvailableObject(currentGemList);
                burningMushGemsList = currentGemList;
                return gemToReturn;
            case gemTypes.SCAREDCAT_GEM:
                currentGemPrefab = ScaredCatGemPrefab;
                currentGemList = scaredCatGemsList;
                gemToReturn = GetFirstAvailableObject(currentGemList);
                scaredCatGemsList = currentGemList;
                return gemToReturn;
            case gemTypes.OBSIDIAN_GEM:
                currentGemPrefab = obsidianGemPrefab;
                currentGemList = obsidianGemsList;
                gemToReturn = GetFirstAvailableObject(currentGemList);
                obsidianGemsList = currentGemList;
                return gemToReturn;
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
