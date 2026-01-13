using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RGemSystem : MonoBehaviour
{
    [SerializeField] private GemsPool.gemTypes gemTypeToDrop;

    public IEnumerator SpawnGem()
    {
        yield return new WaitForSeconds(2f);
        GameObject newGem = GemsPool.Instance.GetGem(gemTypeToDrop);
        newGem.SetActive(true);
        newGem.transform.position = transform.position;
    }
}
