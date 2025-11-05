using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimesSpawner : MonoBehaviour
{

    [SerializeField] private GameObject slimeToSpawn;
    [SerializeField] private int slimesNumber;

    [SerializeField] private int launchRange;
    [SerializeField] private int launchUpForce;


    private void Start()
    {
        StartCoroutine(DropSlime());
        
    }

    private IEnumerator DropSlime()
    {
        for (int i = 0; i < slimesNumber; i++)
        {
            yield return new WaitForSeconds(0.5f);
            GameObject slime = Instantiate(slimeToSpawn, transform.position, Quaternion.identity);
            slime.GetComponentInChildren<Rigidbody>().AddForce(GetRandomDropDirection(), ForceMode.Impulse);
        }

    }

    private Vector3 GetRandomDropDirection()
    {
        Vector3 LaunchDirection = new Vector3(Random.Range( -launchRange, launchRange), launchUpForce, Random.Range(-launchRange, launchRange));

        return LaunchDirection;
    }
}
