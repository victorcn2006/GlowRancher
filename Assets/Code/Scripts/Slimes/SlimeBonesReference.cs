using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeBonesReference : MonoBehaviour
{

    [SerializeField] private List<GameObject> slimeBonesList = new List<GameObject>();

    public List<GameObject> GetSlimeBonesList()
    {
        return slimeBonesList;
    }
}
