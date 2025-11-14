using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemSystem : MonoBehaviour
{


    public IEnumerator SpawnGem()
    {
        yield return new WaitForSeconds(2f);
        GameObject newGem = BasicGemsPool.Instance.GetFirstAvailableObject();
        newGem.SetActive(true);
        newGem.transform.position = transform.position;
    }
}
