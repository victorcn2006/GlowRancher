using System.Collections.Generic;
using UnityEngine;

public class MarketManager : MonoBehaviour
{
    public static MarketManager Instance;
    [Header("variables")]
    private int _multi = 1;

    // Multiplicadors per a cada tipus de gemma
    private Dictionary<GemsPool.gemTypes, float> priceMultipliers = new Dictionary<GemsPool.gemTypes, float>();

    private void Awake()
    {
        Instance = this;
        _multi = 1; //ens asegurem que comency en 1
        InitMulti();
    }

    //inisialitzador de variables(multiplicador)
    private void InitMulti() {
        priceMultipliers[GemsPool.gemTypes.BLUE_GEM] = _multi;
        priceMultipliers[GemsPool.gemTypes.RED_GEM] = _multi;
        priceMultipliers[GemsPool.gemTypes.REDBLUE_GEM] = _multi;
    }

    // Aquesta funció calcula el preu actual basat en la pool de gemas
    public int GetCurrentPrice(GemsPool.gemTypes type, int baseValue)
    {
        float multiplier = priceMultipliers[type];
        return Mathf.RoundToInt(baseValue * multiplier);
    }

    // Quan una gema sigui venguda els preus seran alterats
    public void UpdateMarketPrices()
    {
        UpdateTypeMultiplier(GemsPool.gemTypes.BLUE_GEM, GemsPool.Instance.blueGemsList);
        UpdateTypeMultiplier(GemsPool.gemTypes.RED_GEM, GemsPool.Instance.redGemsList);
        UpdateTypeMultiplier(GemsPool.gemTypes.REDBLUE_GEM, GemsPool.Instance.redblueGemsList);
    }

    private void UpdateTypeMultiplier(GemsPool.gemTypes type, List<GameObject> list)
    {
        int activeCount = 0;
        foreach (var g in list) if (g.activeInHierarchy) activeCount++;

        // Lògica simple: si hi ha més de 10, el preu baixa un 5% per cada gemma extra
        // Si hi ha menys de 5, el preu puja.
        if (activeCount > 10)
            priceMultipliers[type] = 0.7f; // Abundància: preu baix
        else if (activeCount < 3)
            priceMultipliers[type] = 1.5f; // Escassetat: preu alt
        else
            priceMultipliers[type] = 1.0f; // Normal
    }
}
