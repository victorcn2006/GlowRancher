using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lightRefrences : MonoBehaviour
{
    [Header("light Reference")]
    [SerializeField]public List<GameObject> lights;

    [Header("Zone light")]
    public Light sun;
}
