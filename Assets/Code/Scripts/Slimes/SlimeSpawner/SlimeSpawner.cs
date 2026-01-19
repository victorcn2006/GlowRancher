using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeSpawner : MonoBehaviour
{

    [SerializeField] private int _slimesQuantity;
    [SerializeField] private float _launchRange;
    [SerializeField] private float _launchUpForce;

    void Start()
    {
        StartCoroutine(LaunchSlimes());
    }

    private IEnumerator LaunchSlimes()
    {
        for (int i = 0; i < _slimesQuantity; i++)
        {
            yield return new WaitForSeconds(0.5f);
            GameObject slime = PoolManager.Instance.GetFirstAvailableObject("BasicSlime");
            slime.transform.position = transform.position;
            slime.GetComponentInChildren<Rigidbody>().AddForce(new Vector3(Random.Range(-_launchRange, _launchRange), _launchUpForce, Random.Range(-_launchRange, _launchRange)), ForceMode.Impulse);
        }
    }
}
