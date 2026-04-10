using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class TomatoSpawn : MonoBehaviour, IAspirable
{
    private TomatoPlant tomatoPlant;

    private float _minTimeToSpawn;
    private float _maxTimeToSpawn;

    private float timer;

    private GameObject _tomatoPrefab;

    private void Start()
    {
        tomatoPlant = GetComponentInParent<TomatoPlant>();

        _tomatoPrefab = tomatoPlant.GetTomatoPrefab();

        _minTimeToSpawn = tomatoPlant.GetMinTimeToGrow();
        _maxTimeToSpawn = tomatoPlant.GetMaxTimeToGrow();

        ResetTimer();
    }

    private void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0) SpawnTomato();
    }

    private float ResetTimer() => timer = Random.Range(_minTimeToSpawn, _maxTimeToSpawn);

    private void SpawnTomato()
    {
        GameObject newTomato = Instantiate(_tomatoPrefab,transform);

        newTomato.transform.localScale = Vector3.zero;
        newTomato.transform.DOScale(Vector3.one, 0.75f).SetEase(Ease.OutElastic);

        newTomato.GetComponent<Rigidbody>().isKinematic = true;
        newTomato.GetComponent<Rigidbody>().useGravity = false;

    }

    public void BeingAspired()
    {
        Transform tomato = transform.GetChild(0);

        tomato.SetParent(null);

        ResetTimer();

        tomato.GetComponent<Rigidbody>().isKinematic = true;
        tomato.GetComponent<Rigidbody>().useGravity = false;

    }

    public void StopBeingAspired() { }
}
