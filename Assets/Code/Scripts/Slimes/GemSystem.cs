using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemSystem : MonoBehaviour
{

    [SerializeField] private GemsPool.gemTypes _gemTypeToDrop;

    public IEnumerator SpawnGem()
    {
        yield return new WaitForSeconds(2f);
        GameObject newGem = GemsPool.Instance.GetGem(_gemTypeToDrop);
        newGem.SetActive(true);
        newGem.transform.position = transform.position;
    }
}
