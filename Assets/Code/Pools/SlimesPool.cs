using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimesPool : MonoBehaviour
{

    public static SlimesPool Instance;

    [SerializeField] private GameObject slimePrefab;

    public List<GameObject> slimes = new List<GameObject>();


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

        foreach (GameObject obj in slimes)
        {
            if (!obj.activeInHierarchy)
            {
                return obj;
            }
        }

        GameObject newSlime = Instantiate(slimePrefab);
        slimes.Add(newSlime);
        return newSlime;
    }
}
