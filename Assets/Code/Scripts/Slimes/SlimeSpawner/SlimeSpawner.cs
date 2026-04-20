using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeSpawner : MonoBehaviour
{

    [SerializeField] private int _slimesQuantity;
    [SerializeField] private float _launchRange;
    [SerializeField] private float _launchUpForce;

    [SerializeField] private List<GameObject> slimesList = new List<GameObject>();

    private bool _corrupted = true;

    void Start()
    {
        StartCoroutine(LaunchSlimes());
    }

    private IEnumerator LaunchSlimes()
    {
        for (int i = 0; i < _slimesQuantity; i++)
        {
            GameObject slime;
            if (_corrupted)
            {
                int corruptedProbability = Random.Range(0, 3); //hay una probabilidad de 1 entre 3 de que salga corrupto si el spawner está corrupto

                if (corruptedProbability == 0) slime = PoolManager.Instance.GetFirstAvailableObject("Corrupted");
                else
                {
                    int selectedSlimeTypeIndex = Random.Range(0, slimesList.Count);
                    slime = PoolManager.Instance.GetFirstAvailableObject(slimesList[selectedSlimeTypeIndex].tag);
                }

            }
            else
            {
                int selectedSlimeTypeIndex = Random.Range(0, slimesList.Count);
                slime = PoolManager.Instance.GetFirstAvailableObject(slimesList[selectedSlimeTypeIndex].tag);

            }

            slime.transform.position = transform.position;
            slime.GetComponentInChildren<Rigidbody>().AddForce(new Vector3(Random.Range(-_launchRange, _launchRange), _launchUpForce, Random.Range(-_launchRange, _launchRange)), ForceMode.Impulse);

            yield return new WaitForSeconds(0.5f);
        }
    }

    public void SetCorrupted() => _corrupted = false;

}
