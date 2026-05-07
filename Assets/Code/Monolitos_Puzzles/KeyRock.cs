using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyRock : MonoBehaviour
{
    [SerializeField] private GameObject _key;

    public void SpawnKey()
    {
        _key.SetActive(true);
    }
}
