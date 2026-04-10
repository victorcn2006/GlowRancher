using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TomatoPlant : MonoBehaviour
{

    [SerializeField] private List<GameObject> tomatoesSpawn = new List<GameObject>();

    [SerializeField] private GameObject tomatoPrefab;

    [SerializeField] private float minTimeToGrow;
    [SerializeField] private float maxTimeToGrow;

    public GameObject GetTomatoPrefab() => tomatoPrefab;

    public float GetMinTimeToGrow() => minTimeToGrow;
    public float GetMaxTimeToGrow() => maxTimeToGrow;

}
