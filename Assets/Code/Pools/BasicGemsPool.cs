using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicGemsPool : MonoBehaviour
{

    public static BasicGemsPool Instance;

    [SerializeField] private GameObject gemPrefab;

    public List<GameObject> gems = new List<GameObject>();


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

    public GameObject GetFirstAvailableObject()
    {

        foreach (GameObject obj in gems)
        {
            if (!obj.activeInHierarchy)
            {
                return obj;
            }
        }

        GameObject newSlime = Instantiate(gemPrefab);
        gems.Add(newSlime);
        return newSlime;
    }
}
