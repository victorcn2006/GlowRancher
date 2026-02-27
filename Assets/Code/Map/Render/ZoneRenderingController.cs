using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoneRenderingController : MonoBehaviour
{

    public static ZoneRenderingController Instance { get; private set; }
    [SerializeField] private List<GameObject> zones = new List<GameObject>();

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
    public void SetActiveZones(List<GameObject> zonesToActive)
    {
        foreach(GameObject zone in zones)
        {
            if(zonesToActive.Contains(zone)) zone.SetActive(true);
            else zone.SetActive(false);
        }
    }
}
