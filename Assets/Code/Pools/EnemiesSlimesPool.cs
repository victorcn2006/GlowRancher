using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesSlimesPool : MonoBehaviour
{

    public static EnemiesSlimesPool Instance;

    [SerializeField] private GameObject enemySlimePrefab;

    public List<GameObject> enemySlimes = new List<GameObject>();


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

        foreach (GameObject obj in enemySlimes)
        {
            if (!obj.activeInHierarchy)
            {
                return obj;
            }
        }
        GameObject newSlime = Instantiate(enemySlimePrefab);
        enemySlimes.Add(newSlime);
        return newSlime;
    }
}
