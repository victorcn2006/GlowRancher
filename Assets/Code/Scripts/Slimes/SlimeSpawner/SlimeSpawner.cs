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
    private bool _playerInZone = false;
    private bool _slimesDespawnInProcess;

    private Coroutine _despawnCoroutine;

    void Start()
    {
        //StartCoroutine(LaunchSlimes());
    }

    private IEnumerator LaunchSlimes()
    {
        for (int i = 0; i < _slimesQuantity; i++)
        {
            GameObject slime;
            if (_corrupted)
            {
                int corruptedProbability = Random.Range(0, 3); //hay una probabilidad de 1 entre 3 de que salga corrupto si el spawner está corrupto
                if (corruptedProbability == 0) slime = PoolManager.Instance.GetFirstAvailableObject("CorruptSlime");
                else
                {
                    int selectedSlimeTypeIndex = Random.Range(0, slimesList.Count);
                    slime = PoolManager.Instance.GetFirstAvailableObject(slimesList[selectedSlimeTypeIndex].name);
                }
            }
            else
            {
                int selectedSlimeTypeIndex = Random.Range(0, slimesList.Count);
                slime = PoolManager.Instance.GetFirstAvailableObject(slimesList[selectedSlimeTypeIndex].name);
            }
            slime.SetActive(true);
            slime.transform.SetParent(transform);
            slime.transform.position = transform.position;
            slime.GetComponentInChildren<Rigidbody>().AddForce(new Vector3(Random.Range(-_launchRange, _launchRange), _launchUpForce, Random.Range(-_launchRange, _launchRange)), ForceMode.Impulse);
            yield return new WaitForSeconds(0.5f);
        }
    }

    public void DespawnSlimes()
    {
        foreach (Transform child in transform)
        {
            //llamar al codigo de victor para activar shader de despawn
            child.gameObject.SetActive(false);
        }
    }

    public void SetCorrupted() => _corrupted = true;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _playerInZone = true;

            if (_despawnCoroutine != null)
            {
                StopCoroutine(_despawnCoroutine);
                _despawnCoroutine = null;
            }

            if (!_slimesDespawnInProcess)
            {
                StartCoroutine(LaunchSlimes());
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _playerInZone = false;
            _slimesDespawnInProcess = true;
            _despawnCoroutine = StartCoroutine(DespawnSlimesExtraTime());
        }
    }

    private IEnumerator DespawnSlimesExtraTime()
    {
        yield return new WaitForSeconds(5f);
        if (!_playerInZone)
        {
            DespawnSlimes();
            _slimesDespawnInProcess = false;
            _despawnCoroutine = null;
        }
    }
}
