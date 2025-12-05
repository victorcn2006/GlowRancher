using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeSpawner : MonoBehaviour
{

    [SerializeField] private int slimesQuantity;
    [SerializeField] private float launchRange;
    [SerializeField] private float launchUpForce;

    void Start()
    {
        StartCoroutine(LaunchSlimes());
    }

    private IEnumerator LaunchSlimes()
    {
        for (int i = 0; i < slimesQuantity; i++)
        {
            yield return new WaitForSeconds(0.5f);
            GameObject slime = PoolManager.Instance.GetFirstAvailableObject("BasicSlime");
            slime.transform.position = transform.position;
            slime.GetComponentInChildren<Rigidbody>().AddForce(new Vector3(Random.Range(-launchRange, launchRange), launchUpForce, Random.Range(-launchRange, launchRange)), ForceMode.Impulse);
        }
    }
}
