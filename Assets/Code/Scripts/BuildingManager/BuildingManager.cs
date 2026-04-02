using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private InteractiveShop _shop;
    [SerializeField] private Transform _spawnPlace;

    [Header("Prefs")]
    [SerializeField] private GameObject _incineratorHologram;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /*
     cuan comprem el incinerador es desactivara el panell i es activara el holograma del incinerador.
     */
    public void IncineratorBuyed()
    {
        _shop.HandleKeyboardToggle();

        Instantiate(_incineratorHologram, _spawnPlace.position, _spawnPlace.rotation);
    }
}
