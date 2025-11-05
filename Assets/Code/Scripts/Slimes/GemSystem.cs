using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemSystem : MonoBehaviour
{

    [SerializeField] private GameObject gem;

    public IEnumerator SpawnGem()
    {
        yield return new WaitForSeconds(2f);
        Instantiate(gem, transform.position, transform.rotation);
    }
}
